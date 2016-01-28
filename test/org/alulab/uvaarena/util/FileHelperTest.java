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

        path = "good file.txt";
        expResult = "good file.txt";
        result = FileHelper.cleanFileName(path);
        assertEquals(expResult, result);

        path = "Cas-=23\\23[3'mao<><<>to";
        expResult = "Cas-=23 23[3'mao to";
        result = FileHelper.cleanFileName(path);
        System.out.println(result);
        assertEquals(expResult, result);

        path = "very _ +  <   bad > ? >/ ~`1 file :;;;. ../? txt ";
        expResult = "very _ + bad ~`1 file ;;;. .. txt";
        result = FileHelper.cleanFileName(path);
        System.out.println(result);
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
     * Test of getDefaultWorkingFolder method, of class FileHelper.
     */
    @Test
    public void testGetDefaultWorkingFolder() {
        System.out.println("getDefaultWorkingFolder");
        String result = FileHelper.getDefaultWorkDir();
        System.out.println(result);
    }

    /**
     * Test of getDefaultCodeFolder method, of class FileHelper.
     */
    @Test
    public void testGetDefaultCodeFolder() {
        System.out.println("getDefaultCodeFolder");
        String result = FileHelper.getDefaultCodeDir();
        System.out.println(result);
    }

    @Test
    public void testRenameFile() {
        System.out.println("renameFile");
        String destName;
        File sourceFile, result;

        sourceFile = new File("E:\\Projects\\GitHub\\UVA-Arena\\build\\dest.txt");
        destName = "wat/sd\\for test.cpp";
        result = FileHelper.renameFile(sourceFile, destName);
        System.out.println("Destination: " + result);

        if (result == null) {
            sourceFile = new File("E:\\Projects\\GitHub\\UVA-Arena\\build\\for test.cpp");
            destName = "wat/sd\\dest.txt";
            result = FileHelper.renameFile(sourceFile, destName);
            System.out.println("Destination: " + result);
        }
    }

}
