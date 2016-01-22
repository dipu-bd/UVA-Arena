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
import java.io.ByteArrayOutputStream;
import org.apache.commons.io.IOUtils;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.CloseableHttpClient;

/**
 * Extends the DownloadTask class to download string data.
 */
public class DownloadString extends DownloadTask {

    private String mResult = "";
    private final ByteArrayOutputStream mBAOS;

    public DownloadString(CloseableHttpClient client, String url) {
        super(client, url);
        mBAOS = new ByteArrayOutputStream();
    }

    /**
     * Gets the result string of the download. If download did not succeed an
     * empty string is returned.
     *
     * @return
     */
    public String getResult() {
        if (isSuccess()) {
            return mResult;
        } else {
            return "";
        }
    }

    @Override
    HttpUriRequest getUriRequest() {
        return new HttpGet(this.getUrl());
    }

    @Override
    void beforeDownloadStart() {
        mBAOS.reset();
    }

    @Override
    void processByte(byte[] data) throws IOException {
        mBAOS.write(data);
    }

    @Override
    void afterDownloadSucceed() throws IOException {
        mResult = IOUtils.toString(mBAOS.toByteArray(), getEncoding());
    }

}
