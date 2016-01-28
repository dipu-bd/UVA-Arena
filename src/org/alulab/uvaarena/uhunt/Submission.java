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
package org.alulab.uvaarena.uhunt;

import java.io.Serializable;
import java.util.Date;

/**
 *
 * @author Dipu
 */
public class Submission implements Comparable<Submission>, Serializable {

    private long mGlobalID;
    private long mId;
    private long mUserId;
    private long mProblemId;
    private long mProblemNumber;
    private long mProblemTitle;
    private long mVerdict;
    private Language mLanguage;
    private long mRuntime;
    private long mMemory;
    private long mRank;
    private long mSubmissionTime;
    private String mFullName;
    private String mUserName;

    public Submission(long id) {
        mId = id;
        
    }

    /**
     * Gets the submission time in date
     *
     * @return
     */
    public Date getSubmissionTime() {
        return new Date(mSubmissionTime * 1000);
    }

    /**
     * @return the mGlobalID
     */
    public long getGlobalID() {
        return mGlobalID;
    }

    /**
     * @return the mID
     */
    public long getID() {
        return mId;
    }

    /**
     * @return the mUserId
     */
    public long getUserId() {
        return mUserId;
    }

    /**
     * @return the mProblemId
     */
    public long getProblemId() {
        return mProblemId;
    }

    /**
     * @return the mProblemNumber
     */
    public long getProblemNumber() {
        return mProblemNumber;
    }

    /**
     * @return the mProblemTitle
     */
    public long getProblemTitle() {
        return mProblemTitle;
    }

    /**
     * @return the mVerdict
     */
    public long getVerdict() {
        return mVerdict;
    }

    /**
     * @return the mLanguage
     */
    public Language getLanguage() {
        return mLanguage;
    }

    /**
     * @return the mRuntime
     */
    public long getRuntime() {
        return mRuntime;
    }

    /**
     * @return the mMemory
     */
    public long getMemory() {
        return mMemory;
    }

    /**
     * @return the mRank
     */
    public long getRank() {
        return mRank;
    }

    /**
     * @return the mSubmissionTime in UNIX timestamp
     */
    public long getSubmissionTimeUnix() {
        return mSubmissionTime;
    }

    /**
     * @return the mFullName
     */
    public String getFullName() {
        return mFullName;
    }

    /**
     * @return the mUserName
     */
    public String getUserName() {
        return mUserName;
    }

    /**
     * @param globalID the mGlobalID to set
     */
    public void setGlobalID(long globalID) {
        this.mGlobalID = globalID;
    }

    /**
     * @param mID the mID to set
     */
    public void setID(long mID) {
        this.mId = mID;
    }

    /**
     * @param userId the mUserId to set
     */
    public void setUserId(long userId) {
        this.mUserId = userId;
    }

    /**
     * @param problemId the mProblemId to set
     */
    public void setProblemId(long problemId) {
        this.mProblemId = problemId;
    }

    /**
     * @param problemNumber the mProblemNumber to set
     */
    public void setProblemNumber(long problemNumber) {
        this.mProblemNumber = problemNumber;
    }

    /**
     * @param problemTitle the mProblemTitle to set
     */
    public void setProblemTitle(long problemTitle) {
        this.mProblemTitle = problemTitle;
    }

    /**
     * @param verdict the mVerdict to set
     */
    public void setVerdict(long verdict) {
        this.mVerdict = verdict;
    }

    /**
     * @param language the mLanguage to set
     */
    public void setLanguage(Language language) {
        this.mLanguage = language;
    }

    /**
     * @param runtime the mRuntime to set
     */
    public void setRuntime(long runtime) {
        this.mRuntime = runtime;
    }

    /**
     * @param memory the mMemory to set
     */
    public void setMemory(long memory) {
        this.mMemory = memory;
    }

    /**
     * @param rank the mRank to set
     */
    public void setRank(long rank) {
        this.mRank = rank;
    }

    /**
     * @param submissionTime the mSubmissionTime to set
     */
    public void setSubmissionTime(long submissionTime) {
        this.mSubmissionTime = submissionTime;
    }

    /**
     * @param fullName the mFullName to set
     */
    public void setFullName(String fullName) {
        this.mFullName = fullName;
    }

    /**
     * @param userName the mUserName to set
     */
    public void setUserName(String userName) {
        this.mUserName = userName;
    }

    @Override
    public int compareTo(Submission t) {
        return (int) (getID() - t.getID());
    }

    @Override
    public String toString() {
        return String.format("{%d: %d %s}", mUserName, mProblemNumber, mVerdict);
    }

}
