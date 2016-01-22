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

import java.util.HashMap;
import java.util.Map.Entry; 
import org.alulab.uvaarena.Core;

/**
 *
 */
public class UHuntAPI {

    public final int MAX_CONNECTION_TO_UVA = 2;
    public final int MAX_CONNECTION_TO_UHUNT = 6;
    public final String WEBHOST_UVA = "uva.onlinejudge.org";
    public final String WEBHOST_UHUNT = "uhunt.felix-halim.net";
    
    //
    // Private variables
    //
    private final Core mCore;
    private final HashMap<String, Long> mUserNameToID;

    /**
     * Initialize a new UhuntAPI.
     *
     * @param core UVA Arena core.
     */
    public UHuntAPI(Core core) {
        mCore = core;
        mUserNameToID = new HashMap<>();
        core.downloader().setMaxPerRoute(WEBHOST_UVA, MAX_CONNECTION_TO_UVA);
        core.downloader().setMaxPerRoute(WEBHOST_UHUNT, MAX_CONNECTION_TO_UHUNT);
    }

    /**
     * Gets the user id from the user name. If user does not exist it returns 0.
     *
     * @param userName Name of the user.
     * @return
     */
    public long getUserID(String userName) {
        return mUserNameToID.getOrDefault(userName, 0L);
    }

    /**
     * Gets the user name from the user id. If user does not exist it returns an
     * Empty string.
     *
     * @param userID ID of the user.
     * @return
     */
    public String getUserName(long userID) {
        for (Entry<String, Long> entry : mUserNameToID.entrySet()) {
            if (entry.getValue() == userID) {
                return entry.getKey();
            }
        }
        return "";
    }

    //<editor-fold defaultstate="collapsed" desc="Methods to download data from web">
    /**
     * Gets the user id of a user.
     *
     * @param userName Name of the user.
     * @param taskProgress Can be null. Gets called when progress changed.
     * @param taskFinished Can be null. Gets called when download finished.
     */
    public void downloadUserID(String userName, Runnable taskProgress, Runnable taskFinished) {

    }

    /**
     * Downloads the problem list.
     *
     * @param taskProgress Can be null. Gets called when progress changed.
     * @param taskFinished Can be null. Gets called when download finished.
     */
    public void downloadProblemList(Runnable taskProgress, Runnable taskFinished) {

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

    //</editor-fold>    
}
