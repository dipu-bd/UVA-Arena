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
package org.alulab.uvaarena.uhunt;

import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonSyntaxException;
import java.io.File;
import java.io.IOException;
import java.io.Serializable;
import java.util.NavigableSet;
import java.util.SortedSet;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.apache.commons.io.FileUtils;

/**
 *
 */
public final class Problems extends java.util.TreeSet<Problem> implements Serializable {

    private static final Logger logger = Logger.getLogger(Problems.class.getName());

    /**
     * Creates a list of problems
     */
    public Problems() {
    }

    /**
     * Parses a JSON array and creates problem array from it.
     *
     * @param jarr JSON array to parse.
     * @return List of problems. EMPTY array if any error occurs while parsing.
     */
    public static Problems create(JsonArray jarr) {
        Problems problems = new Problems();
        try {
            jarr.forEach((JsonElement elem) -> {
                Problem prob = Problem.create(elem.getAsJsonArray());
                if (prob != null) {
                    problems.add(prob);
                }
            });
        } catch (Exception ex) {
            logger.log(Level.SEVERE, null, ex);
        }
        return problems;
    }

    /**
     * Creates problem list from the given file.
     *
     * @param file File to load from
     * @return Problems array on success. Empty array otherwise.
     */
    public static Problems load(File file) {
        Problems problem = null;
        try {
            String json = FileUtils.readFileToString(file);
            problem = UHunt.getGson().fromJson(json, Problems.class);
        } catch (IOException | JsonSyntaxException ex) {
            logger.log(Level.SEVERE, null, ex);
        }
        return (problem == null) ? new Problems() : problem;
    }

    /**
     * Saves the problems list to a specific file.
     *
     * @param problemList List of problems
     * @param file File to save to.
     * @return True on success, False otherwise.
     */
    public static boolean save(Problems problemList, File file) {
        try {
            String json = UHunt.getGson().toJson(problemList);
            FileUtils.writeStringToFile(file, json);
            return true;
        } catch (IOException ex) {
            logger.log(Level.SEVERE, null, ex);
            return false;
        }
    }

    /**
     * Creates a new dummy problem using only problem number.
     *
     * @param number Problem number to use.
     * @return A new Problem
     */
    public Problem dummy(long number) {
        return new Problem(number);
    }

    public Problem lower(long number) {
        return lower(dummy(number));
    }

    public Problem floor(long number) {
        return floor(dummy(number));
    }

    public Problem ceiling(long number) {
        return ceiling(dummy(number));
    }

    public Problem higher(long number) {
        return higher(dummy(number));
    }

    public NavigableSet<Problem> subSet(long number, boolean bln, long number1, boolean bln1) {
        return subSet(dummy(number), bln, dummy(number1), bln1);
    }

    public NavigableSet<Problem> headSet(long number, boolean bln) {
        return headSet(dummy(number), bln);
    }

    public NavigableSet<Problem> tailSet(long number, boolean bln) {
        return tailSet(dummy(number), bln);
    }

    public SortedSet<Problem> subSet(long first, long last) {
        return subSet(dummy(first), dummy(last));
    }

    public SortedSet<Problem> headSet(long number) {
        return headSet(dummy(number));
    }

    public SortedSet<Problem> tailSet(long number) {
        return tailSet(dummy(number));
    }

    public Problem[] toProblemArray() {
        return toArray(new Problem[0]);
    }

    public boolean contains(long number) {
        return floor(number).number() == number;
    }
}
