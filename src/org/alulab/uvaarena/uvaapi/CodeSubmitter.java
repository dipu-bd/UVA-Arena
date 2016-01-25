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
package org.alulab.uvaarena.uvaapi;

import java.util.HashMap;
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;
import org.alulab.uvaarena.webapi.RestClient;
import org.jsoup.nodes.FormElement;

/**
 *
 * @author Dipu
 */
public final class CodeSubmitter {

    private static final Logger logger = Logger.getLogger(CodeSubmitter.class.getName());

    private static final String DEFAULT_USER_NAME = "uarena";
    private static final String DEFAULT_PASSWORD = "uarena_2_vjudge";
    private static final String SUBMIT_URL = "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25";

    private StringProperty mUserName = new SimpleStringProperty(DEFAULT_USER_NAME);
    private StringProperty mPassword = new SimpleStringProperty(DEFAULT_PASSWORD);
    private StringProperty mProblem = new SimpleStringProperty("0");
    private StringProperty mCode = new SimpleStringProperty("");
    private StringProperty mLanguage = new SimpleStringProperty("0");

    private final RestClient mRestClient;

    public CodeSubmitter() {
        mRestClient = new RestClient();
    }

    public void submit() {
        try {
            loadSubmitPage();
            makeLogin();
            loadSubmitPage();
            makeSubmit();
        } catch (Exception ex) {
            logger.log(Level.SEVERE, null, ex);
        }

        String submissionId = getSubmissionID();
        if (submissionId.isEmpty()) {
            logger.log(Level.SEVERE, "Submission did not reach the server");
        } else {
            logger.log(Level.INFO, "Last submission id: {0}", submissionId);
        }
    }

    private FormElement getLoginForm() {
        return mRestClient.getFormById("mod_loginform");
    }

    private FormElement getSubmitForm() {
        return mRestClient.getFormByAttributeValue("action",
                "index.php?option=com_onlinejudge&Itemid=25&page=save_submission");
    }

    private void loadSubmitPage() {
        logger.log(Level.INFO, "Loading submit page...");
        mRestClient.load(SUBMIT_URL);
        logger.log(Level.INFO, "Submit page loaded.");
    }

    private void makeLogin() throws Exception {
        logger.log(Level.INFO, "Not logged in. Loggin in...");
        FormElement form = getLoginForm();
        if (form != null) {
            HashMap<String, String> data = new HashMap<>();
            data.put("username", mUserName.get());
            data.put("passwd", mPassword.get());
            RestClient.fillUpForm(form, data);
            mRestClient.submitForm(form);
        } else {
            logger.log(Level.SEVERE, "Could not find login form. Requesting url: {0}", mRestClient.getURL());
            throw new Exception("Could not find login form");
        }
        // recheck if logged in
        if (getLoginForm() == null) {
            logger.log(Level.INFO, "Successfully logged in.");
        } else {
            logger.log(Level.SEVERE, "Could not log into the server. Requesting url: {0}", mRestClient.getURL());
            throw new Exception("Could not log in");
        }
    }

    private void makeSubmit() throws Exception {
        FormElement form = getSubmitForm();
        if (form != null) {
            logger.log(Level.INFO, "Submitting code...");
            HashMap<String, String> data = new HashMap<>();
            data.put("code", mCode.get());
            data.put("localid", mProblem.get());
            data.put(" #language", mLanguage.get());
            RestClient.fillUpForm(form, data);
            mRestClient.submitForm(form);
        } else {
            logger.log(Level.SEVERE, "Could not find submit form. Requesting url: {0}", mRestClient.getURL());
            throw new Exception("Submission failed");
        }
        logger.log(Level.INFO, getMessage());
    }

    private String getMessage() {
        return mRestClient.getQueryValue("mosmsg");
    }

    private String getSubmissionID() {
        try {
            String msg = "Submission received with ID ";
            if (getMessage().startsWith(msg)) {
                return getMessage().substring(msg.length()).trim();
            } else {
                return "";
            }
        } catch (Exception ex) {
            return "";
        }
    }

    public StringProperty userNameProperty() {
        return mUserName;
    }

    public StringProperty passwordProperty() {
        return mPassword;
    }

    public String getUserName() {
        return mUserName.get();
    }

    public void setUserName(String userName) {
        mUserName.set(userName);
    }

    public String getPassword() {
        return mPassword.get();
    }

    public void setPassword(String password) {
        mPassword.set(password);
    }

}
