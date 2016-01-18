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

/**
 *
 */
public final class Commons {

    static final String[] BYTE_LENGTH_SUFFIX = {"B", "KB", "MB", "GB", "TB", "PB"};

    /**
     * Converts a given byte length into suitable format.
     *
     * @param byteLength Length in bytes.
     * @param precission Number of placed to display after decimal point. -1 if
     * does not matter. Default is 2.
     * @return
     */
    public static String formatByteLength(long byteLength, int precission) {
        int suffix = 0;
        double val = (double) byteLength;
        while (val >= 1024.0) {
            suffix++;
            val /= 1024.0;
        }
        String args = "%." + String.valueOf(precission) + "f %s";
        if (precission < 0) {
            args = "%.12f %s";
        }
        return String.format(args, val, BYTE_LENGTH_SUFFIX[suffix]);
    }

    /**
     * Converts a given byte length into suitable format.
     *
     * @param byteLength Length in bytes.
     * @return
     */
    public static String formatByteLength(long byteLength) {
        return formatByteLength(byteLength, 2);
    }
}
