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
package org.alulab.uvaarena.webapi;

import java.io.IOException;
import java.util.ArrayList;
import org.alulab.uvaarena.util.Commons;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.CloseableHttpClient;

/**
 * Download Manager to control downloading tasks.
 */
public abstract class DownloadTask {

    final int WAITING = 0;
    final int RUNNING = 1;
    final int STOPPING = 2;
    final int FINISHED = 3;
    final int REPORT_INTERVAL_MILLIS = 100;

    private volatile int mStatus;
    private String mUrl;
    private int mRetryCount;
    private long mTotalBytes;
    private long mDownloadedBytes;
    private Exception mError;
    private final DownloadThread mThread;
    private final CloseableHttpClient mClient;
    private final ArrayList<TaskMonitor> mTaskMonitors;
    private long mLastReportTime;

    /**
     * Gets the HTTP URI Request to download data from.
     *
     * @return
     */
    public abstract HttpUriRequest getUriRequest();

    /**
     * Process the response received by the client.
     *
     * @param response
     * @throws java.io.IOException
     * @throws java.lang.InterruptedException
     */
    public abstract void processResponse(CloseableHttpResponse response) throws IOException, InterruptedException;

    /**
     * Handles the task to download data
     */
    private final class DownloadThread extends Thread {

        @Override
        public void run() {
            mStatus = RUNNING;
            for (int i = 0; mStatus == RUNNING; ++i) {
                // reset download progress
                resetCounter();
                reportProgress();
                // get response
                try (CloseableHttpResponse response = mClient.execute(getUriRequest())) {
                    //process response
                    processResponse(response);
                    // report download finished
                    mStatus = FINISHED;
                    reportFinish();
                    return;
                } catch (Exception ex) {
                    // stop download when retry count is exceeded
                    if (i == getRetryCount()) {
                        mError = ex;
                        mStatus = FINISHED;
                        reportFinish();
                        return;
                    }
                    // increase priority on failure
                    this.setPriority(Math.min(Thread.MAX_PRIORITY, this.getPriority() + 1));
                }
            }
            // when loop ended before the download could finish
            mStatus = FINISHED;
            mError = new Exception("Download Failed");
            reportFinish();
        }
    }

    /**
     * Initializes an instance of this class
     *
     * @param client
     */
    public DownloadTask(CloseableHttpClient client) {
        mStatus = WAITING;
        mClient = client;
        mUrl = null;
        mRetryCount = 0;
        mTotalBytes = 0;
        mDownloadedBytes = 0;
        mError = null;
        mTaskMonitors = new ArrayList<>();
        mThread = new DownloadThread();
    }

    /**
     * Starts the download. If download is running it ignores the request.
     */
    public void startDownload() {
        if (!mThread.isAlive()) {
            mThread.start();
        }
    }

    /**
     * Stops the download. If download is not ongoing it ignores the request.
     */
    public void stopDownload() {
        if (mThread.isAlive()) {
            mStatus = STOPPING;
        }
    }

    /**
     * Reports the current progress to all observers.
     */
    protected void reportProgress() {
        long curtime = System.currentTimeMillis();
        if (curtime - mLastReportTime < REPORT_INTERVAL_MILLIS) {
            return;
        }
        mLastReportTime = curtime;
        mTaskMonitors.forEach((TaskMonitor runnable) -> {
            runnable.statusChanged(this);
        });
    }

    protected void reportFinish() {
        if (!isFinished()) {
            return;
        }
        mTaskMonitors.forEach((TaskMonitor runnable) -> {
            runnable.downloadFinished(this);
        });
    }

    /**
     * Resets all the counters and make it ready for another download.
     */
    protected void resetCounter() {
        mError = null;
        mTotalBytes = 0;
        mDownloadedBytes = 0;
    }

    /**
     * Gets the thread controlling the download
     *
     * @return
     */
    public Thread getDownloadThread() {
        return mThread;
    }

    /**
     * Gets the URL of the download task
     *
     * @return
     */
    public String getUrl() {
        return mUrl;
    }

    /**
     * Sets the URL of the download task
     *
     * @param url
     */
    protected void setUrl(String url) {
        mUrl = url;
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
     * Set the total bytes to be downloaded.
     *
     * @param bytes
     */
    protected void setTotalBytes(long bytes) {
        mTotalBytes = bytes;
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
     * Sets the number of bytes downloaded.
     *
     * @param bytes
     */
    protected void setDownloadedBytes(long bytes) {
        mDownloadedBytes = bytes;
        mTotalBytes = Math.max(bytes, mTotalBytes);
    }

    /**
     * Adds the number of bytes with current downloaded bytes.
     *
     * @param bytesToAdd
     */
    protected void addDownloadedBytes(long bytesToAdd) {
        setDownloadedBytes(mDownloadedBytes + bytesToAdd);
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
        if (mTotalBytes == 0) {
            return 1E-14;
        } else {
            return (double) mDownloadedBytes * 100.0 / (double) mTotalBytes + 1E-14;
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
        return String.format(args, getDownloadProgress());
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
     * Sets the number of times to retry on failure.
     *
     * @param count
     */
    public void setRetryCount(int count) {
        mRetryCount = Math.max(0, count);
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
     * Sets the task monitor to monitor progress of the download
     *
     * @param taskMonitor
     */
    protected void addTaskMonitor(TaskMonitor taskMonitor) {
        if (taskMonitor != null) {
            mTaskMonitors.add(taskMonitor);
        }
    }

    /**
     * Sets the status of this task.
     *
     * @param value
     */
    protected void setStatusCode(int value) {
        mStatus = value;
    }

    /**
     * Gets the current status code of the task.
     *
     * @return
     */
    public int getStatusCode() {
        return mStatus;
    }

    /**
     * True if the task is stopped and waiting to be called.
     *
     * @return
     */
    public boolean isWaiting() {
        return mStatus == WAITING;
    }

    /**
     * True if the task is running.
     *
     * @return
     */
    public boolean isRunning() {
        return mStatus == RUNNING || mStatus == STOPPING;
    }

    /**
     * True if the task has finished.
     *
     * @return
     */
    public boolean isFinished() {
        return mStatus == FINISHED;
    }

    /**
     * True if the task has finished successfully.
     *
     * @return
     */
    public boolean isSuccess() {
        return mStatus == FINISHED && mError == null;
    }

    /**
     * True if the task has failed to download properly.
     *
     * @return
     */
    public boolean isFailed() {
        return mStatus == FINISHED && mError != null;
    }

    @Override
    public String toString() {
        String status = "WAITING";
        if (mStatus == RUNNING) {
            status = "DOWNLOADING";
        } else if (mStatus == STOPPING) {
            status = "STOPPING";
        } else if (mError != null) {
            status = "ERROR";
        } else if (mStatus == FINISHED) {
            status = "SUCCESS";
        }
        String out = String.format("%s : %s%% [%s of %s] ~~ %s",
                mUrl, getDownloadProgress(2), getDownloadedByteLength(), getTotalByteLength(), status);
        if (mError != null) {
            out += " : " + mError.getMessage();
        }
        return out;
    }

}

/*
 * STATE TRANSITION GRAPH --------------------------------------- 
 * waiting -> running 
 * running -> error, stopping, waiting 
 * stopping -> error, waiting 
 * error -> running, waiting 
 * --------------------------------------- 
 * GROUP1 : OUT OF THREAD : WAITING 
 * GROUP2 : IN_THREAD : STOPPING, RUNNING, ERROR 
 * GROUP3 : INTERNAL_IN_THREAD : RUNNING, ERROR 
 * ---------------------------------------
 * GROUP1 -> GROUP3 
 * GROUP3 -> GROUP1, GROUP2 
 * GROUP2 -> GROUP1
 */
