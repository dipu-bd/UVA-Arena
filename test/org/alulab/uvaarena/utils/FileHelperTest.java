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
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Dipu
 */
public class FileHelperTest {

    public FileHelperTest() {
    }

    @BeforeClass
    public static void setUpClass() {
    }

    @AfterClass
    public static void tearDownClass() {
    }

    @Before
    public void setUp() {
    }

    @After
    public void tearDown() {
    }

    /**
     * Test of cleanPath method, of class FileHelper.
     */
    @Test
    public void testCleanFileName() {
        System.out.println("cleanPath");

        String path, expResult, result;

        path = "Cas-=23\\23[3'mao<><<>to";
        expResult = "Cas-=23_23[3'mao_to";
        result = FileHelper.cleanFileName(path);
        assertEquals(expResult, result);
    }

    /**
     * Test of joinPath method, of class FileHelper.
     */
    @Test
    public void testJoinPath_StringArr() {
        System.out.println("joinPath");
        String[] parts;
        String expResult, result;

        parts = new String[]{"te", "ww", "as", "ssd"};
        expResult = "te\\ww\\as\\ssd";
        result = FileHelper.joinPath(parts);
        assertEquals(expResult, result);

        parts = new String[]{"te", "ww", "as", " "};
        expResult = "te\\ww\\as";
        result = FileHelper.joinPath(parts);
        assertEquals(expResult, result);

        parts = new String[]{"  ", "ww", "as", "ssd"};
        expResult = "ww\\as\\ssd";
        result = FileHelper.joinPath(parts);
        assertEquals(expResult, result);

        parts = new String[]{" ", "ww", null, " "};
        expResult = "ww";
        result = FileHelper.joinPath(parts);
        assertEquals(expResult, result);

        parts = new String[]{"C:\\", "  www  ", "as\\", "tt "};
        expResult = "C:\\\\www\\as\\\\tt";
        result = FileHelper.joinPath(parts);
        assertEquals(expResult, result);
    }

    /**
     * Test of joinPath method, of class FileHelper.
     */
    @Test
    public void testJoinPath_File_StringArr() {
        System.out.println("joinPath");
        File file = new File("C:\\Users\\Dipu\\Desktop");
        String[] parts = { " e ", "  ", null, " hello", " ", "no need " };
        File expResult = new File("C:\\Users\\Dipu\\Desktop\\e\\hello\\no need");
        File result = FileHelper.joinPath(file, parts);
        assertEquals(expResult, result); 
    }

    /**
     * Test of copyFiles method, of class FileHelper.
     */
    @Test
    public void testCopyFiles() throws Exception {
        System.out.println("copyFiles");
        File destination = null;
        File[] sources = null;
        FileHelper.copyFiles(destination, sources);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getUserHome method, of class FileHelper.
     */
    @Test
    public void testGetUserHome() {
        System.out.println("getUserHome"); 
        String result = FileHelper.getUserHome();
        System.out.println(result);
    }

    /**
     * Test of getDefaultWorkingFolder method, of class FileHelper.
     */
    @Test
    public void testGetDefaultWorkingFolder() {
        System.out.println("getDefaultWorkingFolder");
        String result = FileHelper.getDefaultWorkingFolder();
        System.out.println(result);
    }

    /**
     * Test of getDefaultCodeFolder method, of class FileHelper.
     */
    @Test
    public void testGetDefaultCodeFolder() {
        System.out.println("getDefaultCodeFolder");
        String result = FileHelper.getDefaultCodeFolder();
        System.out.println(result);
    }

}
