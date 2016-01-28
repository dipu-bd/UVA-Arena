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

/**
 * Language of a submission <br>
 * 1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11
 *
 * @author Dipu
 */
public enum Language {

    Unknown,
    AnsiC,
    Java,
    Cpp,
    Pascal,
    Cpp11;

    @Override
    public String toString() {
        switch (this) {
            case AnsiC:
                return "ANSI C";
            case Java:
                return "Java";
            case Cpp:
                return "C++";
            case Pascal:
                return "Pascal";
            case Cpp11:
                return "C++ 11";
            default:
                return super.toString();
        }
    }

    public int toInt() {
        switch (this) {
            case AnsiC:
                return 1;
            case Java:
                return 2;
            case Cpp:
                return 3;
            case Pascal:
                return 4;
            case Cpp11:
                return 5;
            default:
                return 0;
        }
    }
}
