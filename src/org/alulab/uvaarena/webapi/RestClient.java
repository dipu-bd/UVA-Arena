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

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.message.BasicNameValuePair;
import org.jsoup.nodes.Element;
import org.jsoup.nodes.FormElement; 

/**
 * A customized REST client for the web
 *
 * @author Dipu
 */
public class RestClient extends DownloadPage {

    public RestClient() {
        super("");
    }

    public void load(String url) {
        HttpGet httpGet = new HttpGet(url);
        httpGet.setConfig(DownloadManager.getRequestConfig());
        setUriRequest(httpGet);
        startDownload();
    }

    public void submitForm(String url, List<NameValuePair> data) throws UnsupportedEncodingException {
        HttpPost httpPost = new HttpPost(url);
        // add header                
        httpPost.setHeader("User-Agent", DownloadManager.USER_AGENT);
        httpPost.setHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        httpPost.setHeader("Accept-Language", "en-US,en;q=0.5");
        httpPost.setHeader("Cookie", getCookies());
        httpPost.setHeader("Referer", getUrl());
        httpPost.setHeader("Content-Type", "application/x-www-form-urlencoded");
        // set entity
        httpPost.setEntity(new UrlEncodedFormEntity(data));
        // start download
        setUriRequest(httpPost);
        startDownload();
    }

    public void submitForm(FormElement form) throws UnsupportedEncodingException {
        HttpPost httpPost = new HttpPost(form.absUrl("action"));
        // add header                
        httpPost.setHeader("User-Agent", DownloadManager.USER_AGENT);
        httpPost.setHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        httpPost.setHeader("Accept-Language", "en-US,en;q=0.5");
        httpPost.setHeader("Cookie", getCookies());
        httpPost.setHeader("Referer", getUrl());
        // set content type        
        String type = form.attr("enctype");
        httpPost.setHeader("Content-Type", (type == null) ? "application/x-www-form-urlencoded" : type);
        // set entity         
        List<NameValuePair> data = new ArrayList<>();
        form.formData().forEach((kv) -> {
            data.add(new BasicNameValuePair(kv.key(), kv.value()));
        });
        httpPost.setEntity(new UrlEncodedFormEntity(data));
        // start download
        setUriRequest(httpPost);
        startDownload();
    }

    /**
     * Gets a list of all form elements of this document
     *
     * @return
     */
    public List<FormElement> getAllForms() {
        return getDocument().getElementsByTag("form").forms();
    }

    /**
     * Gets a form by id
     *
     * @param id ID of the form
     * @return
     */
    public FormElement getFormById(String id) {
        Element elem = getDocument().getElementById(id);
        return (elem instanceof FormElement) ? (FormElement) elem : null;
    }

    /**
     * Gets a form by the value of its attribute.
     *
     * @param attribute Attribute name.
     * @param value Value of the attribute.
     * @return Null if none found.
     */
    public FormElement getFormByAttributeValue(String attribute, String value) {
        List<FormElement> forms = getDocument().getElementsByAttributeValue(attribute, value).forms();
        return forms.isEmpty() ? null : forms.get(0);
    }

    /**
     * Fill up a FormElement by the provided value.
     *
     * @param form Form element to fill up.
     * @param data Values to fill up.
     */
    public void fillUpForm(FormElement form, HashMap<String, String> data) {
        form.elements().forEach((tag) -> {
            tag.val(data.getOrDefault(tag.attr("name"), tag.val()));
        });
    }
}
