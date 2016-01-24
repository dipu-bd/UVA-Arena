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
import java.io.FileOutputStream; 
import java.io.IOException;
import org.apache.commons.io.FileUtils; 
import org.apache.commons.io.IOUtils; 
import org.apache.http.client.methods.HttpGet;

/**
 * 
 */
public class DownloadFile extends DownloadTask {

    private File mFile = null;
    private File mTmpFile = null;
    private FileOutputStream mFOS = null;

    public DownloadFile(String url, File file) {
        setFile(file);
        setUriRequest(new HttpGet(url));
    }

    public File getFile() {
        return mFile;
    }

    public final void setFile(File file) {
        mFile = file;        
        mTmpFile = getTempFile();
    }

    private File getTempFile() {
        File tmp = null;
        String num = "";
        String name = mFile.getAbsoluteFile() + ".dfdump";
        for (int i = 1; (tmp = new File(name + num)).exists(); ++i) {
            num = "_" + i;
        }
        return tmp;
    }

    @Override
    void beforeProcessingResponse() throws IOException {
        IOUtils.closeQuietly(mFOS);
        mFOS = new FileOutputStream(mTmpFile);            
    }

    @Override
    void processByte(byte[] data, int size) throws IOException {
        mFOS.write(data, 0, size);
    }

    @Override
    void afterProcessingResponse() throws IOException {
        mFOS.close();
        FileUtils.moveFile(mTmpFile, mFile);
    }
    
    @Override
    void onDownloadFailed(Exception ex) { 
        FileUtils.deleteQuietly(mTmpFile);
    }            
} 
