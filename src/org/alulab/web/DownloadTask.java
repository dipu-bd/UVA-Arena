/*
 * Copyright (c) 2016, Sudipto Chandra
 * 
 * Permission to use, copy, modify, and/or distribute this software for any
 * purpose with or without fee is hereby granted, provided that the above
 * copyright notice and this permission notice appear in all copies.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
 * WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
 * ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
 * WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
 * ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
 * OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
 */
package org.alulab.web;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import org.uvaarena.util.Commons;
import org.apache.http.HttpEntity;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpUriRequest;

/**
 * Abstract class to handle downloading task.
 */
public abstract class DownloadTask {

    private static final String DEFAULT_CHARSET = "UTF-8";

    final int BUFFER_SIZE = 2048;
    final long REPORT_INTERVAL_NANOS = 100_000_000; // 0.1 sec

    private volatile WorkState mStatus = WorkState.WAITING;
    private HttpUriRequest mUriRequest;
    private int mRetryCount = 0;
    private long mTotalBytes = 0;
    private long mDownloadedBytes = 0;
    private boolean mChunked = false;
    private String mContentCharset = null;
    private int mPriority = Thread.MIN_PRIORITY;

    //private final String mHashCode;
    private Exception mError = null;
    private long mLastReportTime = 0;
    private long mIntervalPassed = 0;
    private final ArrayList<TaskMonitor<? extends DownloadTask>> mTaskMonitors;

    /**
     * Initializes an instance of this class
     *
     * @param uriRequest URL to download
     */
    public DownloadTask(HttpUriRequest uriRequest) {
        mUriRequest = uriRequest;
        mTaskMonitors = new ArrayList<>();
        //mHashCode = Commons.generateHashString();
    }

    @Override
    public String toString() {
        return String.format("%s : %s%% [%s of %s] @ %s ~ %s",
                mUriRequest, getDownloadProgress(2), getDownloadedByteLength(),
                getTotalByteLength(), getDownloadSpeedFormatted(), getStatusMessage());
    }

    /**
     * Method that gets called before the starting to process the response.
     *
     * @throws IOException
     */
    abstract void beforeProcessingResponse() throws IOException;

    /**
     * Method that gets called after receiving a chunk of data. Usually intended
     * to process and save the data in the way the sub class wants.
     *
     * @param data
     * @throws IOException
     */
    abstract void processByte(byte[] data, int size) throws IOException;

    /**
     * Method that gets called after all content of response has been received
     * successfully.
     *
     * @throws IOException
     */
    abstract void afterProcessingResponse() throws IOException;

    /**
     * Method that gets called when the download has failed.
     */
    abstract void onDownloadFailed(Exception ex);

    /**
     * Handles the task to download data
     */
    private final Thread mDownloadThread = new Thread(() -> {
        mStatus = WorkState.RUNNING;
        for (int i = 0; mStatus == WorkState.RUNNING; ++i) {
            // reset some data
            mError = null;
            mTotalBytes = 0;
            mDownloadedBytes = 0;
            mIntervalPassed = 0;
            reportProgress();
            // get response
            try (final CloseableHttpResponse response = DownloadManager.getHttpClient().execute(mUriRequest)) {
                // before processing response
                beforeProcessingResponse();
                // processing response
                processResponse(response);
                // after processing response
                afterProcessingResponse();
                // report finish on success
                mStatus = WorkState.FINISHED;
                reportFinish();
                return;
            } catch (Exception ex) {
                // increase priority of this thread on failure
                if (DownloadTask.this.getPriority() < Thread.MAX_PRIORITY) {
                    DownloadTask.this.setPriority(DownloadTask.this.getPriority() + 1);
                }
                // stop download when retry slot is empty
                if (i == getRetryCount() || mStatus != WorkState.RUNNING) {
                    mError = ex;
                    onDownloadFailed(ex);
                    // report finish on failure
                    mStatus = WorkState.FAILED;
                    reportFinish();
                    return;
                }
            }
        }
        // report finish on end of loop
        mError = new Exception("Download Failed");
        mStatus = WorkState.FAILED;
        reportFinish();
    });

    /**
     * Processes the response and receive contents in chunk
     */
    private boolean processResponse(CloseableHttpResponse response) throws IOException, InterruptedException {
        // get entity
        HttpEntity entity = response.getEntity();
        mChunked = entity.isChunked();
        // get total bytes
        mTotalBytes = Math.max(0, entity.getContentLength());
        reportProgress();
        // get content
        long start = System.nanoTime();
        byte[] data = new byte[BUFFER_SIZE];
        try (InputStream is = entity.getContent()) {
            for (int b; (b = is.read(data)) > 0;) {
                processByte(data, b);
                mDownloadedBytes += b;
                mTotalBytes = Math.max(mTotalBytes, mDownloadedBytes);
                mIntervalPassed = System.nanoTime() - start;
                reportProgress();
                // check if download should continue
                if (mStatus == WorkState.STOPPING) {
                    throw new InterruptedException("Download was interrupted before it was finished.");
                }
            }
        }
        // set download time in nonosecs
        mIntervalPassed = System.nanoTime() - start;
        // get encoding
        mContentCharset = DEFAULT_CHARSET;
        if (entity.getContentType() != null) {
            String contentType = entity.getContentType().getValue();
            String charsetKey = "charset=";
            int index = contentType.indexOf(charsetKey);
            if (index >= 0) {
                int last = Math.max(contentType.indexOf(";", index), contentType.length());
                mContentCharset = contentType.substring(index + charsetKey.length(), last);
            }
        }
        // all done
        return true;
    }

    /**
     * Starts the download. If download is running it ignores the request.
     */
    public void startDownload() {
        if (isWaiting()) {
            mDownloadThread.setPriority(mPriority);
            mDownloadThread.start(); 
        }
    }

    /**
     * Stops the download. If download is not ongoing it ignores the request.
     */
    public void stopDownload() {
        if (isRunning()) {
            mStatus = WorkState.STOPPING;
        }
    }

    /**
     * Reports the current progress to all observers.
     */
    private void reportProgress() {
        long curtime = System.nanoTime();
        if (curtime - mLastReportTime >= REPORT_INTERVAL_NANOS) {
            DownloadTask task = this;
            mLastReportTime = curtime;
            mTaskMonitors.forEach((TaskMonitor runnable) -> {
                java.awt.EventQueue.invokeLater(() -> {
                    runnable.statusChanged(task);
                });
            });
        }
    }

    /**
     * Reports that the download is finished to all observers.
     */
    private void reportFinish() {
        if (isFinished()) {
            DownloadTask task = this;
            mTaskMonitors.forEach((TaskMonitor runnable) -> {
                java.awt.EventQueue.invokeLater(() -> {
                    runnable.downloadFinished(task);
                });
            });
        }
    }

    /**
     * Sets the task monitor to monitor progress of the download
     *
     * @param <T>
     * @param taskMonitor
     * @return
     */
    public <T extends DownloadTask> T addTaskMonitor(TaskMonitor<T> taskMonitor) {
        if (taskMonitor != null) {
            mTaskMonitors.add(taskMonitor);
        }
        return (T) this;
    }

    /**
     * Sets the number of times to retry on failure.
     *
     * @param count
     * @return
     */
    public DownloadTask setRetryCount(int count) {
        mRetryCount = Math.max(0, count);
        return this;
    }

    /**
     * Sets the default thread priority
     *
     * @param priority
     * @return
     */
    public DownloadTask setPriority(int priority) {
        mPriority = priority;
        return this;
    }

    /**
     * Gets the URL of the download task.
     *
     * @return
     */
    public String getUrl() {
        return mUriRequest.getURI().toString();
    }

    /**
     * Gets the URI Request of the download task.
     *
     * @return
     */
    public HttpUriRequest getUriRequest() {
        return mUriRequest;
    }

    /**
     * Gets the total bytes to be downloaded.
     *
     * @return
     */
    public long getTotalBytes() {
        return mTotalBytes;
    }

    /**
     * Gets the length of total byte in well formatted string.
     *
     * @return
     */
    public String getTotalByteLength() {
        return Commons.formatByteLength(mTotalBytes);
    }

    /**
     * Gets the number of bytes downloaded.
     *
     * @return
     */
    public long getDownloadedBytes() {
        return mDownloadedBytes;
    }

    /**
     * Gets the length of downloaded bytes in a well formatted string.
     *
     * @return
     */
    public String getDownloadedByteLength() {
        return Commons.formatByteLength(mDownloadedBytes);
    }

    /**
     * Gets the current progress of download in percentage.
     *
     * @return
     */
    public double getDownloadProgress() {
        if (mTotalBytes == 0 || mTotalBytes < mDownloadedBytes) {
            return 0;
        } else {
            return (double) mDownloadedBytes * 100.0 / (double) mTotalBytes;
        }
    }

    /**
     * Gets the current progress of download valid upto a fixed precision.
     *
     * @param precision Precision of the download progress.
     * @return
     */
    public String getDownloadProgress(int precision) {
        String args = "%f";
        if (precision > 0) {
            args = "%." + String.valueOf(precision) + "f";
        }
        return String.format(args, getDownloadProgress() + 1E-14);
    }

    /**
     * Gets the raw download speed in bytes/seconds
     *
     * @return
     */
    public double getDownloadSpeed() {
        double download = (double) mDownloadedBytes;
        double seconds = (double) mIntervalPassed;
        double unit = 1_000_000_000.0; // 1 nano second
        double speed = (seconds == 0) ? download : unit * download / seconds;
        return (speed < download) ? speed : download;
    }

    /**
     * Gets well formatted download speed in bytes/seconds
     *
     * @param precission Precision of the download speed.
     * @return
     */
    public String getDownloadSpeedFormatted(int precission) {
        return Commons.formatByteLength(getDownloadSpeed(), precission) + "/s";
    }

    /**
     * Gets well formatted download speed in bytes/seconds upto 2 digits after
     * decimal point.
     *
     * @return
     */
    public String getDownloadSpeedFormatted() {
        return getDownloadSpeedFormatted(2);
    }

    /**
     * Gets the total time spend on downloading in nanoseconds.
     *
     * @return
     */
    public long getDownloadTime() {
        return mIntervalPassed;
    }

    /**
     * Get the number of times to retry on failure.
     *
     * @return
     */
    public int getRetryCount() {
        return mRetryCount;
    }

    /**
     * Gets the default thread priority
     *
     * @return
     */
    public int getPriority() {
        return mPriority;
    }

    /**
     * Gets the error message. If no error, "No Error" is returned.
     *
     * @return
     */
    public String getErrorMessage() {
        return (mError != null) ? mError.getMessage() : "No Error";
    }

    /**
     * Gets the Error. If none, a null value is returned.
     *
     * @return
     */
    public Exception getError() {
        return mError;
    }

    /**
     * Gets the content encoding. If encoding is unknown a NULL value is
     * returned indicating the default encoding should be used.
     *
     * @return
     */
    public String getCharset() {
        return mContentCharset;
    }

    /**
     * Gets the current status code of the task.
     *
     * @return
     */
    public WorkState getStatusCode() {
        return mStatus;
    }

    /**
     * Gets the status message of the current task.
     *
     * @return
     */
    public String getStatusMessage() {
        switch (mStatus) {
            case WAITING:
                return "Waiting";
            case RUNNING:
                return "Running";
            case STOPPING:
                return "Stopping";
            case FINISHED:
                return "Success";
            case FAILED:
                return "Failed : " + mError.getMessage();
            default:
                return "Unknown";
        }
    }

    /**
     * True if the last attempt to download received InputStream in chunk.
     *
     * @return
     */
    public boolean isChunked() {
        return mChunked;
    }

    /**
     * True if the task is stopped and waiting to be called.
     *
     * @return
     */
    public boolean isWaiting() {
        return mStatus == WorkState.WAITING;
    }

    /**
     * True if the task is running.
     *
     * @return
     */
    public boolean isRunning() {
        return mStatus == WorkState.RUNNING || mStatus == WorkState.STOPPING;
    }

    /**
     * True if the task has finished.
     *
     * @return
     */
    public boolean isFinished() {
        return mStatus == WorkState.FINISHED || mStatus == WorkState.FAILED;
    }

    /**
     * True if the task has finished successfully.
     *
     * @return
     */
    public boolean isSuccess() {
        return mStatus == WorkState.FINISHED;
    }

    /**
     * True if the task has failed to download properly.
     *
     * @return
     */
    public boolean isFailed() {
        return mStatus == WorkState.FAILED;
    }

    /**
     * Gets a unique hash code for this object
     *
     * @return
     */
    //public String getHashCode() {
    //    return mHashCode;
    //}
}
