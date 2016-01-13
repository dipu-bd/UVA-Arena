/*
 * Copyright (c) 2016, Dipu
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * * Redistributions of source code must retain the above copyright notice, this
 *   list of conditions and the following disclaimer.
 * * Redistributions in binary form must reproduce the above copyright notice,
 *   this list of conditions and the following disclaimer in the documentation
 *   and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
package org.alulab.uvaarena.utils;

import java.io.File;
import java.util.prefs.Preferences;
import javax.imageio.stream.FileImageOutputStream;
import org.alulab.uvaarena.Launcher;

/**
 * Connector with preferences to store settings.
 */
public class Settings {
    
    final String KEY_WORKING_DIR = "Working Folder";
    
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
}
