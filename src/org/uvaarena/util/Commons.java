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
package org.uvaarena.util;

import java.math.BigInteger; 
import java.util.logging.Logger;
import java.util.prefs.Preferences;
import org.apache.commons.codec.binary.Base64;
import org.apache.commons.codec.binary.StringUtils;
import org.joda.time.Period;
import org.joda.time.format.PeriodFormat;
import org.joda.time.format.PeriodFormatter; 
import org.uvaarena.Launcher;

/**
 *
 */
public abstract class Commons {

    private static final Logger logger = Logger.getLogger(Commons.class.getName());
    private static final String[] BYTE_LENGTH_SUFFIX = {"B", "KB", "MB", "GB", "TB", "PB"};
    private static BigInteger mCurrentHash = BigInteger.valueOf(System.nanoTime());

    /**
     * Converts a given byte length into suitable format.
     *
     * @param byteLength Length in bytes.
     * @param precission Number of placed to display after decimal point. -1 if
     * does not matter. Default is 2.
     * @return
     */
    public static String formatByteLength(double byteLength, int precission) {
        int suffix = 0;
        while (byteLength >= 1024.0) {
            suffix++;
            byteLength /= 1024.0;
        }
        String args = "%." + String.valueOf(precission) + "f %s";
        if (precission < 0) {
            args = "%.12f %s";
        }
        return String.format(args, byteLength, BYTE_LENGTH_SUFFIX[suffix]);
    }

    /**
     * Converts a given byte length into suitable format.
     *
     * @param byteLength Length in bytes.
     * @param precission Number of placed to display after decimal point. -1 if
     * does not matter. Default is 2.
     * @return
     */
    public static String formatByteLength(long byteLength, int precission) {
        return formatByteLength((double) byteLength, precission);
    }

    /**
     * Converts a given byte length into suitable format.
     *
     * @param byteLength Length in bytes.
     * @return
     */
    public static String formatByteLength(double byteLength) {
        return formatByteLength(byteLength, 2);
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

    /**
     * Formats the time spanned into pretty format.
     *
     * @param span Amount of time spanned in milli-seconds
     * @return
     */
    public static String formatTimeSpan(long span) {
        Period period = new Period(span);
        PeriodFormatter formatter = PeriodFormat.getDefault();
        return formatter.print(period);
    }

    /**
     * Generates a unique string hash value
     *
     * @return
     */
    public static String generateHashString() {
        mCurrentHash = mCurrentHash.add(BigInteger.ONE);
        byte[] data = new byte[6];
        byte[] cur = mCurrentHash.toByteArray();
        for (int i = 0; i < 6; ++i) {
            data[i] = i < cur.length ? cur[i] : 0;
        }
        return Base64.encodeBase64String(data);
    }

    /**
     * Encodes a string. You can only decode() it by decodePass() method.
     *
     * @param word String to encode.
     * @return Encoded string in Base64 format.
     */
    public static String encodePass(String word) {
        final byte[] data = StringUtils.getBytesUtf8(word);
        final byte[] salt = StringUtils.getBytesUtf8(getSecretSalt());
        for (int i = 0; i < data.length; ++i) {
            data[i] = (byte) (data[i] ^ salt[i % salt.length]);
        }
        return Base64.encodeBase64String(data);
    }

    /**
     * Decodes a secret string encoded by encodePass() method.
     *
     * @param secret Secret string to decode.
     * @return Decoded string.
     */
    public static String decodePass(String secret) {
        final byte[] data = Base64.decodeBase64(secret);
        final byte[] salt = StringUtils.getBytesUtf8(getSecretSalt());
        for (int i = 0; i < data.length; ++i) {
            data[i] = (byte) (data[i] ^ salt[i % salt.length]);
        }
        return StringUtils.newStringUtf8(data);
    }

    /**
     * Gets a secure secret string which is pre-generated at the installation
     * time of the application and can not be found anyway outside the system.
     *
     * @return
     */
    public static String getSecretSalt() {  
        Preferences pref = Preferences.userNodeForPackage(Launcher.class);
        String salt = pref.get(Settings.KEY_SALT, null);
        if (salt == null) {
            salt = generateHashString();
            pref.put(Settings.KEY_SALT, salt);
        }
        return salt;

    }
}
