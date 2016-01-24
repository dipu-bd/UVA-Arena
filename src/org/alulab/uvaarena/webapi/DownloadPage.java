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
import java.util.List;
import org.apache.http.NameValuePair;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost; 
import org.apache.http.entity.ContentType;
import org.apache.http.entity.mime.MultipartEntityBuilder;
import org.apache.http.entity.mime.content.ContentBody;
import org.jsoup.nodes.Document;

/**
 * Extends DownloadTask class to handle downloading an HTML page using cookies
 * and caches.
 */
public class DownloadPage extends DownloadTask {

    private Document mDocument;
    private final ByteArrayOutputStream mBAOS;

    /**
     * Creates a new instance of DownloadPage using GET request method.
     *
     * @param url
     */
    public DownloadPage(String url) {
        // create get request
        HttpGet httpGet = new HttpGet(url);
        httpGet.setConfig(DownloadManager.getRequestConfig());
        // initialize others
        mBAOS = new ByteArrayOutputStream();
        setUriRequest(httpGet);
    }

    /**
     * Creates a new instance of DownloadPage using POST request method and with
     * provided content.
     *
     * @param url
     * @param form
     */
    public DownloadPage(String url, List<NameValuePair> form) {
        // create post request
        HttpPost httpPost = new HttpPost(url); 
        httpPost.setConfig(DownloadManager.getRequestConfig());
        // create entity
        MultipartEntityBuilder multipart = MultipartEntityBuilder.create();
        multipart.setContentType(ContentType.APPLICATION_FORM_URLENCODED);
        form.forEach((pair) -> {
            multipart.addTextBody(pair.getName(), pair.getValue());
        });
        httpPost.setEntity(multipart.build());
        // initialize others
        mBAOS = new ByteArrayOutputStream();
        setUriRequest(httpPost);
    }

    @Override
    void beforeProcessingResponse() throws IOException {
        mBAOS.reset();
    }

    @Override
    void processByte(byte[] data, int size) throws IOException {
        mBAOS.write(data, 0, size);
    }

    @Override
    void afterProcessingResponse() throws IOException {
        mBAOS.flush();
        String html = mBAOS.toString(getCharset());
        mDocument = org.jsoup.parser.Parser.parse(html, getUrl());
    }

    @Override
    void onDownloadFailed(Exception ex) {
    }

    public Document getDocument() {
        return mDocument;
    }
}
