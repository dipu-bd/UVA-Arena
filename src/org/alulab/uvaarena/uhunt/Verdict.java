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
 * Verdict of a submission.
 */
public enum Verdict {

    //10 : Submission error 
    SubmissionError,
    //15 : Can't be judged
    CantBeJudged,
    //20 : In queue
    InQueue,
    //30 : Compile error
    CompileError,
    //35 : Restricted function
    RestrictedFunction,
    //40 : Runtime error
    RuntimeError,
    //45 : Output limit
    OutputLimit,
    //50 : Time limit
    TimeLimit,
    //60 : Memory limit
    MemoryLimit,
    //70 : Wrong answer
    WrongAnswer,
    //80 : PresentationE
    PresentationError,
    //90 : Accepted
    Accepted;

    @Override
    public String toString() {
        switch (this) {
            case SubmissionError:
                return "Submission Error";
            case CantBeJudged:
                return "Can't Be Judged";
            case InQueue:
                return "In Queue";
            case CompileError:
                return "Compile Error";
            case RestrictedFunction:
                return "Restricted Function";
            case RuntimeError:
                return "Runtime Error";
            case OutputLimit:
                return "Output Limit Exceeded";
            case TimeLimit:
                return "Time Limit Exceeded";
            case MemoryLimit:
                return "Memory Limit Exceeded";
            case WrongAnswer:
                return "Wrong Answer";
            case PresentationError:
                return "Presentation Error";
            case Accepted:
                return "Accepted";
            default:
                return super.toString();
        }
    }

    public int toInt() {
        switch (this) {
            case SubmissionError:
                return 10;
            case CantBeJudged:
                return 15;
            case InQueue:
                return 20;
            case CompileError:
                return 30;
            case RestrictedFunction:
                return 35;
            case RuntimeError:
                return 40;
            case OutputLimit:
                return 45;
            case TimeLimit:
                return 50;
            case MemoryLimit:
                return 60;
            case WrongAnswer:
                return 70;
            case PresentationError:
                return 80;
            case Accepted:
                return 90;
            default:
                return 10;
        }
    }

    public static Verdict fromInt(int val) {
        switch (val) {
            case 10:
                return SubmissionError;
            case 15:
                return CantBeJudged;
            case 20:
                return InQueue;
            case 30:
                return CompileError;
            case 35:
                return RestrictedFunction;
            case 40:
                return RuntimeError;
            case 45:
                return OutputLimit;
            case 50:
                return TimeLimit;
            case 60:
                return MemoryLimit;
            case 70:
                return WrongAnswer;
            case 80:
                return PresentationError;
            case 90:
                return Accepted;
            default:
                return InQueue;
        }
    }
}
