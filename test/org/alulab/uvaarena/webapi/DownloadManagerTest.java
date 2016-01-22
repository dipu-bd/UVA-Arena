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

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Dipu
 */
public class DownloadManagerTest {

    public DownloadManagerTest() {
    }

    @BeforeClass
    public static void setUpClass() {
    }

    @AfterClass
    public static void tearDownClass() {
    }

    @Before
    public void setUp() {
    }

    @After
    public void tearDown() {
    }

    @Test
    public void testSomeMethod() {

    }

    int finished = 0;

    @Test
    public void testDownloadString() throws InterruptedException {

        DownloadManager instance = new DownloadManager();
        String urls[] = new String[]{
            "https://www.google.com",
            "https://www.facebook.com",
            "http://uhunt.felix-halim.net",
            "http://uva.online-judge.org",
            "https://image.google.com",
            "http://uhunt.felix-halim.net/id/222248",
            "https://www.facebook.com/sdipu.fb",
            "http://uhunt.felix-halim.net/id/2222",
            "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=9",
            "http://video.google.com",
            "http://uhunt.felix-halim.net/id/326",
            "https://www.google.com",
            "https://www.facebook.com",
            "http://uhunt.felix-halim.net",
            "http://uva.online-judge.org",
            "https://image.google.com",
            "http://uhunt.felix-halim.net/id/222248",
            "https://www.facebook.com/sdipu.fb",
            "http://uhunt.felix-halim.net/id/2222",
            "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=9",
            "http://video.google.com",
            "http://uhunt.felix-halim.net/id/326",
            "https://www.google.com",
            "https://www.facebook.com",
            "http://uhunt.felix-halim.net",
            "http://uva.online-judge.org",
            "https://image.google.com",
            "http://uhunt.felix-halim.net/id/222248",
            "https://www.facebook.com/sdipu.fb",
            "http://uhunt.felix-halim.net/id/2222",
            "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=9",
            "http://video.google.com",
            "http://uhunt.felix-halim.net/id/326", "https://www.google.com",
            "https://www.facebook.com",
            "http://uhunt.felix-halim.net",
            "http://uva.online-judge.org",
            "https://image.google.com",
            "http://uhunt.felix-halim.net/id/222248",
            "https://www.facebook.com/sdipu.fb",
            "http://uhunt.felix-halim.net/id/2222",
            "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=9",
            "http://video.google.com",
            "http://uhunt.felix-halim.net/id/326",
            "https://www.google.com",
            "https://www.facebook.com",
            "http://uhunt.felix-halim.net",
            "http://uva.online-judge.org",
            "https://image.google.com",
            "http://uhunt.felix-halim.net/id/222248",
            "https://www.facebook.com/sdipu.fb",
            "http://uhunt.felix-halim.net/id/2222",
            "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=9",
            "http://video.google.com",
            "http://uhunt.felix-halim.net/id/326",
            "https://www.google.com",
            "https://www.facebook.com",
            "http://uhunt.felix-halim.net",
            "http://uva.online-judge.org",
            "https://image.google.com",
            "http://uhunt.felix-halim.net/id/222248",
            "https://www.facebook.com/sdipu.fb",
            "http://uhunt.felix-halim.net/id/2222",
            "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=9",
            "http://video.google.com",
            "http://uhunt.felix-halim.net/id/326"
        };

        finished = 0;
        System.out.println("DownloadString test");
        System.out.println("---------------------------------");

        TaskMonitor tm = new TaskMonitor() {

            @Override
            public void statusChanged(DownloadTask task) {
                System.out.println(task);
            }

            @Override
            public void downloadFinished(DownloadTask task) {
                finished++;
                long len = 0;
                if (task instanceof DownloadString) {
                    len = ((DownloadString) task).getResult().length();
                }
                System.out.printf("%s | [%d]\n", task, len);
            }
        };

        int task = 0;
        for (String url : urls) {
            ++task;
            DownloadString result = instance.downloadString(url);
            result.addTaskMonitor(tm);
            result.startDownload();
        }
        System.out.println("Running " + task + " tasks");
        
        while (finished < task) {
            Thread.sleep(100);
        }
        
        assertEquals(finished, task);
    }
}
