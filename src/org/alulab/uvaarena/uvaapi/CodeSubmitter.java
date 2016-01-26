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
import javafx.beans.property.BooleanProperty;
import javafx.beans.property.LongProperty;
import javafx.beans.property.ReadOnlyBooleanProperty;
import javafx.beans.property.ReadOnlyLongProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.beans.property.SimpleLongProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;
import org.alulab.uvaarena.webapi.RestClient;
import org.jsoup.nodes.FormElement;

/**
 *
 * @author Dipu
 */
public final class CodeSubmitter extends java.util.Observable {

    private static final Logger logger = Logger.getLogger(CodeSubmitter.class.getName());

    public static final String DEFAULT_USER_NAME = "uarena";
    public static final String DEFAULT_PASSWORD = "uarena_2_vjudge";
    public static final String SUBMIT_URL = "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25";

    private WorkState mState;
    private Exception mError;
    private final RestClient mRestClient;

    private final BooleanProperty mBusy;
    private final StringProperty mUserName;
    private final StringProperty mPassword;
    private final StringProperty mProblem;
    private final StringProperty mCode;
    private final StringProperty mLanguage;
    private final LongProperty mSubmissionID;

    public CodeSubmitter() {
        mRestClient = new RestClient();
        mBusy = new SimpleBooleanProperty(false);
        mUserName = new SimpleStringProperty(DEFAULT_USER_NAME);
        mPassword = new SimpleStringProperty(DEFAULT_PASSWORD);
        mProblem = new SimpleStringProperty("");
        mCode = new SimpleStringProperty("");
        mLanguage = new SimpleStringProperty("5");
        mSubmissionID = new SimpleLongProperty(0);
    }

    /**
     * Run the work thread to make login or submit.
     *
     * @param canSubmit True if submit request to be sent. False if only login
     * is necessary.
     */
    private void doWork(boolean canSubmit) {
        if (!mBusy.get()) {
            mBusy.set(true);
            (new WorkThread(canSubmit)).start();
        }
    }

    /**
     * To run the work in separate thread.
     */
    private final class WorkThread extends Thread {

        volatile boolean mCanSubmit;

        public WorkThread(boolean canSubmit) {
            mState = WorkState.UNKNOWN;
            mCanSubmit = canSubmit;
        }

        @Override
        public void run() {
            mError = null;
            mSubmissionID.set(0);
            mState = WorkState.STARTED;
            try {
                // make login
                loadSubmitPage();
                makeLogin();
                mState = WorkState.LOGINED;

                if (mCanSubmit) {
                    // make submission
                    loadSubmitPage();
                    makeSubmit();
                    mState = WorkState.SUBMITED;

                    // validate submission id
                    long sid = getSubmssionIdFromResult();
                    mSubmissionID.set(sid);
                    if (sid <= 0) {
                        throw new Exception(getMessage());
                    } else {
                        mState = WorkState.CONFIRMED;
                    }
                }
            } catch (Exception ex) {
                logger.log(Level.SEVERE, null, ex);
                mError = ex;
                mState = WorkState.ERROR;
            }
            mBusy.set(false);

            // notify all observers     
            setChanged();
            notifyObservers(mState);
        }
    }

    /**
     * Gets the login form. Assumed that a page is loaded where login form can
     * be found. Returns null if not found.
     */
    private FormElement getLoginForm() {
        return mRestClient.getFormById("mod_loginform");
    }

    /**
     * Gets the submit page. Assumed that a page is loaded where submit form can
     * be found. Returns null if not found.
     */
    private FormElement getSubmitForm() {
        return mRestClient.getFormByAttributeValue("action",
                "index.php?option=com_onlinejudge&Itemid=25&page=save_submission");
    }

    /**
     * Loads the submit page.
     */
    private synchronized void loadSubmitPage() {
        if (!SUBMIT_URL.equals(mRestClient.getFullUrl())) {
            logger.log(Level.OFF, "Loading submit page...");
            mRestClient.load(SUBMIT_URL);
            logger.log(Level.OFF, "Submit page loaded.");
        }
    }

    /**
     * Log into the server. Assumed that a page is loaded where login form can
     * be found.
     */
    private synchronized void makeLogin() throws Exception {
        FormElement form = getLoginForm();
        if (form != null) {
            logger.log(Level.OFF, "Loggin in...");
            HashMap<String, String> data = new HashMap<>();
            data.put("username", getUserName());
            data.put("passwd", getPassword());
            RestClient.fillUpForm(form, data);
            mRestClient.submitForm(form);
            // recheck if logged in
            if (getLoginForm() == null) {
                logger.log(Level.OFF, "Successfully logged in.");
            } else {
                logger.log(Level.INFO, "Could not log into the server. Requesting url: {0}", mRestClient.getURL());
                throw new Exception("Login failed.");
            }
        } else {
            logger.log(Level.OFF, "Already logged in.");
        }
    }

    /**
     * Make a submit request. Assumed user has already logged in at this point
     * and a page is loaded where submit form can be found.
     */
    private synchronized void makeSubmit() throws Exception {
        FormElement form = getSubmitForm();
        if (form != null) {
            logger.log(Level.OFF, "Submitting code...");
            HashMap<String, String> data = new HashMap<>();
            data.put("code", getCode());
            data.put("localid", getProblem());
            data.put(" #language", getLanguage());
            RestClient.fillUpForm(form, data);
            mRestClient.submitForm(form);
        } else {
            logger.log(Level.INFO, "Could not find submit form. Requesting url: {0}", mRestClient.getURL());
            throw new Exception("Submission failed.");
        }
        logger.log(Level.INFO, getMessage());
    }

    /**
     * get the confirmation message received from the server
     */
    private String getMessage() {
        return mRestClient.getQueryValue("mosmsg");
    }

    /**
     * will return 0 on failure
     */
    private long getSubmssionIdFromResult() {
        long result = 0;
        try {
            String msg = "Submission received with ID ";
            if (getMessage().startsWith(msg)) {
                String id = getMessage().substring(msg.length()).trim();
                result = Long.parseLong(id);
            }
        } catch (Exception ex) {
            result = 0;
        }
        return result;
    }

    /**
     * Log into the UVA server using the given credentials.
     *
     * @param username Username
     * @param password Password
     */
    public void login(String username, String password) {
        if (!mBusy.get()) {
            mRestClient.reset();
            setUserName(username);
            setPassword(password);
            doWork(false);
        }
    }

    /**
     * Submit the given code using the given credentials
     *
     * @param username Username
     * @param password Password
     * @param problemNumber Problem number
     * @param language Language id (1 = AnsiC, 2 = Java, 3 = C++, 4 = Pascal, 5
     * = C++11)
     * @param code Code to submit.
     */
    public void submit(String username, String password, String problemNumber, String language, String code) {
        if (!mBusy.get()) {
            if (!(getUserName().equals(username)
                    && getPassword().equals(password))) {
                mRestClient.reset();
                setUserName(username);
                setPassword(password);
            }
            setProblem(problemNumber);
            setLanguage(language);
            setCode(code);
            doWork(true);
        }
    }

    /**
     * Submit the given code using the credentials that has been already set.
     *
     * @param problemNumber Problem number
     * @param language Language id (1 = AnsiC, 2 = Java, 3 = C++, 4 = Pascal, 5
     * = C++11)
     * @param code Code to submit.
     */
    @Deprecated
    public void submit(String problemNumber, String language, String code) {
        submit(mUserName.get(), mPassword.get(), problemNumber, language, code);
    }

    /**
     * Resets the REST client and clears all cookies.
     */
    public void reset() {
        if (!mBusy.get()) {
            mRestClient.reset();
        }
    }

    /**
     * Gets the username property.
     *
     * @return
     */
    public StringProperty usernameProperty() {
        return mUserName;
    }

    /**
     * Gets the password property.
     *
     * @return
     */
    public StringProperty passwordProperty() {
        return mPassword;
    }

    /**
     * Gets the problem number property.
     *
     * @return
     */
    public StringProperty problemProperty() {
        return mProblem;
    }

    /**
     * Gets the language property.
     *
     * @return
     */
    public StringProperty languageProperty() {
        return mLanguage;
    }

    /**
     * Gets the code property.
     *
     * @return
     */
    public StringProperty codeProperty() {
        return mCode;
    }

    /**
     * Gets the submission id property.
     *
     * @return
     */
    public ReadOnlyLongProperty submissionIdPropery() {
        return ReadOnlyLongProperty.readOnlyLongProperty(mSubmissionID);
    }

    /**
     * Gets the busy property.
     *
     * @return
     */
    public ReadOnlyBooleanProperty busyPropery() {
        return ReadOnlyBooleanProperty.readOnlyBooleanProperty(mBusy);
    }

    /**
     * Gets the user name.
     *
     * @return
     */
    public String getUserName() {
        return mUserName.get();
    }

    /**
     * Sets the user name.
     *
     * @param userName
     */
    public void setUserName(String userName) {
        mUserName.set(userName);
    }

    /**
     * Gets the password.
     *
     * @return
     */
    public String getPassword() {
        return mPassword.get();
    }

    /**
     * Sets the password.
     *
     * @param password
     */
    public void setPassword(String password) {
        mPassword.set(password);
    }

    /**
     * Gets the problem number.
     *
     * @return
     */
    public String getProblem() {
        return mProblem.get();
    }

    /**
     * Sets the problem number.
     *
     * @param number
     */
    public void setProblem(String number) {
        mProblem.set(number);
    }

    /**
     * Gets the language id.
     *
     * @return
     */
    public String getLanguage() {
        return mLanguage.get();
    }

    /**
     * Sets the language id.
     *
     * @param languageId
     */
    public void setLanguage(String languageId) {
        mLanguage.set(languageId);
    }

    /**
     * Gets the code to submit.
     *
     * @return
     */
    public String getCode() {
        return mCode.get();
    }

    /**
     * Sets the code to submit.
     *
     * @param code
     */
    public void setCode(String code) {
        mCode.set(code);
    }

    /**
     * Gets the last submission id.
     *
     * @return
     */
    public long getSubmissionID() {
        return mSubmissionID.get();
    }

    /**
     * Gets the state of the working thread
     *
     * @return
     */
    public WorkState getWorkState() {
        return mState;
    }

    /**
     * Gets the error if any
     *
     * @return
     */
    public Exception getError() {
        return mError;
    }

    /**
     * Gets whether this instance is busy or not.
     *
     * @return
     */
    public boolean isBusy() {
        return mBusy.get();
    }

    /**
     * Represents the state of the work thread
     */
    public enum WorkState {

        UNKNOWN,
        STARTED,
        LOGINED,
        SUBMITED,
        CONFIRMED,
        ERROR
    }
}
