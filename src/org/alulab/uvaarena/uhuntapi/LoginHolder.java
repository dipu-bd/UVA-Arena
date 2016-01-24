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
package org.alulab.uvaarena.uhuntapi;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpCookie;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;
import org.alulab.uvaarena.webapi.DownloadManager;
import org.alulab.uvaarena.webapi.DownloadString;
import org.alulab.uvaarena.webapi.DownloadTask;
import org.alulab.uvaarena.webapi.TaskMonitor;
import org.apache.http.Header;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.fluent.Form;
import org.apache.http.client.fluent.Request;
import org.apache.http.client.fluent.Response;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;
import org.jsoup.nodes.Document;
import org.jsoup.parser.Parser;
import sun.net.www.protocol.http.HttpURLConnection;

/**
 *
 * @author Dipu
 */
public class LoginHolder extends DownloadString {

    private static final String URL = "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25";
    private static final String USERNAME_ID = "mod_login_username";
    private static final String PASSWORD_ID = "mod_login_remember";
    private static final String LOGIN_URL = "https://uva.onlinejudge.org/index.php?option=com_comprofiler&task=login";

    private String mUserName = "dipu_sust";
    private String mPassword = "sd.19.93";
    private Document mDocument = null;

    public LoginHolder() {
        super(URL);
        addTaskMonitor(new LoginTaskMonitor());
    }

    public void login() throws IOException {
        
        CloseableHttpClient client = DownloadManager.getHttpClient(); 
        HttpPost post = new HttpPost(LOGIN_URL);

        ArrayList<NameValuePair> urlParameters = new ArrayList<>();
        urlParameters.add(new BasicNameValuePair(USERNAME_ID, mUserName));
        urlParameters.add(new BasicNameValuePair(PASSWORD_ID, mPassword));
        urlParameters.add(new BasicNameValuePair("remember", "yes"));
        urlParameters.add(new BasicNameValuePair("Submit", "Login"));
        urlParameters.add(new BasicNameValuePair("loginfrom", "loginmodule"));
        urlParameters.add(new BasicNameValuePair("op2", "login"));
        
        post.setEntity(new UrlEncodedFormEntity(urlParameters));

        HttpResponse response = client.execute(post);
        System.out.println("Response Code : "
                + response.getStatusLine().getStatusCode());        

        BufferedReader rd = new BufferedReader(
                new InputStreamReader(response.getEntity().getContent()));

        String line;
        StringBuilder result = new StringBuilder();        
        while ((line = rd.readLine()) != null) {
            result.append(line);
        }        
        
        System.out.println("Result : ");
        System.out.println(result.toString());
    }

    private class LoginTaskMonitor implements TaskMonitor {

        @Override
        public void statusChanged(DownloadTask task) {

        }

        @Override
        public void downloadFinished(DownloadTask task) {
            // mDocument = Parser.parse(getResult(), URL);

        }

    }

    public String getUserName() {
        return mUserName;
    }

    public void setUserName(String userName) {
        mUserName = userName;
    }

    public String getPassword() {
        return mPassword;
    }

    public void setPassword(String password) {
        mPassword = password;
    }
}
