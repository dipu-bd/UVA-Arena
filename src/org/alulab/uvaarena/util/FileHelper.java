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

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import org.apache.commons.io.FileUtils;
import org.apache.commons.io.FilenameUtils;

/**
 *
 * @author Dipu
 */
public abstract class FileHelper {

    private final static int[] illegalChars = {34, 60, 62, 124, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 58, 42, 63, 92, 47};

    static {
        Arrays.sort(illegalChars);
    }

    /**
     * Gets the default directory where all data is stored.
     *
     * @return USER_HOME/Arena Suite/UVA Arena
     */
    public static String getDefaultWorkingFolder() {
        return FileUtils.getFile(FileUtils.getUserDirectory(), "Arena Suite", "UVA Arena").toString();
    }

    /**
     * Gets the default directory to store code files.
     *
     * @return USER_HOME/Arena Suite/UVA Arena/Codes
     */
    public static String getDefaultCodeFolder() {
        return FileUtils.getFile(getDefaultWorkingFolder(), "Codes").toString();
    }

    /**
     * Replace unsupported characters with underscore(_) and returns a valid
     * path.
     *
     * @param badFileName file name to clean.
     * @return
     */
    public static String cleanFileName(String badFileName) {
        StringBuilder cleanName = new StringBuilder();
        int len = badFileName.codePointCount(0, badFileName.length());
        int lastPoint = 0;
        for (int i = 0; i < len; i++) {
            int c = badFileName.codePointAt(i);
            if (Arrays.binarySearch(illegalChars, c) >= 0) {  // illegal
                c = ' ';
            }
            if (!(c == ' ' && lastPoint == ' ')) {
                cleanName.appendCodePoint(c);
            }
            lastPoint = c;
        }
        return cleanName.toString().trim();
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
     * Moves all <code>source</code> file or folders into the
     * <code>destination</code> folder. <br/>
     * If the source is a directory, it copies all of its content recursively
     * into the destination folder.
     *
     * @param destination Folder to copy files.
     * @param sources List of source files, can be either a Directory or File.
     */
    public static void moveFiles(File destination, File... sources) throws IOException {
        for (File file : sources) {
            if (file.exists()) {
                if (file.isFile()) {
                    FileUtils.moveFileToDirectory(file, destination, true);
                } else {
                    FileUtils.moveDirectoryToDirectory(file, destination, true);
                }
            }
        }
    }

    /**
     * Renames a file to the given file name
     *
     * @param sourceFile Source file.
     * @param destFileName Destination file name.
     * @return The destination file if renamed successfully, null otherwise.
     */
    public static File renameFile(File sourceFile, String destFileName) {
        try {
            File dest = sourceFile.getParentFile();
            destFileName = FilenameUtils.getName(destFileName);
            if (dest == null) {
                dest = new File(destFileName);
            } else {
                dest = new File(sourceFile.getParent() + File.separator + destFileName);
            }

            if (sourceFile.renameTo(dest)) {
                return dest;
            }
        } catch (Exception ex) {
        }
        return null;
    }

}
