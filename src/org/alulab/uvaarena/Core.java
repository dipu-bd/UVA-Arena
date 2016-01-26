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
import org.alulab.uvaarena.uhunt.UHunt;
import org.alulab.uvaarena.util.Settings;
import org.alulab.uvaarena.uva.CodeSubmitter;

/**
 * Core object for interactivity among all the components of the application.
 */
public final class Core {

    private final Stage mPrimaryStage;
    private final Settings mSetting;
    private final UHunt mUhunt;
    private final CodeSubmitter mSubmitter;

    public Core(Stage stage) {
        mPrimaryStage = stage;
        mSetting = new Settings();
        mSubmitter = new CodeSubmitter();
        mUhunt = new UHunt(this);

        initialize();
    }

    /**
     * Initializes all values after the is core created
     */
    private void initialize() {
        mSubmitter.setUserName(mSetting.getDefaultUsername());
        mSubmitter.setPassword(mSetting.getPassword());
    }

    /**
     * Gets the primary stage.
     *
     * @return
     */
    public Stage primaryStage() {
        return mPrimaryStage;
    }

    /**
     * Gets the settings.
     *
     * @return
     */
    public Settings setting() {
        return mSetting;
    }

    /**
     * Gets the UHunt
     *
     * @return
     */
    public UHunt uhunt() {
        return mUhunt;
    }

    /**
     * Gets the code submitter.
     *
     * @return
     */
    public CodeSubmitter submitter() {
        return mSubmitter;
    }
}
