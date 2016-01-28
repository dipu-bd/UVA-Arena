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
package org.alulab.web;

import org.alulab.web.RestClient;
import java.util.HashMap; 
import org.jsoup.nodes.FormElement;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Dipu
 */
public class RestClientTest {

    public RestClientTest() {
    }

    /**
     * Test of load method, of class RestClient.
     */
    @Test
    public void testLoginAndSubmit() {
        System.out.println("----------- load ------------");
        RestClient instance = new RestClient();

        instance.load("https://uva.onlinejudge.org/");
        System.out.println("Received");

        FormElement form = instance.getFormById("mod_loginform");
        HashMap<String, String> data = new HashMap<>();
        data.put("username", "uarena");
        data.put("passwd", "uarena_2_vjudge");
        RestClient.fillUpForm(form, data);
        instance.submitForm(form);
        System.out.println("Submitted form");        

        instance.load("https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25");
        System.out.println("Redirected to submit");

        FormElement submit = instance.getFormByAttributeValue("action",
                "index.php?option=com_onlinejudge&Itemid=25&page=save_submission");
        data.clear();
        data.put("code", "Hello World");
        data.put("localid", "100");
        //data.put(" #language", "5");
        RestClient.fillUpForm(submit, data);
        instance.submitForm(submit);
        System.out.println("Submitted code");

        String mosmsg = instance.getQueryValue("mosmsg");
        System.out.println("Message: " + mosmsg);

        assertTrue(mosmsg.startsWith("Submission received with ID"));
    }

}
