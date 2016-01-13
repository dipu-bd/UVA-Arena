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

import com.sun.deploy.util.StringUtils;
import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.util.Arrays;
import org.apache.commons.io.FileUtils;

/**
 *
 * @author Dipu
 */
public final class FileHelper {

    private final static int[] illegalChars = {34, 60, 62, 124, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 58, 42, 63, 92, 47};

    static {
        Arrays.sort(illegalChars);
    }

    /**
     * Replace unsupported characters with underscore(_) and returns a valid
     * path.
     *
     * @param path path to clean.
     * @return
     */
    public static String cleanFileName(String badFileName) {
        StringBuilder cleanName = new StringBuilder();
        boolean lastIsValid = true;
        int len = badFileName.codePointCount(0, badFileName.length());
        for (int i = 0; i < len; i++) {
            int c = badFileName.codePointAt(i);
            if (Arrays.binarySearch(illegalChars, c) < 0) {  // not illegal
                lastIsValid = true;
                cleanName.appendCodePoint(c);
            } else {
                if (lastIsValid) { // last found character was not valid
                    cleanName.appendCodePoint('_');
                }
                lastIsValid = false;
            }
        }
        return cleanName.toString();
    }

    /**
     * Joins parts of file path together.
     *
     * @param parts Parts of files listed sequentially.
     * @return
     */
    public static String joinPath(String... parts) {
        String file = "";
        for (String suffix : parts) {
            if (suffix != null && !suffix.trim().isEmpty()) {
                if (!file.isEmpty()) {
                    file += File.separator;
                }
                file += suffix.trim();
            }
        }
        return file;
    }

    /**
     * Joins parts of file paths together.
     *
     * @param file Parent file.
     * @param parts Parts of path listed sequentially.
     * @return
     */
    public static File joinPath(File file, String... parts) {
        String[] temp = new String[parts.length + 1];
        temp[0] = file.getAbsolutePath();
        System.arraycopy(parts, 0, temp, 1, parts.length);
        return new File(joinPath(temp));
    }

    /**
     * Copies all <code>source</code> file or folders into the
     * <code>destination</code> folder. <br/>
     * If the source is a directory, it copies all of its content recursively
     * into the destination folder.
     *
     * @param destination Folder to copy files.
     * @param sources List of source files, can be either a Directory or File.
     */
    public static void copyFiles(File destination, File... sources) throws IOException {
        for (File file : sources) {
            if (file.exists()) {
                if (file.isFile()) {
                    FileUtils.copyFileToDirectory(file, destination, true);
                } else {
                    FileUtils.copyDirectoryToDirectory(file, destination);
                }
            }
        }
    }

    /**
     * Gets the home directory of the current user.
     *
     * @return
     */
    public static String getUserHome() {
        return System.getProperty("user.home");
    }

    /**
     * Gets the default directory where all data is stored.
     *
     * @return USER_HOME/Arena Suite/UVA Arena
     */
    public static String getDefaultWorkingFolder() {
        return joinPath(getUserHome(), "Arena Suite", "UVA Arena");
    }

    /**
     * Gets the default directory to store code files.
     *
     * @return USER_HOME/Arena Suite/UVA Arena/Codes
     */
    public static String getDefaultCodeFolder() {
        return joinPath(getDefaultWorkingFolder(), "Codes");
    }
}
