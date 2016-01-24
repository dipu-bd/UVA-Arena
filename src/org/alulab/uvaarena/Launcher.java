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
package org.alulab.uvaarena;

import java.awt.GridLayout;
import java.io.IOException;
import static javafx.application.Application.launch;
import javafx.application.Application;
import javafx.beans.value.ObservableValue;
import javafx.concurrent.Worker;
import javafx.scene.Scene;
import javafx.scene.layout.BorderPane;
import javafx.scene.web.WebEngine;
import javafx.scene.web.WebView;
import javafx.stage.Stage;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.w3c.dom.html.HTMLFormElement;

/**
 * Start point of the application.
 */
public class Launcher extends Application {

    private Core mCore;
    private Object DownloadManagerTest;

    @Override
    public void start(Stage stage) throws Exception {
        mCore = new Core(stage);

        WebView view = new WebView();
        BorderPane border = new BorderPane();
        border.setCenter(view);
        stage.setScene(new Scene(border));

        stage.show();

        testWebEngineLogin(view);
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }

    private void testWebEngineLogin(WebView webView) throws InterruptedException, IOException {

        System.out.println("---- TEST DOWNLOAD PAGE ----");

        String login = "mod_loginform";
        String unameid = "username";
        String passid = "passwd";
        String problemid = "localid";
        String language = "language";
        String code = "code";
        String file = "codeupl";
        String submiturl = "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25";

        WebEngine webEngine = webView.getEngine();
        webEngine.getLoadWorker().stateProperty().addListener((ObservableValue<? extends Worker.State> observable, Worker.State oldValue, Worker.State newValue) -> {
            if (newValue == Worker.State.SUCCEEDED) {

                Document doc = webEngine.getDocument();

                System.out.println(".... content received ...");

                try {
                    Element elem = doc.getElementById(login);
                    if (elem != null) {
                        int wassetto = 0;
                        HTMLFormElement loginform = (HTMLFormElement) elem;
                        NodeList list = loginform.getElementsByTagName("input");
                        for (int i = 0; i < list.getLength(); ++i) {
                            elem = (Element) list.item(i);
                            if (elem.getAttribute("name").equals(unameid)) {
                                elem.setAttribute("value", "dipu_sust");
                                ++wassetto;
                            } else if (elem.getAttribute("name").equals(passid)) {
                                elem.setAttribute("value", "sd.19.93");
                                ++wassetto;
                            }
                        }
                        if (wassetto != 2) {
                            throw new Exception();
                        }
                        System.out.println("...submitting login form...");
                        loginform.submit();
                    } else {
                        int wassetto = 0;
                        System.out.println(doc.getElementsByTagName("form").getLength());
                        Node nod = doc.getElementsByTagName("form").item(1);
                        HTMLFormElement submitform = (HTMLFormElement) nod;
                        NodeList list = submitform.getChildNodes();
                        for (int i = 0; i < list.getLength(); ++i) {
                            if (list.item(i) instanceof Element) {
                                elem = (Element) list.item(i);
                                if (elem.getAttribute("name").equals(problemid)) {
                                    elem.setAttribute("value", "100");
                                    ++wassetto;
                                } else if (elem.getAttribute("name").equals(code)) {
                                    elem.setAttribute("value", "Hello World");
                                    ++wassetto;
                                } else if (elem.getAttribute("name").equals(language)) {
                                    if (elem.getAttribute("value").equals("3")) {
                                        elem.setAttribute("checked", "true");
                                    }
                                    ++wassetto;
                                }
                            }
                        }
                        if (wassetto != 3) {
                            throw new Exception("...could not fill submit form...");
                        } else {
                            System.out.println("...filled the submit form...");
                        }
                    }
                } catch (Exception ex) {
                    System.out.println(ex);
                    webEngine.load(submiturl);
                    System.out.println("...redirecting to submit form...");
                }
            }
        });

        webEngine.load(submiturl);
    }

}
