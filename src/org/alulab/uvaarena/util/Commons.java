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

import java.io.UnsupportedEncodingException;
import java.math.BigInteger;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLDecoder;
import java.net.URLEncoder;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.apache.commons.codec.binary.Base64;

/**
 *
 */
public abstract class Commons {

    private static final Logger logger = Logger.getLogger(Commons.class.getName());
    private static final String[] BYTE_LENGTH_SUFFIX = {"B", "KB", "MB", "GB", "TB", "PB"};
    private static BigInteger mCurrentHash = BigInteger.valueOf(System.currentTimeMillis() << 3);

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
     * Converts URL into a map of (key, value) pair of query data.
     *
     * @param Url URL to parse
     * @return KeyValue pair of queries. Empty if none.
     */
    public static Map<String, String> splitQuery(String Url) {
        Map<String, String> query_pairs = new LinkedHashMap<>();
        try {
            URL url = new URL(Url);
            String query = url.getQuery();
            String[] pairs = query.split("&");
            for (String pair : pairs) {
                int idx = pair.indexOf("=");
                String key = URLDecoder.decode(pair.substring(0, idx), "UTF-8");
                String value = URLDecoder.decode(pair.substring(idx + 1), "UTF-8");
                query_pairs.put(key, value);
            }
        } catch (MalformedURLException | UnsupportedEncodingException ex) {
            logger.log(Level.SEVERE, null, ex);
        }
        return query_pairs;
    }

}
