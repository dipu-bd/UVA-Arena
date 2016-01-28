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

import com.google.gson.JsonObject;
import java.io.Serializable;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.alulab.uvaarena.util.Commons;

/**
 *
 */
public class Submission implements Comparable<Submission>, Serializable {

    private static final Logger logger = Logger.getLogger(Submission.class.getName());

    private long mGlobalID;
    private long mId;
    private long mUserId;
    private long mProblemId;
    private Verdict mVerdict;
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
     * Converts the JSON object into a submission
     *
     * @param jsob JSON object to convert.
     * @return A new Submission object, NULL if failed.
     */
    public static Submission create(JsonObject jsob, Problems probList) {
        try {
            /*
             * id: 1453152921685,
             * type: "lastsubs",
             * msg: {
             *    sid: 16769928,
             *    uid: 795271,
             *    pid: 1011,
             *    ver: 70,
             *    lan: 3,
             *    run: 0,
             *    mem: 0,
             *    rank: -1,
             *    sbt: 1453990197,
             *    name: "Ishaq Ali",
             *    uname: "IshaqNiloy NSU"
             }
             */
            JsonObject msg = jsob.get("msg").getAsJsonObject();
            Submission submission = new Submission(0);
            //load values
            submission.setGlobalID(jsob.get("id").getAsLong());
            submission.setID(msg.get("sid").getAsLong());
            submission.setUserId(msg.get("uid").getAsLong());
            submission.setProblemId(msg.get("pid").getAsLong());
            submission.setVerdict(msg.get("ver").getAsInt());
            submission.setLanguage(msg.get("lan").getAsInt());
            submission.setRuntime(msg.get("run").getAsLong());
            submission.setMemory(msg.get("mem").getAsLong());
            submission.setRank(msg.get("rank").getAsLong());
            submission.setSubmissionTime(msg.get("sbt").getAsLong());
            submission.setFullName(msg.get("name").getAsString());
            submission.setUserName(msg.get("uname").getAsString());
            return submission;
        } catch (Exception ex) {
            logger.log(Level.SEVERE, null, ex);
            return null;
        }
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
     * @return the GlobalID
     */
    public long getGlobalID() {
        return mGlobalID;
    }

    /**
     * @return the ID
     */
    public long getID() {
        return mId;
    }

    /**
     * @return the UserId
     */
    public long getUserId() {
        return mUserId;
    }

    /**
     * @return the Problem
     */
    public long getProblemId() {
        return mProblemId;
    }

    /**
     * @return the Verdict
     */
    public Verdict verdict() {
        return mVerdict;
    }

    /**
     * @return the Language
     */
    public Language language() {
        return mLanguage;
    }

    /**
     * @return the Runtime
     */
    public long getRuntime() {
        return mRuntime;
    }

    /**
     * @return the Runtime in pretty format.
     */
    public String runtime() {
        return Commons.formatTimeSpan(mRank);
    }

    /**
     * @return the Memory
     */
    public long getMemory() {
        return mMemory;
    }

    /**
     * @return the Memory in pretty format
     */
    public String memory() {
        return Commons.formatByteLength(mMemory);
    }

    /**
     * @return the Rank
     */
    public long getRank() {
        return mRank;
    }

    /**
     * @return the SubmissionTime in UNIX timestamp
     */
    public long getSubmissionTimeUnix() {
        return mSubmissionTime;
    }

    /**
     * @return the FullName
     */
    public String getFullName() {
        return mFullName;
    }

    /**
     * @return the UserName
     */
    public String getUserName() {
        return mUserName;
    }

    /**
     * @param globalID the GlobalID to set
     */
    public void setGlobalID(long globalID) {
        mGlobalID = globalID;
    }

    /**
     * @param mID the ID to set
     */
    public void setID(long mID) {
        mId = mID;
    }

    /**
     * @param userId the UserId to set
     */
    public void setUserId(long userId) {
        mUserId = userId;
    }

    /**
     * @param problemId the ProblemId to set
     */
    public void setProblemId(long problemId) {
        mProblemId = problemId;
    }
 
    /**
     * @param verdict the Verdict to set
     */
    public void setVerdict(Verdict verdict) {
        mVerdict = verdict;
    }

    /**
     * @param verdict the Verdict to set
     */
    public void setVerdict(int verdict) {
        mVerdict = Verdict.fromInt(verdict);
    }

    /**
     * @param language the Language to set
     */
    public void setLanguage(Language language) {
        mLanguage = language;
    }

    /**
     * @param language the Language to set
     */
    public void setLanguage(int language) {
        mLanguage = Language.fromInt(language);
    }

    /**
     * @param runtime the Runtime to set
     */
    public void setRuntime(long runtime) {
        mRuntime = runtime;
    }

    /**
     * @param memory the Memory to set
     */
    public void setMemory(long memory) {
        mMemory = memory;
    }

    /**
     * @param rank the Rank to set
     */
    public void setRank(long rank) {
        mRank = rank;
    }

    /**
     * @param submissionTime the SubmissionTime to set
     */
    public void setSubmissionTime(long submissionTime) {
        mSubmissionTime = submissionTime;
    }

    /**
     * @param fullName the FullName to set
     */
    public void setFullName(String fullName) {
        mFullName = fullName;
    }

    /**
     * @param userName the UserName to set
     */
    public void setUserName(String userName) {
        mUserName = userName;
    }

    @Override
    public int compareTo(Submission t) {
        return (int) (getID() - t.getID());
    }

    @Override
    public String toString() {
        return String.format("%d: %s : %s(%s)", mId, mVerdict, mFullName, mUserName);
    }

}
