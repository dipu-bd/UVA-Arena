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
package org.uvaarena.uhunt;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonArray;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import org.uvaarena.Core;
import org.uvaarena.util.FileHelper;
import org.alulab.web.DownloadManager;
import org.alulab.web.DownloadString;
import org.alulab.web.TaskMonitor;

/**
 *
 */
public class UHunt {

    public final int MAX_CONNECTION_TO_UVA = 1;
    public final int MAX_CONNECTION_TO_UHUNT = 4;
    public final String WEBHOST_UVA = "uva.onlinejudge.org";
    public final String WEBHOST_UHUNT = "uhunt.felix-halim.net";

    //
    // Private variables
    //
    private final Core mCore;
    private Problems mProblems;
    private final Map<String, Long> mUserNameToID;

    /**
     * Initialize a new UhuntAPI.
     *
     * @param core UVA Arena core.
     */
    public UHunt(Core core) {
        mCore = core;
        mProblems = new Problems();
        mUserNameToID = new HashMap<>();
        DownloadManager.setMaxPerRoute(WEBHOST_UVA, MAX_CONNECTION_TO_UVA);
        DownloadManager.setMaxPerRoute(WEBHOST_UHUNT, MAX_CONNECTION_TO_UHUNT);
    }

    /**
     * Creates a new Gson parser
     *
     * @return
     */
    public static Gson getGson() {
        return (new GsonBuilder()).create();
    }

    /**
     * Gets the user id from the user name. If user does not exist it returns 0.
     *
     * @param userName Name of the user.
     * @return
     */
    public long getUserID(String userName) {
        synchronized (mUserNameToID) {
            if (!mUserNameToID.containsKey(userName)) {
                downloadUserID(userName);
            }
            return mUserNameToID.getOrDefault(userName, 0L);
        }
    }

    /**
     * Gets the user name from the user id. If user does not exist it returns an
     * Empty string.
     *
     * @param userID ID of the user.
     * @return
     */
    public String getUserName(long userID) {
        synchronized (mUserNameToID) {
            for (Entry<String, Long> entry : mUserNameToID.entrySet()) {
                if (entry.getValue() == userID) {
                    return entry.getKey();
                }
            }
            return "";
        }
    }

    /**
     * Sets the id of an user
     *
     * @param userName Name of the user.
     * @param userID ID of the user.
     */
    public void setUserID(String userName, long userID) {
        synchronized (mUserNameToID) {
            mUserNameToID.put(userName, userID);
        }
    }

    /**
     * Sets the list of problems
     *
     * @param json
     */
    public void setProblems(String json) {
        mProblems = Problems.create(getGson().fromJson(json, JsonArray.class));
        Problems.save(mProblems, FileHelper.getProblemsFile());
    }

    //
    // URL to download from UHunt or UVA OJ.
    //
    /**
     * URL to convert username to user-id
     *
     * @param username Name of the user
     */
    final String USERNAME_TO_USERID = "http://uhunt.felix-halim.net/api/uname2uid/%s";
    /**
     * URL to get the problem list
     */
    final String PROBLEM_LIST = "http://uhunt.felix-halim.net/api/p";
    /**
     * Alternate URL to get the problem list
     */
    final String PROBLEM_LIST_ALT = "https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/problems.json";
    /**
     * URL to download category index file.
     */
    final String CATEGORY_INDEX = "https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/INDEX";
    /**
     * URL to download category data file.
     *
     * @param filename name of the category file
     */
    final String CATEGORY_FILES = "https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/%s";
    /**
     * URL to download submissions of an user.
     *
     * @param userid User id of the user.
     * @param startSID Submission id of start submission of the list. 0 to
     * download all submissions.
     */
    final String USER_SUBMISSIONS = "http://uhunt.felix-halim.net/api/subs-user/%s/%s";

    /**
     * Gets the user id of a user.
     *
     * @param userName Name of the user.
     */
    public void downloadUserID(String userName) {
        String url = String.format(USERNAME_TO_USERID, userName);
        DownloadManager.downloadString(url, new TaskMonitor<DownloadString>() {
            @Override
            public void downloadFinished(DownloadString task) {
                try {
                    if (task.isSuccess()) {
                        long res = Long.parseLong(task.getResult());
                        if (res > 0) {
                            setUserID(userName, res);
                        }
                    }
                } catch (Exception ex) {
                }
            }

            @Override
            public void statusChanged(DownloadString task) {
            }
        }).startDownload();
    }

    /**
     * Downloads the problem list.
     *
     * @param taskProgress Can be null. Gets called when progress changed.
     * @param taskFinished Can be null. Gets called when download finished.
     */
    public void downloadProblemList(Runnable taskProgress, Runnable taskFinished) {
        String url = String.format(PROBLEM_LIST);
        DownloadManager.downloadString(url, new TaskMonitor<DownloadString>() {
            @Override
            public void downloadFinished(DownloadString task) {
                try {
                    if (task.isSuccess()) {
                        setProblems(task.getResult());
                    }
                } catch (Exception ex) {
                }
            }

            @Override
            public void statusChanged(DownloadString task) {
            }
        }).startDownload();
    }

    /**
     * Downloads the latest judge status data.
     *
     * @param taskProgress Can be null. Gets called when progress changed.
     * @param taskFinished Can be null. Gets called when download finished.
     */
    public void downloadJudgeStatus(Runnable taskProgress, Runnable taskFinished) {
        
    }

    /**
     * Downloads the submission of a user from a specific submission. Pass 0 as
     * <code>startSID</code> to download all submissions.
     *
     * @param userName Name of the user.
     * @param startSID Submission id of start submission of the list. 0 to
     * download all submissions.
     * @param taskProgress Can be null. Gets called when progress changed.
     * @param taskFinished Can be null. Gets called when download finished.
     */
    public void downloadUserSubmission(String userName, int startSID, Runnable taskProgress, Runnable taskFinished) {

    }

}
