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
package org.alulab.uvaarena.util;

import java.util.prefs.Preferences; 
import org.alulab.uvaarena.Launcher;

/**
 * Connector with preferences to store settings.
 */
public final class Settings {
    
    final String KEY_WORKING_DIR = "Working Folder";
    final String KEY_CODE_DIR = "Code Folder";
    
    private final Preferences mPreference;
    
    public Settings() {
        mPreference = Preferences.userNodeForPackage(Launcher.class);
    }
    
    public final Preferences getPreferences() {
        return mPreference;        
    }
    
    public String getWorkingFolder() {
        return mPreference.get(KEY_WORKING_DIR, FileHelper.getDefaultWorkingFolder());        
    }
    public void setWorkingFolder(String dir) {
        mPreference.put(KEY_WORKING_DIR, dir);        
    }
    
    public String getCodeFolder() {
        return mPreference.get(KEY_CODE_DIR, FileHelper.getDefaultCodeFolder());        
    }
    public void setCodeFolder(String dir) {
        mPreference.put(KEY_CODE_DIR, dir);
    }
}
