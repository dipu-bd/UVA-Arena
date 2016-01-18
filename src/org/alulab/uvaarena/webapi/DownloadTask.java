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

import java.util.Observable;
import org.alulab.uvaarena.util.Commons;

/**
 * Download Manager to control downloading tasks.
 */
public abstract class DownloadTask extends Observable implements Runnable {

    final int WAITING = 0;
    final int RUNNING = 1;
    final int STOPPING = 2;

    private String mUrl = null;
    private long mTotalBytes = 0;
    private long mDownloadedBytes = 0;
    private Exception mError = null;
    private int mTimeout = 3000;
    private int mRetryCount = 0;
    private int mStatus = WAITING;
    private Thread mThread = null;
    private int mPriority = Thread.NORM_PRIORITY;

    public DownloadTask() {

    }

    private void reportProgress() {
        setChanged();
    }

    @Override
    public void finalize() throws Throwable {
        stopDownload();
        super.finalize();
    }

    public void startDownload() {
        if (mStatus == WAITING) {
            mError = null;
            mThread = new Thread(this);
            mThread.setPriority(mPriority);
            mThread.start();
        }
    }

    public void stopDownload() {
        if (mStatus != WAITING) {
            mStatus = STOPPING;
        }
    }

    public double downloadProgress() {
        if (mTotalBytes == 0) {
            return 0;
        } else {
            return (double) mDownloadedBytes / mTotalBytes;
        }
    }

    public String downloadProgress(int precission) {
        String args = "%f";
        if (precission > 0) {
            args = "%." + String.valueOf(precission) + "f";
        }
        return String.format(args, downloadProgress());
    }
    
    public void resetCounter() {
        mTotalBytes = 0;
        mDownloadedBytes = 0;
        mError = null;        
    }
    
    public String getUrl() {
        return mUrl;
    }

    public void setUrl(String url) {
        mUrl = url;
    }

    public long getTotalBytes() {
        return mTotalBytes;
    }
    public void setTotalBytes(long bytes) {
        mTotalBytes = bytes;
    }
    public String getTotalByteLength() {
        return Commons.formatByteLength(mTotalBytes);
    }
    
    public long getDownloadedBytes() {
        return mDownloadedBytes;                
    }            
    public void setDownloadedBytes(long bytes) {
        mDownloadedBytes = bytes;
    }
    public void addDownloadedBytes(long bytesToAdd) {
        mDownloadedBytes += bytesToAdd;
    }    
    public String getDownloadedByteLength() {
        return Commons.formatByteLength(mDownloadedBytes);
    }

    public int getPriority() {
        return mPriority;
    }
    public void setPriority(int threadPriority) {
        mPriority = threadPriority;
    }
    
    public int getRetryCount() {
        return mRetryCount;
    }
    public void setRetryCount(int count) {
        mRetryCount = count;
    }

    public int getTimeout() {
        return mTimeout;
    }
    public void setTimeout(int value) {
        mTimeout = value;
    }
    
    public String getErrorMessage() {
        return (mError != null) ? mError.getMessage() : "No Error";
    }

    public Exception getError() {
        return mError;
    }    
    public Thread getThread() {
        return mThread;
    }    

    protected void setStatusCode(int value) {
        mStatus = value;
    }
    public int getStatusCode() {
        return mStatus;
    }
    
    public boolean isStopped() {
        return mStatus == WAITING;
    }

    public boolean isRunning() {
        return mStatus != WAITING;
    }

    public boolean hasFailed() {
        return mStatus == WAITING && mError != null;
    }
    
    @Override
    public String toString() {
        String status = "WAITING";
        if (mStatus == RUNNING) {
            status = "RUNNING";
        } else if (mStatus == STOPPING) {
            status = "STOPPING";
        } else {
            status = "ERROR";
        }
        return String.format("[%s,%d,%d,%s,%d]", mUrl, mDownloadedBytes, mTotalBytes, status, mRetryCount);
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
