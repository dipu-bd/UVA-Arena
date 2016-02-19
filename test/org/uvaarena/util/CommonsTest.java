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

import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Dipu
 */
public class CommonsTest {

    public CommonsTest() {
    }

    /**
     * Test of formatByteLength method, of class Commons.
     */
    @Test
    public void testFormatByteLength_long_int() {
        System.out.println("formatByteLength with precission");
        long byteLength = 1024L;
        int precission = 3;
        String expResult = "1.000 KB";
        String result = Commons.formatByteLength(byteLength, precission);
        assertEquals(expResult, result);

        System.out.println(Commons.formatByteLength(0L, 0));
        System.out.println(Commons.formatByteLength(232333L, 0));
        System.out.println(Commons.formatByteLength(2323223333L, 5));
        System.out.println(Commons.formatByteLength(2323L, -1));
        System.out.println(Commons.formatByteLength(2312253L, 4));
        System.out.println(Commons.formatByteLength(2323224423123L, 6));
        System.out.println(Commons.formatByteLength(2323223395423123L, -1));

        //
        //benchmark test
        /*
         for (int i = 0; i < 100000; ++i) {
         Commons.formatByteLength(0L, 0);
         Commons.formatByteLength(232333L, 0);
         Commons.formatByteLength(2323223333L, 5);
         Commons.formatByteLength(2323L, -1);
         Commons.formatByteLength(2312253L, 4);
         Commons.formatByteLength(2323224423123L, 6);
         Commons.formatByteLength(2323223395423123L, -1);
         } 
         */
    }

    /**
     * Test of formatByteLength method, of class Commons.
     */
    @Test
    public void testFormatByteLength_long() {
        System.out.println("formatByteLength");
        long byteLength = 65123311222L;
        String expResult = "60.65 GB";
        String result = Commons.formatByteLength(byteLength);
        assertEquals(expResult, result);
    }

    /**
     * Test of encodePass method, of class Commons.
     */
    @Test
    public void testEncodeDecodePass() {
        System.out.println("testEncodeDecodePass");
        String salt = Commons.getSecretSalt();
        String pass = "sd*^*laslkj28)35465#@#$32D";
        String encoded = Commons.encodePass(pass);
        String decoded = Commons.decodePass(encoded);
        System.out.println("Salt: " + salt);
        System.out.println("Original: " + pass);
        System.out.println("Encoded: " + encoded);
        System.out.println("Decoded: " + decoded);
        assertEquals(pass, decoded);
    }

    /**
     * Test of getSecretSalt method, of class Commons.
     */
    @Test
    public void getSecretSalt() {
        System.out.println("testSecrectSalt");
        System.out.println(Commons.getSecretSalt());
    }
    
    
    /**
     * Test of generateHashString method, of class Commons.
     */
    @Test
    public void generateHashString() {
        System.out.println("generateHashString");
        System.out.println(Commons.generateHashString());
        System.out.println(Commons.generateHashString(5));
        System.out.println(Commons.generateHashString(12));
        System.out.println(Commons.generateHashString(7));
    }

    /**
     * Test of formatTimeSpan method, of class Commons.
     */
    @Test
    public void testFormatTimeSpan() {
        System.out.println("----- formatTimeSpan -----");
        System.out.println(Commons.formatTimeSpan(4531342222410L));
    }

}
