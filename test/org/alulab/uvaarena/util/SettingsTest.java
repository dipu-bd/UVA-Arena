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

import java.util.prefs.Preferences;
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
public class SettingsTest {
    
    public SettingsTest() {
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
     * Test of getPreferences method, of class Settings.
     */
    @Test
    public void testGetPreferences() {
        System.out.println("getPreferences");
        Settings instance = new Settings();
        Preferences expResult = null;
        Preferences result = instance.getPreferences();
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getWorkingFolder method, of class Settings.
     */
    @Test
    public void testGetWorkingFolder() {
        System.out.println("getWorkingFolder");
        Settings instance = new Settings();
        String expResult = "";
        String result = instance.getWorkingFolder();
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of setWorkingFolder method, of class Settings.
     */
    @Test
    public void testSetWorkingFolder() {
        System.out.println("setWorkingFolder");
        String dir = "";
        Settings instance = new Settings();
        instance.setWorkingFolder(dir);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getCodeFolder method, of class Settings.
     */
    @Test
    public void testGetCodeFolder() {
        System.out.println("getCodeFolder");
        Settings instance = new Settings();
        String expResult = "";
        String result = instance.getCodeFolder();
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of setCodeFolder method, of class Settings.
     */
    @Test
    public void testSetCodeFolder() {
        System.out.println("setCodeFolder");
        String dir = "";
        Settings instance = new Settings();
        instance.setCodeFolder(dir);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }
    
}
