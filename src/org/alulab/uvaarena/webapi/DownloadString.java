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

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import org.apache.commons.io.IOUtils;
import org.apache.http.HttpEntity;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.CloseableHttpClient;

/**
 *
 */
public class DownloadString extends DownloadTask {

    private String mResult;

    public DownloadString(CloseableHttpClient client, String url) {
        super(client);
        mResult = "";
        this.setUrl(url);
    }

    @Override
    public HttpUriRequest getUriRequest() {
        return new HttpGet(this.getUrl());
    }

    @Override
    public void processResponse(CloseableHttpResponse response) throws IOException, InterruptedException {
        // get entity
        HttpEntity entity = response.getEntity();
        long total = Math.max(0, entity.getContentLength());
        setTotalBytes(total);
        reportProgress();
        // get content           
        final int BUFFER_SIZE = 1024;
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        byte[] data = new byte[BUFFER_SIZE];
        try (InputStream is = entity.getContent()) {
            for (int b; (b = is.read(data)) > 0;) {
                baos.write(data);
                addDownloadedBytes(b);
                reportProgress();
                if (getStatusCode() != RUNNING) {
                    throw new InterruptedException("Download was interrupted before it had finished.");
                }
            }
        }
        // convert result
        // mResult = EntityUtils.toString(entity);     
        String encoding = (entity.getContentEncoding() == null)
                ? null : entity.getContentEncoding().getValue();
        mResult = IOUtils.toString(baos.toByteArray(), encoding);
        reportProgress();
    }

    public String getResult() {
        if (this.isFinished()) {
            return mResult;
        } else {
            return "";
        }
    }

}
