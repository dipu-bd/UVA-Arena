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

import java.util.Observable;
import java.util.Observer;
import org.junit.Test;

/**
 *
 */
public class CodeSubmitterTest {

    public CodeSubmitterTest() {
    }

    private final Observer observer = (Observable o, Object o1) -> {
        CodeSubmitter instance = (CodeSubmitter) o;
        System.out.println(o1);
        if (o1 == CodeSubmitter.WorkState.CONFIRMED) {
            System.out.println("Id = " + instance.getSubmissionID());
        }
        if (o1 == CodeSubmitter.WorkState.ERROR) {
            System.out.println(instance.getError());
        }
    };

    /**
     * Test of login method, of class CodeSubmitter.
     */
    @Test
    public void testLogin() throws InterruptedException {
        System.out.println("-----Test Login-----");

        CodeSubmitter instance = new CodeSubmitter();
        instance.addObserver(observer);

        instance.login("uarena", "uarena_2_vjudge");
        while (instance.isBusy()) {
            Thread.sleep(100);
        }
    }

    /**
     * Test of submit method, of class CodeSubmitter.
     */
    @Test
    public void testSubmit_3args() throws InterruptedException {
        System.out.println("-----Test Submit 3args-----");

        CodeSubmitter instance = new CodeSubmitter();
        instance.addObserver(observer);

        instance.login("uarena", "uarena_2_vjudge");
        while (instance.isBusy()) {
            Thread.sleep(100);
        }

        instance.submit("100", "", "Hello World!");
        while (instance.isBusy()) {
            Thread.sleep(100);
        }

        instance.submit("100", "4", "Hello World!");
        while (instance.isBusy()) {
            Thread.sleep(100);
        }
    }

    /**
     * Test of submit method, of class CodeSubmitter.
     */
    @Test
    public void testSubmit_5args() throws InterruptedException {
        System.out.println("-----Test Submit 5args-----");

        CodeSubmitter instance = new CodeSubmitter();
        instance.addObserver(observer);

        instance.submit("uarena", "uarena_2_vjudge", "100", "4", "");
        while (instance.isBusy()) {
            Thread.sleep(100);
        }
    }

}
