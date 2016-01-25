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
import org.w3c.dom.html.HTMLCollection;
import org.w3c.dom.html.HTMLDivElement;
import org.w3c.dom.html.HTMLFormElement;
import org.w3c.dom.html.HTMLInputElement;
import org.w3c.dom.html.HTMLTextAreaElement;

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

        String submiturl = "https://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25";
        String submitaction = "index.php?option=com_onlinejudge&Itemid=25&page=save_submission";
        String loginaction = "https://uva.onlinejudge.org/index.php?option=com_comprofiler&task=login";

        WebEngine webEngine = webView.getEngine();
        webEngine.getLoadWorker().stateProperty().addListener((ObservableValue<? extends Worker.State> observable, Worker.State oldValue, Worker.State newValue) -> {
            if (newValue == Worker.State.SUCCEEDED) {

                Document doc = webEngine.getDocument();

                System.out.println(".... content received ...");

                try {
                    // first get the message
                    Element content = doc.getElementById("col3_content_wrapper");
                    if (content != null) {
                        NodeList list = content.getElementsByTagName("div");
                        for (int i = 0; i < list.getLength(); ++i) {
                            HTMLDivElement div = (HTMLDivElement) list.item(i);
                            if (div.getClassName() != null
                                    && div.getClassName().equals("message")) {
                                String message = div.getTextContent();
                                System.out.println(message);
                                if (message.startsWith("Submission received with ID")) {
                                    return;
                                }
                            }
                        }
                    }

                    // then proceed to form
                    NodeList allforms = doc.getElementsByTagName("form");
                    System.out.println("--- form count: " + allforms.getLength() + " --- ");
                    HTMLFormElement submitform = null, loginform = null;
                    for (int i = 0; i < allforms.getLength(); ++i) {
                        HTMLFormElement form = (HTMLFormElement) allforms.item(i);
                        if (form.getAction().endsWith(loginaction)) {
                            loginform = form;
                        } else if (form.getAction().endsWith(submitaction)) {
                            submitform = form;
                        }
                    }

                    // first try to login
                    if (loginform != null) {
                        int wassetto = 0;
                        HTMLCollection list = loginform.getElements();
                        for (int i = 0; i < list.getLength(); ++i) {
                            if (list.item(i) instanceof HTMLInputElement) {
                                HTMLInputElement input = (HTMLInputElement) list.item(i);
                                if (input.getType().equals("text")
                                        && input.getName().equals("username")) {
                                    input.setValue("dipu_sust");
                                    ++wassetto;
                                } else if (input.getType().equals("password")
                                        && input.getName().equals("passwd")) {
                                    input.setValue("sd.19.93");
                                    ++wassetto;
                                }
                            }
                        }
                        System.out.println("...submitting login form...");
                        if (wassetto == 2) {
                            loginform.submit();
                        } else {
                            throw new Exception("~~ could not fill login form ~~");
                        }
                    } // if logged in then try to submit
                    else if (submitform != null) {
                        int wassetto = 0;
                        HTMLCollection list = submitform.getElements();
                        for (int i = 0; i < list.getLength(); ++i) {
                            if (list.item(i) instanceof HTMLInputElement) {
                                HTMLInputElement input = (HTMLInputElement) list.item(i);
                                if (input.getType().equals("text")
                                        && input.getName().equals("localid")) {
                                    input.setValue("100");
                                    ++wassetto;
                                } else if (input.getType().equals("radio")
                                        && input.getName().equals("language")
                                        && input.getValue().equals("4")) {
                                    input.setChecked(true);
                                    ++wassetto;
                                }
                            } else if (list.item(i) instanceof HTMLTextAreaElement) {
                                HTMLTextAreaElement code = (HTMLTextAreaElement) list.item(i);
                                if (code.getName().equals("code")) {
                                    code.setTextContent("Hello World!");
                                    ++wassetto;
                                }
                            }
                        }
                        if (wassetto == 3) {
                            System.out.println("... filled the submit form ...");
                            //submitform.submit();
                        } else {
                            throw new Exception("~~ could not fill submit form ~~");
                        }
                    } // if logged in but could not submit then reload page
                    else {
                        webEngine.load(submiturl);
                    }
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        });

        webEngine.load(submiturl);
    }

}
