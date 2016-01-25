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
import java.net.CookieHandler;
import java.net.CookieManager;
import java.net.URL;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.jsoup.Connection;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.nodes.FormElement;

/**
 * A customized REST client for the web
 *
 * @author Dipu
 */
public class RestClient {

    private static final Logger logger = Logger.getLogger(RestClient.class.getName());
    private static final Document EMPTY_DOCUMENT = Jsoup.parse("<!DOCTYPE html><html></html>");

    private String mUrl;
    private Document mDocument;
    private final Map<String, String> mHeaders;
    private final Map<String, String> mCookies;

    public RestClient() {
        mHeaders = new HashMap<>();
        mCookies = new HashMap<>();
        mDocument = EMPTY_DOCUMENT.clone();
    }

    private synchronized void openResponse(Connection connection) {
        try {
            mHeaders.clear();
            Connection.Response response;
            response = connection
                    .timeout(5000)
                    .cookies(mCookies)
                    .userAgent(DownloadManager.USER_AGENT)
                    .execute();
            mUrl = response.url().toString();
            mCookies.putAll(response.cookies());
            mHeaders.putAll(response.headers());
            mDocument = response.parse();
        } catch (Exception ex) {
            logger.log(Level.SEVERE, null, ex);
            mUrl = null;
            mDocument = EMPTY_DOCUMENT.clone();
        }
    }

    /**
     * Navigate to the given URL
     *
     * @param url URL to navigate to.
     */
    public synchronized void load(String url) {
        openResponse(Jsoup.connect(url));
    }

    /**
     * Submits a form element.
     *
     * @param form Form Element to submit.
     */
    public synchronized void submitForm(FormElement form) {
        openResponse(form.submit().method(Connection.Method.POST));
    }

    /**
     * Gets the URL of the client
     *
     * @return URL object of NULL if none.
     */
    public String getURL() {
        return mUrl;
    }

    /**
     * Gets the lists of cookies that are active now.
     *
     * @return Map of cookies. Empty if none.
     */
    public Map<String, String> getCookies() {
        return Collections.unmodifiableMap(mCookies);
    }

    /**
     * Gets the header list received at last response.
     *
     * @return Map of headers. Empty if none.
     */
    public Map<String, String> getHeaders() {
        return Collections.unmodifiableMap(mHeaders);
    }

    /**
     * Gets the underlying HTML document.
     *
     * @return Document object. Never null.
     */
    public Document getDocument() {
        return mDocument;
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
     * <p>
     * Normally it matches the NAME attribute with the KEY of <b>data</b>
     * parameter. But another kind of match can be defined. Suppose we have
     * these elements-
     * <pre> &lt;input type="radio" name="rad1" value="1"&gt;
     * &lt;input type="radio" name="rad2" value="2"&gt;
     * &lt;input type="radio" name="rad3" value="3"&gt;</pre>
     * <br/>
     * Then if we put <code>(" #rad2", "2")</code> in <b>data<b>, the second
     * input will have its "checked" attribute altered to True and will looks
     * like-
     * <pre> &lt;input type="radio" name="rad1" value="1"&gt;
     * &lt;input type="radio" name="rad2" value="2" checked="true"&gt;
     * &lt;input type="radio" name="rad3" value="3"&gt;</pre>
     * </p>
     *
     * @param form Form element to fill up.
     * @param data Values to fill up.
     */
    public void fillUpForm(FormElement form, Map<String, String> data) {
        form.elements().forEach((tag) -> {
            String name = tag.attr("name");
            if (data.containsKey(" #" + name)) {
                if (tag.val().equals(data.get(" #" + name))) {
                    tag.attr("checked", true);
                }
            } else {
                tag.val(data.getOrDefault(name, tag.val()));
            }
        });
    }
}
