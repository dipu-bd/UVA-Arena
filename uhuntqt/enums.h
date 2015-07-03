#pragma once

enum ProblemStatus
{
    Unavaible = 0,
    Normal = 1,
    Special_Judge = 2,
};

enum Language
{
    Other = 0,
    C = 1,
    Java = 2,
    CPP = 3,
    Pascal = 4,
    CPP11 = 5
};

enum Verdict
{
    SubError = 10,
    CannotBeJudge = 15,
    InQueue = 20,
    CompileError = 30,
    RestrictedFunction = 35,
    RuntimeError = 40,
    OutputLimit = 45,
    TimLimit = 50,
    MemoryLimit = 60,
    WrongAnswer = 70,
    PresentationError = 80,
    Accepted = 90
};
