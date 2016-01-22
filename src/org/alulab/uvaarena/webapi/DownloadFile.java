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

import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import org.apache.commons.io.FileUtils;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.CloseableHttpClient;

/**
 *
 * @author Dipu
 */
public class DownloadFile extends DownloadTask {

    private File mFile = null;
    private FileOutputStream mFIS;

    public DownloadFile(CloseableHttpClient client, String url, File file) {
        super(client, url);
        mFile = file;
    }

    public File getFile() {
        return mFile;
    }

    public void setFile(File file) throws IllegalAccessError {
        if (isRunning()) {
            throw new IllegalAccessError("Can not change file while download task is running");
        } else {
            mFile = file;
        }
    }

    @Override
    HttpUriRequest getUriRequest() {
        return new HttpGet(this.getUrl());
    }

    @Override
    void beforeDownloadStart() throws IOException {
        if (mFIS != null) {
            mFIS.close();
        }
        mFIS = FileUtils.openOutputStream(mFile);
    }

    @Override
    void processByte(byte[] data) throws IOException {
        mFIS.write(data);
    }

    @Override
    void afterDownloadSucceed() throws IOException {
        mFIS.flush();
        mFIS.close();
        mFIS = null;
    }
}
