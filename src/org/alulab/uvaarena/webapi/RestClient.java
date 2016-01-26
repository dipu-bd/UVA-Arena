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

import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLDecoder;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.beans.property.ObjectProperty;
import javafx.beans.property.ReadOnlyObjectProperty;
import javafx.beans.property.SimpleObjectProperty;
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

    private URL mUrl;
    private Map<String, String> mUrlQuery;
    private final Map<String, String> mHeaders;
    private final Map<String, String> mCookies;
    private final ObjectProperty<Document> mDocument;

    /**
     * Creates a new RestClient
     */
    public RestClient() {
        mHeaders = new HashMap<>();
        mCookies = new HashMap<>();
        mUrlQuery = new HashMap<>();
        mDocument = new SimpleObjectProperty<>(EMPTY_DOCUMENT);
    }

    // open response from a connection and process it
    private void openResponse(Connection connection) {
        try {
            mHeaders.clear();
            Connection.Response response;
            response = connection
                    .timeout(5000)
                    .cookies(mCookies)
                    .userAgent(DownloadManager.USER_AGENT)
                    .execute();
            mUrl = response.url();
            mUrlQuery = parseURL(mUrl);
            mCookies.putAll(response.cookies());
            mHeaders.putAll(response.headers());
            mDocument.set(response.parse());
        } catch (Exception ex) {
            logger.log(Level.WARNING, null, ex);
            mUrl = null;
            mUrlQuery.clear();
            mDocument.set(EMPTY_DOCUMENT.clone());
        }
    }

    /**
     * Navigate to the given URL
     *
     * @param url URL to navigate to.
     */
    public void load(String url) {
        openResponse(Jsoup.connect(url));
    }

    /**
     * Submits a form element.
     *
     * @param form Form Element to submit.
     */
    public void submitForm(FormElement form) {
        openResponse(form.submit().method(Connection.Method.POST));
    }

    /**
     * Resets the client and clears all caches and cookies
     */
    public void reset() {
        mUrl = null;
        mHeaders.clear();
        mCookies.clear();
        mUrlQuery.clear();
        mDocument.set(EMPTY_DOCUMENT.clone());
    }

    /**
     * Re-downloads the document.
     */
    public void reload() {
        load(getFullUrl());
    }

    /**
     * Get the Document property.
     *
     * @return
     */
    public ReadOnlyObjectProperty<Document> documentProperty() {
        return (ReadOnlyObjectProperty<Document>) mDocument;
    }

    /**
     * Gets the URL of the client
     *
     * @return NULL if none.
     */
    public URL getURL() {
        return mUrl;
    }

    /**
     * Get the host of the client
     *
     * @return Empty if none.
     */
    public String getHost() {
        return mUrl == null ? "" : mUrl.getHost();
    }

    /**
     * Gets the URL of the client
     *
     * @return Empty string if none.
     */
    public String getFullUrl() {
        return mUrl == null ? "" : mUrl.toString();
    }

    /**
     * Gets the value of a query key in the URL
     *
     * @param name Name of the query key
     * @return Query value or empty string if none (never null)
     */
    public String getQueryValue(String name) {
        return mUrlQuery.getOrDefault(name, "");
    }

    /**
     * Gets the queries in the URL
     *
     * @return
     */
    public Map<String, String> getQueries() {
        return Collections.unmodifiableMap(mUrlQuery);
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
        return mDocument.get();
    }

    /**
     * Gets a form by id
     *
     * @param id ID of the form
     * @return
     */
    public FormElement getFormById(String id) {
        final Element elem = getDocument().getElementById(id);
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
        final List<FormElement> forms = getDocument().getElementsByAttributeValue(attribute, value).forms();
        return forms.isEmpty() ? null : forms.get(0);
    }

    /**
     * Converts URL into a map of (key, value) pair of query data.
     *
     * @param url URL to parse query
     * @return KeyValue pair of queries. Empty if none.
     */
    public static Map<String, String> parseURL(URL url) {
        Map<String, String> query_pairs = new HashMap<>();
        try {
            String query = url.getQuery();
            String[] pairs = query.split("&");
            for (String pair : pairs) {
                int idx = pair.indexOf("=");
                final String key = URLDecoder.decode(pair.substring(0, idx), "UTF-8");
                final String val = URLDecoder.decode(pair.substring(idx + 1), "UTF-8");
                query_pairs.put(key, val);
            }
        } catch (Exception ex) {
        }
        return query_pairs;
    }

    /**
     * Converts URL into a map of (key, value) pair of query data.
     *
     * @param url URL to parse query
     * @return KeyValue pair of queries. Empty if none.
     */
    public static Map<String, String> parseURL(String url) {
        Map<String, String> query_pairs = new HashMap<>();
        try {
            query_pairs = parseURL(new URL(url));
        } catch (MalformedURLException ex) {
            logger.log(Level.WARNING, null, ex);
        }
        return query_pairs;
    }

    /**
     * Fill up a FormElement by the provided value.
     * <br>
     * Normally it matches the NAME attribute with the KEY given in <b>data</b>
     * parameter sets its value. But another kind of match can be defined.
     * Suppose we have these elements-
     * <pre> &lt;input type="radio" name="rad2" value="2"&gt; </pre>
     *
     * If we put <code>(" #rad2", "2")</code> in <b>data<b>, the second input
     * will have its "checked" attribute altered to True and will look like-
     * <pre> &lt;input type="radio" name="rad2" value="2" checked="true"&gt;
     * </pre>
     *
     * @param form Form element to fill up.
     * @param data Values to fill up.
     */
    public static void fillUpForm(FormElement form, Map<String, String> data) {

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
