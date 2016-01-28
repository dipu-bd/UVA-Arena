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
    Submission_Error,
    //15 : Can't be judged
    Cant_Be_Judged,
    //20 : In queue
    In_Queue,
    //30 : Compile error
    Compile_Error,
    //35 : Restricted function
    Restricted_Function,
    //40 : Runtime error
    Runtime_Error,
    //45 : Output limit
    Output_Limit,
    //50 : Time limit
    Time_Limit,
    //60 : Memory limit
    Memory_Limit,
    //70 : Wrong answer
    Wrong_Answer,
    //80 : PresentationE
    Presentation_Error,
    //90 : Accepted
    Accepted;

    @Override
    public String toString() {
        switch (this) {
            case Submission_Error:
                return "Submission Error";
            case Cant_Be_Judged:
                return "Can't Be Judged";
            case In_Queue:
                return "In Queue";
            case Compile_Error:
                return "Compile Error";
            case Restricted_Function:
                return "Restricted Function";
            case Runtime_Error:
                return "Runtime Error";
            case Output_Limit:
                return "Output Limit Exceeded";
            case Time_Limit:
                return "Time Limit Exceeded";
            case Memory_Limit:
                return "Memory Limit Exceeded";
            case Wrong_Answer:
                return "Wrong Answer";
            case Presentation_Error:
                return "Presentation Error";
            case Accepted:
                return "Accepted";
            default:
                return super.toString();
        }
    }

    public int toInt() {
        switch (this) {
            case Submission_Error:
                return 10;
            case Cant_Be_Judged:
                return 15;
            case In_Queue:
                return 20;
            case Compile_Error:
                return 30;
            case Restricted_Function:
                return 35;
            case Runtime_Error:
                return 40;
            case Output_Limit:
                return 45;
            case Time_Limit:
                return 50;
            case Memory_Limit:
                return 60;
            case Wrong_Answer:
                return 70;
            case Presentation_Error:
                return 80;
            case Accepted:
                return 90;
            default:
                return 10;
        }
    }

}
