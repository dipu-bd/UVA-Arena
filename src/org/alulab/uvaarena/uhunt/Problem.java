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

import org.json.simple.JSONArray;

/**
 *
 */
public class Problem implements Comparable<Problem> {

    private final int MAX_LEVEL = 9;

    private long mID;
    private long mNumber;
    private String mTitle;
    private long mDACU;
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
     */
    public Problem() {
    }

    /**
     * Creates a new Problem object from given JSON array.
     *
     * @param jarr JSON Array representing the Problem.
     * @return
     */
    public static Problem parse(JSONArray jarr) {
        if (jarr == null) {
            return null;
        }
        Problem problem = new Problem();
        problem.setID((long) jarr.get(0));
        problem.setNumber((long) jarr.get(1));
        problem.setTitle((String) jarr.get(2));
        problem.setDACU((long) jarr.get(3));
        problem.setBestRuntime((long) jarr.get(4));
        problem.setBestMemory((long) jarr.get(5));
        problem.setNoVerdict((long) jarr.get(6));
        problem.setSubError((long) jarr.get(7));
        problem.setCantBeJudged((long) jarr.get(8));
        problem.setInQueue((long) jarr.get(9));
        problem.setComplieError((long) jarr.get(10));
        problem.setRestrictedFunc((long) jarr.get(11));
        problem.setRuntimeError((long) jarr.get(12));
        problem.setOutLimExceed((long) jarr.get(13));
        problem.setTimeLimExceed((long) jarr.get(14));
        problem.setMemLimExceed((long) jarr.get(15));
        problem.setWrongAnsCount((long) jarr.get(16));
        problem.setPresentError((long) jarr.get(17));
        problem.setAcceptedCount((long) jarr.get(18));
        problem.setRunTimeLimit((long) jarr.get(19));
        problem.setStatus((int) jarr.get(20));
        return problem;
    }

    @Override
    public String toString() {
        return String.format("{%d - %s}", mNumber, mTitle);
    }

    @Override
    public int compareTo(Problem t) {
        return (int) (getNumber() - t.getNumber());
    }

    /**
     * Gets the hardness level of the problem
     *
     * @return
     */
    public int getLevel() {
        if (mDACU <= 0) {
            return (1 + MAX_LEVEL);
        } else {
            return (1 + MAX_LEVEL) - Math.min(MAX_LEVEL, (int) Math.floor(Math.log(mDACU)));
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
    public long getID() {
        return mID;
    }

    /**
     * Problem Number
     *
     * @return
     */
    public long getNumber() {
        return mNumber;
    }

    /**
     * Problem Title
     *
     * @return
     */
    public String getTitle() {
        return mTitle;
    }

    /**
     * Number of Distinct Accepted User (DACU)
     *
     * @return
     */
    public long getDACU() {
        return mDACU;
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
    public void setID(long val) {
        mID = val;
    }

    /**
     * Problem Number
     *
     * @param val
     */
    public void setNumber(long val) {
        mNumber = val;
    }

    /**
     * Problem Title
     *
     * @param val
     */
    public void setTitle(String val) {
        mTitle = val;
    }

    /**
     * Number of Distinct Accepted User (DACU)
     *
     * @param val
     */
    public void setDACU(long val) {
        mDACU = val;
    }

    /**
     * Best Runtime of an Accepted Submission
     *
     * @param val
     */
    public void setBestRuntime(long val) {
        mBestRuntime = val;
    }

    /**
     * Best Memory used of an Accepted Submission
     *
     * @param val
     */
    public void setBestMemory(long val) {
        mBestMemory = val;
    }

    /**
     * Number of No Verdict Given (can be ignored)
     *
     * @param val
     */
    public void setNoVerdict(long val) {
        mNoVerdict = val;
    }

    /**
     * Number of Submission Error
     *
     * @param val
     */
    public void setSubError(long val) {
        mSubError = val;
    }

    /**
     * Number of Can't be Judged
     *
     * @param val
     */
    public void setCantBeJudged(long val) {
        mCantBeJudged = val;
    }

    /**
     * Number of In Queue
     *
     * @param val
     */
    public void setInQueue(long val) {
        mInQueue = val;
    }

    /**
     * Number of Compilation Error
     *
     * @param val
     */
    public void setComplieError(long val) {
        mComplieError = val;
    }

    /**
     * Number of Restricted Function
     *
     * @param val
     */
    public void setRestrictedFunc(long val) {
        mRestrictedFunc = val;
    }

    /**
     * Number of Runtime Error
     *
     * @param val
     */
    public void setRuntimeError(long val) {
        mRuntimeError = val;
    }

    /**
     * Number of Output Limit Exceeded
     *
     * @param val
     */
    public void setOutLimExceed(long val) {
        mOutLimExceed = val;
    }

    /**
     * Number of Time Limit Exceeded
     *
     * @param val
     */
    public void setTimeLimExceed(long val) {
        mTimeLimExceed = val;
    }

    /**
     * Number of Memory Limit Exceeded
     *
     * @param val
     */
    public void setMemLimExceed(long val) {
        mMemLimExceed = val;
    }

    /**
     * Number of Wrong Answer
     *
     * @param val
     */
    public void setWrongAnsCount(long val) {
        mWrongAnsCount = val;
    }

    /**
     * Number of Presentation Error
     *
     * @param val
     */
    public void setPresentError(long val) {
        mPresentError = val;
    }

    /**
     * Number of Accepted
     *
     * @param val
     */
    public void setAcceptedCount(long val) {
        mAcceptedCount = val;
    }

    /**
     * Time Limit (milliseconds)
     *
     * @param val
     */
    public void setRunTimeLimit(long val) {
        mRunTimeLimit = val;
    }

    /**
     * Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
     *
     * @param val
     */
    public void setStatus(int val) {
        switch (val) {
            case 0:
                setStatus(ProblemStatus.Unavailable);
                break;
            case 1:
                setStatus(ProblemStatus.Normal);
                break;
            case 2:
                setStatus(ProblemStatus.Special_Judge);
                break;
        }
    }

    /**
     * Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
     *
     * @param val
     */
    public void setStatus(ProblemStatus val) {
        mStatus = val;
    }

}
