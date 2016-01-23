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

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.List;
import org.apache.commons.io.IOUtils;
import org.apache.http.Header;
import org.apache.http.HttpEntity;
import org.apache.http.client.fluent.Request;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpUriRequest;
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
    public void testDownlaodClient() {
        System.out.println("Response Header Test");
        long startTime = System.currentTimeMillis();

        DownloadManager instance = new DownloadManager();
        // process request
        HttpUriRequest req = new HttpGet("https://dl.dropboxusercontent.com/content_link/WvoJNFNxV9gQceUPFeXvh1vCaU8XxIrthK1Vm9c8f87jibAsAxmW7rBbzgZvSN68/file?dl=1");
        System.out.println("------------ Request -------------");
        System.out.println(req.getURI().toString());
        if (req.getProtocolVersion() != null) {
            System.out.println("Protocol : " + req.getProtocolVersion().toString());
        }
        if (req.getAllHeaders() != null) {
            for (Header head : req.getAllHeaders()) {
                System.out.println(head);
            }
        }
        // get response   
        try (CloseableHttpResponse response = instance.getHttpClient().execute(req)) {
            // process response
            System.out.println("------------ Response -------------");
            System.out.println(response.getLocale());
            if (response.getProtocolVersion() != null) {
                System.out.println("Protocol : " + response.getProtocolVersion().toString());
            }
            if (response.getStatusLine() != null) {
                System.out.println("ProtocolVersion: " + response.getStatusLine().getProtocolVersion());
                System.out.println("ReasonPhrase: " + response.getStatusLine().getReasonPhrase());
                System.out.println("StatusCode: " + response.getStatusLine().getStatusCode());
            }
            if (response.getAllHeaders() != null) {
                for (Header head : response.getAllHeaders()) {
                    System.out.println(head);
                }
            }
            //process entity
            System.out.println("------------- Entity --------------");
            HttpEntity entity = response.getEntity();
            System.out.println("ContentLength : " + entity.getContentLength());
            System.out.println("Chunked : " + entity.isChunked());
            System.out.println("Repeatable : " + entity.isRepeatable());
            System.out.println("Streaming : " + entity.isStreaming());
            System.out.println(entity.getContentEncoding());
            System.out.println(entity.getContentType());
            System.out.println("------------- Content --------------");
            String output = IOUtils.toString(entity.getContent());
            System.out.println(output);
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        long stopTime = System.currentTimeMillis();
        System.out.printf("------ Time : %.3f seconds -------\n", (stopTime - startTime) / 1000.0);
        assertTrue(true);
    }

    @Test
    public void testUrlConnection() throws MalformedURLException, IOException {
        System.out.println("URL Connection test");
        long startTime = System.currentTimeMillis();

        DownloadManager instance = new DownloadManager();
        // process request
        URL url = new URL("http://uhunt.felix-halim.net/api/poll/0");
        System.out.println("------------ URL -------------");
        System.out.println("Authority : " + url.getAuthority());
        System.out.println("File : " + url.getFile());
        System.out.println("Host : " + url.getHost());
        System.out.println("Path : " + url.getPath());
        System.out.println("Protocol : " + url.getProtocol());
        System.out.println("Query : " + url.getQuery());
        System.out.println("Ref : " + url.getRef());
        System.out.println("UserInfo : " + url.getUserInfo());
        System.out.println("DefaultPort : " + url.getDefaultPort());
        System.out.println("Port : " + url.getPort());
        // open connection
        System.out.println("------------ Http Url Connection -------------");
        HttpURLConnection huc = (HttpURLConnection) url.openConnection();
        huc.connect();
        System.out.println("ContentEncoding : " + huc.getContentEncoding());
        System.out.println("ContentType : " + huc.getContentType());
        System.out.println("RequestMethod : " + huc.getRequestMethod());
        System.out.println("ResponseMessage : " + huc.getResponseMessage());
        System.out.println("UserInteraction : " + huc.getAllowUserInteraction());
        System.out.println("Timeout : " + huc.getConnectTimeout());
        System.out.println("ContentLength : " + huc.getContentLength());
        if (huc.getHeaderFields() != null) {
            huc.getHeaderFields().forEach((String k, List<String> v) -> {
                System.out.println(k + " : " + String.join("; ", v));
            });
        }
        System.out.println("------------ Content -------------");
        String output = IOUtils.toString((InputStream) huc.getContent());
        System.out.println(output);

        long stopTime = System.currentTimeMillis();
        System.out.printf("------ Time : %.3f seconds -------\n", (stopTime - startTime) / 1000.0);
        assertTrue(true);
    }

    @Test
    public void testFluentAPI() throws IOException {
        System.out.println("Fluet API Test");
        long startTime = System.currentTimeMillis();
        Request.Get("http://uhunt.felix-halim.net/api/poll/0").execute().returnContent().asString();

        long stopTime = System.currentTimeMillis();
        System.out.printf("------ Time : %.3f seconds -------\n", (stopTime - startTime) / 1000.0);
    }

    int finished = 0;

    @Test
    public void testDownloadString() throws InterruptedException {

        DownloadManager instance = new DownloadManager();
        String urls[] = new String[]{
            "http://uhunt.felix-halim.net/api/poll/0",
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
            "http://uva.online-judge.org",};

        finished = 0;
        System.out.println("DownloadString test");
        System.out.println("---------------------------------");

        long startTime = System.currentTimeMillis();

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
                System.out.printf("Total Download Time : %.3f seconds\n", task.getDownloadTimeMillis() / 1000.0);

                System.out.println("------------ Content -------------");
                System.out.println(((DownloadString) task).getResult());
            }
        };

        int task = 0;
        for (String url : urls) {
            ++task;
            DownloadString result = instance.downloadString(url);
            result.addTaskMonitor(tm);
            result.startDownload();
            if (task == 1) {
                break;
            }
        }
        System.out.println("Running " + task + " tasks");

        while (finished < task) {
            Thread.sleep(100);
        }

        long stopTime = System.currentTimeMillis();
        System.out.printf("------ Time : %.3f seconds -------\n", (stopTime - startTime) / 1000.0);
        assertEquals(finished, task);
    }

    @Test
    public void testDownloadFile() throws InterruptedException {

        System.out.println("Download File Test");
        DownloadManager instance = new DownloadManager();

        File store = new File("C:\\Users\\Dipu\\Desktop\\Data\\httpclient.tar.gz");
        String url = "http://www.us.apache.org/dist//httpcomponents/httpclient/binary/httpcomponents-client-4.5.1-bin.tar.gz";

        TaskMonitor tm = new TaskMonitor() {

            @Override
            public void statusChanged(DownloadTask task) {
                System.out.println(task);
            }

            @Override
            public void downloadFinished(DownloadTask task) {
                System.out.println(task);
                System.out.printf("Total Download Time : %.3f seconds\n", task.getDownloadTimeMillis() / 1000.0);
            }
        };

        DownloadFile df = instance.downloadFile(url, store, tm);

        df.startDownload();
        df.getDownloadThread().join();

        assertTrue(df.isSuccess());
    }
}
