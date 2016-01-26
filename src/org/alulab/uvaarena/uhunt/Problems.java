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

import java.util.logging.Level;
import java.util.logging.Logger;
import org.json.simple.JSONArray;

/**
 *
 * @author Dipu
 */
public class Problems extends java.util.HashSet<Problem> {

    private static final Logger logger = Logger.getLogger(Problems.class.getName());

    /**
     * Creates a list of problems
     */
    public Problems() {
    }

    public static Problems parse(JSONArray jarr) {
        Problems problems = new Problems();
        try {
            jarr.forEach((Object ob) -> {
                Problem prob = Problem.parse((JSONArray) ob);
                if (prob != null) {
                    problems.add(prob);
                }
            });
        } catch (Exception ex) {
            logger.log(Level.SEVERE, null, ex);
        }
        return problems;
    }

}
