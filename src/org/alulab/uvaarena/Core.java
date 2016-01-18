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
      
import javafx.stage.Stage;
import org.alulab.uvaarena.uhuntapi.UHuntAPI;
import org.alulab.uvaarena.webapi.DownloadManager;
import org.alulab.uvaarena.util.Settings;

/**
 * Core object for interactivity among all the components of the application.
 */
public class Core {

    private final Stage mPrimaryStage;
    private final Settings mSetting;
    private final DownloadManager mDownloader;
    private final UHuntAPI mUhuntApi;
    
    public Core(Stage stage) {
        mPrimaryStage = stage;      
        mSetting = new Settings();       
        mUhuntApi = new UHuntAPI();
        mDownloader = new DownloadManager();
    }
        
    public Stage primaryStage() {
        return mPrimaryStage;
    }
    
    public Settings setting() {
        return mSetting;
    }
    
    public DownloadManager downloader() {
        return mDownloader;
    }
    
    public UHuntAPI uhunt() {
        return mUhuntApi;
    }
}
