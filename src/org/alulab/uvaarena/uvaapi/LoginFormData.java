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

/**
 * Contains all the information about login form
 */
public interface LoginFormData {

    // page url to get the form
    public static String PAGE_URL = "https://uva.onlinejudge.org/";
    public static String REQUEST_URL = "https://uva.onlinejudge.org/index.php?option=com_comprofiler&task=login";

    // login form information
    // provide as many needed to find the specific form in the page
    public static String FORM_ID = "mod_loginform";
    public static String FORM_ACTION = "https://uva.onlinejudge.org/index.php?option=com_comprofiler&task=login";
    public static String FORM_METHOD = "post";
    public static String FORM_NAME = "";

    // provide names of the input fields we are interested on
    public static String USER_INPUT_NAME = "username";
    public static String PASS_INPUT_NAME = "passwd";

    // provide more data about connections
    public static String ACCEPT = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    public static String FORM_ENCTYPE = "application/x-www-form-urlencoded";
}
