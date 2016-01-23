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

import java.io.File;
import org.apache.http.HttpHost;
import org.apache.http.conn.routing.HttpRoute;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.impl.conn.PoolingHttpClientConnectionManager;

/**
 * This class handles all downloading tasks.
 */
public final class DownloadManager {

    final int DEFAULT_MAX_TOTAL = 20;

    private int mPerRoute;
    private final CloseableHttpClient mClient;
    private final PoolingHttpClientConnectionManager mHttpPool;

    /**
     * Initializes this instance of download manager
     */
    public DownloadManager() {
        mHttpPool = new PoolingHttpClientConnectionManager();
        mClient = HttpClients.custom().setConnectionManager(mHttpPool).build();

        mHttpPool.setMaxTotal(DEFAULT_MAX_TOTAL);
    }

    /**
     * Sets the number of maximum total concurrent connections. Default value is
     * 20.
     *
     * @param maxTotal
     */
    public void setMaxTotal(int maxTotal) {
        mHttpPool.setMaxTotal(maxTotal);
    }

    /**
     * Gets the number of maximum total concurrent connections.
     *
     * @return
     */
    public int getMaxTotal() {
        return mHttpPool.getMaxTotal();
    }

    /**
     * Sets the number of maximum concurrent connection to a specific host
     *
     * @param host
     * @param maxConn
     */
    public void setMaxPerRoute(String host, int maxConn) {
        mHttpPool.setMaxPerRoute(new HttpRoute(new HttpHost(host)), maxConn);
    }

    /**
     * Gets the client to make HTTP connection and download data.
     *
     * @return
     */
    public CloseableHttpClient getHttpClient() {
        return mClient;
    }

    /**
     * Creates a new download string task and returns its instance. It does not
     * starts the download. Call startDownload() method to start the download.
     *
     * @param url URL to download
     * @return
     */
    public DownloadString downloadString(String url) {
        return new DownloadString(mClient, url);
    }

    /**
     * Creates a new download string task and returns its instance. It does not
     * starts the download. Call startDownload() method to start the download.
     *
     * @param url URL to download
     * @param taskMonitor TaskMonitor object to monitor download progress.
     * @return
     */
    public DownloadString downloadString(String url, TaskMonitor taskMonitor) {
        DownloadString ds = new DownloadString(mClient, url);
        ds.addTaskMonitor(taskMonitor);
        return ds;
    }

    /**
     * Creates a new download file task and returns its instance. It does not
     * starts the download. Call startDownload() method to start the download.
     *
     * @param url URL to download
     * @param storeFile File to store downloaded data
     * @return
     */
    public DownloadFile downloadFile(String url, File storeFile) {
        return new DownloadFile(mClient, url, storeFile);
    }

    /**
     * Creates a new download file task and returns its instance. It does not
     * starts the download. Call startDownload() method to start the download.
     *
     * @param url URL to download
     * @param storeFile File to store downloaded data
     * @param taskMonitor TaskMonitor object to monitor download progress.
     * @return
     */
    public DownloadFile downloadFile(String url, File storeFile, TaskMonitor taskMonitor) {
        DownloadFile df = new DownloadFile(mClient, url, storeFile);
        df.addTaskMonitor(taskMonitor);
        return df;
    }
 

}
