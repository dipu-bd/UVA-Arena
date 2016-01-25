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
package org.alulab.uvaarena.webapi;

import java.net.URI;
import java.net.URLDecoder;
import java.util.HashMap;
import org.alulab.uvaarena.util.Commons;
import org.apache.commons.codec.net.URLCodec;
import org.apache.http.client.utils.URIUtils;
import org.apache.http.client.utils.URLEncodedUtils;
import org.jsoup.nodes.FormElement;
import org.junit.Test;
import static org.junit.Assert.*;
import sun.net.util.URLUtil;

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
    public void testLoad() {
        System.out.println("----------- load ------------");
        RestClient instance = new RestClient();

        instance.load("https://uva.onlinejudge.org/");
        System.out.println("Received");

        FormElement form = instance.getFormById("mod_loginform");
        HashMap<String, String> data = new HashMap<>();
        data.put("username", "uarena");
        data.put("passwd", "uarena_2_vjudge");
        instance.fillUpForm(form, data);
        instance.submitForm(form);
        System.out.println("Submitted form");

        instance.load("https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25");
        System.out.println("Redirected to submit");

        FormElement submit = instance.getFormByAttributeValue("action",
                "index.php?option=com_onlinejudge&Itemid=25&page=save_submission");
        data.clear();
        data.put("code", "Hello World");
        data.put("localid", "100");
        data.put(" #language", "5");
        instance.fillUpForm(submit, data);
        instance.submitForm(submit);
        System.out.println("Submitted code");
        
        String mosmsg = Commons.splitQuery(instance.getURL()).get("mosmsg");
        System.out.println("Message: " + mosmsg);

        assertTrue(mosmsg.startsWith("Submission received with ID"));
    }

    /**
     * Test of submitForm method, of class RestClient.
     */
    @Test
    public void testSubmitForm() {
        System.out.println("submitForm");
        FormElement form = null;
        RestClient instance = new RestClient();
        instance.submitForm(form);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getFormById method, of class RestClient.
     */
    @Test
    public void testGetFormById() {
        System.out.println("getFormById");
        String id = "";
        RestClient instance = new RestClient();
        FormElement expResult = null;
        FormElement result = instance.getFormById(id);
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getFormByAttributeValue method, of class RestClient.
     */
    @Test
    public void testGetFormByAttributeValue() {
        System.out.println("getFormByAttributeValue");
        String attribute = "";
        String value = "";
        RestClient instance = new RestClient();
        FormElement expResult = null;
        FormElement result = instance.getFormByAttributeValue(attribute, value);
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of fillUpForm method, of class RestClient.
     */
    @Test
    public void testFillUpForm() {
        System.out.println("fillUpForm");
        FormElement form = null;
        HashMap<String, String> data = null;
        RestClient instance = new RestClient();
        instance.fillUpForm(form, data);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

}
