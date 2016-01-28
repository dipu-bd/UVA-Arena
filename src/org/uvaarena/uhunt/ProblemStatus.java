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
package org.uvaarena.uhunt;

/**
 * Status of a problem <br>
 * 0 = unavailable, 1 = normal, 2 = special judge
 *
 * @author Dipu
 */
public enum ProblemStatus {

    //0 = unavaiable
    Unavailable,
    //1 = normal
    Normal,
    //2 = special judge
    Special_Judge;

    @Override
    public String toString() {
        switch (this) {
            case Unavailable:
                return "Unavaiable";
            case Normal:
                return "Normal";
            case Special_Judge:
                return "Special Judge";
            default:
                return super.toString();
        }
    }

    public int toInt() {
        switch (this) {
            case Unavailable:
                return 0;
            case Normal:
                return 1;
            case Special_Judge:
                return 2;
            default:
                return 1;
        }
    }
    
    public static ProblemStatus fromInt(int val) {
          switch (val) {
            case 0:
                return Unavailable;
            case 1:
                return Normal;
            case 2:
                return Special_Judge;
            default:
                return Normal;
        }
    }
}
