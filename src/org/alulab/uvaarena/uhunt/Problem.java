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

import com.google.gson.JsonArray;
import java.io.Serializable;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 */
public class Problem implements Comparable<Problem>, Serializable {

    private static final Logger logger = Logger.getLogger(Problem.class.getName());

    private final int MAX_LEVEL = 9;

    private long mId;
    private long mNumber;
    private String mTitle;
    private long mDacu;
    private long mBestRuntime;
    private long mBestMemory;
    private long mNoVerdict;
    private long mSubError;
    private long mCantBeJudged;
    private long mInQueue;
    private long mComplieError;
    private long mRestrictedFunc;
    private long mRuntimeError;
    private long mOutLimExceed;
    private long mTimeLimExceed;
    private long mMemLimExceed;
    private long mWrongAnsCount;
    private long mPresentError;
    private long mAcceptedCount;
    private long mRunTimeLimit;
    private ProblemStatus mStatus;

    /**
     * Creates a new Problem
     *
     * @param problemNumber
     */
    public Problem(long problemNumber) {
        mNumber = problemNumber;
    }

    /**
     * Creates a new Problem object from given JSON array.
     *
     * @param jarr JSON Array representing the Problem.
     * @return Problem object. NULL if parsing failed.
     */
    public static Problem create(JsonArray jarr) {
        try {
            Problem problem = new Problem(0);
            problem.setID(jarr.get(0).getAsLong());
            problem.setNumber(jarr.get(1).getAsLong());
            problem.setTitle((String) jarr.get(2).getAsString());
            problem.setDACU(jarr.get(3).getAsLong());
            problem.setBestRuntime(jarr.get(4).getAsLong());
            problem.setBestMemory(jarr.get(5).getAsLong());
            problem.setNoVerdict(jarr.get(6).getAsLong());
            problem.setSubError(jarr.get(7).getAsLong());
            problem.setCantBeJudged(jarr.get(8).getAsLong());
            problem.setInQueue(jarr.get(9).getAsLong());
            problem.setComplieError(jarr.get(10).getAsLong());
            problem.setRestrictedFunc(jarr.get(11).getAsLong());
            problem.setRuntimeError(jarr.get(12).getAsLong());
            problem.setOutLimExceed(jarr.get(13).getAsLong());
            problem.setTimeLimExceed(jarr.get(14).getAsLong());
            problem.setMemLimExceed(jarr.get(15).getAsLong());
            problem.setWrongAnsCount(jarr.get(16).getAsLong());
            problem.setPresentError(jarr.get(17).getAsLong());
            problem.setAcceptedCount(jarr.get(18).getAsLong());
            problem.setRunTimeLimit(jarr.get(19).getAsLong());
            problem.setStatus(jarr.get(20).getAsInt());
            return problem;
        } catch (NullPointerException | NumberFormatException | ClassCastException ex) {
            logger.log(Level.SEVERE, null, ex);
            return null;
        }
    }

    @Override
    public String toString() {
        return String.format("{%d - %s}", mNumber, mTitle);
    }

    @Override
    public int compareTo(Problem t) {
        return (int) (number() - t.number());
    }

    /**
     * Gets the hardness level of the problem
     *
     * @return
     */
    public int getLevel() {
        if (mDacu <= 0) {
            return (1 + MAX_LEVEL);
        } else {
            return (1 + MAX_LEVEL) - Math.min(MAX_LEVEL, (int) Math.floor(Math.log(mDacu)));
        }
    }

    /**
     * Gets the total number of submissions on this problem.
     *
     * @return
     */
    public long getTotalSubmission() {
        return mAcceptedCount + mWrongAnsCount + mCantBeJudged + mComplieError
                + mMemLimExceed + mTimeLimExceed + mOutLimExceed + mNoVerdict
                + mPresentError + mRestrictedFunc + mRuntimeError + mSubError;
    }

    /**
     * Gets the accepted percentage on this problem
     *
     * @return
     */
    public double getAcceptedPercentage() {
        if (getTotalSubmission() == 0) {
            return 0.0;
        } else {
            return 100.0 * (double) mAcceptedCount / (double) getTotalSubmission();
        }
    }

    /**
     * Problem ID
     *
     * @return
     */
    public long ID() {
        return mId;
    }

    /**
     * Problem Number
     *
     * @return
     */
    public long number() {
        return mNumber;
    }

    /**
     * Problem Title
     *
     * @return
     */
    public String title() {
        return mTitle;
    }

    /**
     * Number of Distinct Accepted User (DACU)
     *
     * @return
     */
    public long dacu() {
        return mDacu;
    }

    /**
     * Best Runtime of an Accepted Submission
     *
     * @return
     */
    public long getBestRuntime() {
        return mBestRuntime;
    }

    /**
     * Best Memory used of an Accepted Submission
     *
     * @return
     */
    public long getBestMemory() {
        return mBestMemory;
    }

    /**
     * Number of No Verdict Given (can be ignored)
     *
     * @return
     */
    public long getNoVerdict() {
        return mNoVerdict;
    }

    /**
     * Number of Submission Error
     *
     * @return
     */
    public long getSubError() {
        return mSubError;
    }

    /**
     * Number of Can't be Judged
     *
     * @return
     */
    public long getCantBeJudged() {
        return mCantBeJudged;
    }

    /**
     * Number of In Queue
     *
     * @return
     */
    public long getInQueue() {
        return mInQueue;
    }

    /**
     * Number of Compilation Error
     *
     * @return
     */
    public long getComplieError() {
        return mComplieError;
    }

    /**
     * Number of Restricted Function
     *
     * @return
     */
    public long getRestrictedFunc() {
        return mRestrictedFunc;
    }

    /**
     * Number of Runtime Error
     *
     * @return
     */
    public long getRuntimeError() {
        return mRuntimeError;
    }

    /**
     * Number of Output Limit Exceeded
     *
     * @return
     */
    public long getOutLimExceed() {
        return mOutLimExceed;
    }

    /**
     * Number of Time Limit Exceeded
     *
     * @return
     */
    public long getTimeLimExceed() {
        return mTimeLimExceed;
    }

    /**
     * Number of Memory Limit Exceeded
     *
     * @return
     */
    public long getMemLimExceed() {
        return mMemLimExceed;
    }

    /**
     * Number of Wrong Answer
     *
     * @return
     */
    public long getWrongAnswer() {
        return mWrongAnsCount;
    }

    /**
     * Number of Presentation Error
     *
     * @return
     */
    public long getPresentError() {
        return mPresentError;
    }

    /**
     * Number of Accepted
     *
     * @return
     */
    public long getAcceptedCount() {
        return mAcceptedCount;
    }

    /**
     * Time Limit (milliseconds)
     *
     * @return
     */
    public long getRunTimeLimit() {
        return mRunTimeLimit;
    }

    /**
     * Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
     *
     * @return
     */
    public ProblemStatus getStatus() {
        return mStatus;
    }

    /**
     * Problem ID
     *
     * @param val
     */
    protected void setID(long val) {
        mId = val;
    }

    /**
     * Problem Number
     *
     * @param val
     */
    protected void setNumber(long val) {
        mNumber = val;
    }

    /**
     * Problem Title
     *
     * @param val
     */
    protected void setTitle(String val) {
        mTitle = val;
    }

    /**
     * Number of Distinct Accepted User (DACU)
     *
     * @param val
     */
    protected void setDACU(long val) {
        mDacu = val;
    }

    /**
     * Best Runtime of an Accepted Submission
     *
     * @param val
     */
    protected void setBestRuntime(long val) {
        mBestRuntime = val;
    }

    /**
     * Best Memory used of an Accepted Submission
     *
     * @param val
     */
    protected void setBestMemory(long val) {
        mBestMemory = val;
    }

    /**
     * Number of No Verdict Given (can be ignored)
     *
     * @param val
     */
    protected void setNoVerdict(long val) {
        mNoVerdict = val;
    }

    /**
     * Number of Submission Error
     *
     * @param val
     */
    protected void setSubError(long val) {
        mSubError = val;
    }

    /**
     * Number of Can't be Judged
     *
     * @param val
     */
    protected void setCantBeJudged(long val) {
        mCantBeJudged = val;
    }

    /**
     * Number of In Queue
     *
     * @param val
     */
    protected void setInQueue(long val) {
        mInQueue = val;
    }

    /**
     * Number of Compilation Error
     *
     * @param val
     */
    protected void setComplieError(long val) {
        mComplieError = val;
    }

    /**
     * Number of Restricted Function
     *
     * @param val
     */
    protected void setRestrictedFunc(long val) {
        mRestrictedFunc = val;
    }

    /**
     * Number of Runtime Error
     *
     * @param val
     */
    protected void setRuntimeError(long val) {
        mRuntimeError = val;
    }

    /**
     * Number of Output Limit Exceeded
     *
     * @param val
     */
    protected void setOutLimExceed(long val) {
        mOutLimExceed = val;
    }

    /**
     * Number of Time Limit Exceeded
     *
     * @param val
     */
    protected void setTimeLimExceed(long val) {
        mTimeLimExceed = val;
    }

    /**
     * Number of Memory Limit Exceeded
     *
     * @param val
     */
    protected void setMemLimExceed(long val) {
        mMemLimExceed = val;
    }

    /**
     * Number of Wrong Answer
     *
     * @param val
     */
    protected void setWrongAnsCount(long val) {
        mWrongAnsCount = val;
    }

    /**
     * Number of Presentation Error
     *
     * @param val
     */
    protected void setPresentError(long val) {
        mPresentError = val;
    }

    /**
     * Number of Accepted
     *
     * @param val
     */
    protected void setAcceptedCount(long val) {
        mAcceptedCount = val;
    }

    /**
     * Time Limit (milliseconds)
     *
     * @param val
     */
    protected void setRunTimeLimit(long val) {
        mRunTimeLimit = val;
    }

    /**
     * Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
     *
     * @param val
     */
    protected void setStatus(int val) {
        mStatus = ProblemStatus.fromInt(val);
    }

    /**
     * Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
     *
     * @param val
     */
    protected void setStatus(ProblemStatus val) {
        mStatus = val;
    }

}
