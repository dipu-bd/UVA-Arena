#include "submissionmessage.h"

using namespace uva;

SubmissionMessage::SubmissionMessage()
{
    setUserID(0);
    setUserName("-");
    setProblemNumber(0);
    setProblemTitle("-");
}

SubmissionMessage SubmissionMessage::fromJsonObject(const QJsonObject& data)
{
    SubmissionMessage sub;

    //sid: Submission ID
    sub.setSubmissionID(data["sid"].toInt());
    //uid: user id
    sub.setUserID(data["uid"].toInt());
    //pid: Problem ID
    sub.setProblemID(data["pid"].toInt());
    //ver: Verdict ID
    sub.setVerdict(data["ver"].toInt());
    //lan: Language ID
    sub.setLanguage(data["lan"].toInt());
    //run : Runtime
    sub.setRuntime(data["run"].toInt());
    //mem: Memory taken
    sub.setMemory(data["mem"].toInt());
    //rank: Submission Rank
    sub.setRank(data["rank"].toInt());
    //sbt: Submission Time (UNIX time stamp)
    sub.setSubmissionTime(data["sbt"].toInt());
    //name: full username
    sub.setFullName(data["name"].toString());
    //uname: user name
    sub.setUserName(data["uname"].toString());

    /*
      TODO:
        sub.setProblemNumber(...);
        sub.setProblemTitle(...);
    */

    return sub;
}

void SubmissionMessage::setVerdict(int verdict)
{
    switch(verdict) {
        case 10: mVerdict = Verdict::SubError; break;
        case 15: mVerdict = Verdict::CannotBeJudge; break;
        case 20: mVerdict = Verdict::InQueue; break;
        case 30: mVerdict = Verdict::CompileError; break;
        case 35: mVerdict = Verdict::RestrictedFunction; break;
        case 40: mVerdict = Verdict::RuntimeError; break;
        case 45: mVerdict = Verdict::OutputLimit; break;
        case 50: mVerdict = Verdict::TimLimit; break;
        case 60: mVerdict = Verdict::MemoryLimit; break;
        case 70: mVerdict = Verdict::WrongAnswer; break;
        case 80: mVerdict = Verdict::PresentationError; break;
        case 90: mVerdict = Verdict::Accepted; break;
        default: mVerdict = Verdict::InQueue; break;
    }
}

void SubmissionMessage::setLanguage(int v)
{
    switch(v) {
    case 1: mLanguage = Language::C; break;
    case 2: mLanguage = Language::Java; break;
    case 3: mLanguage = Language::CPP; break;
    case 4: mLanguage = Language::Pascal; break;
    case 5: mLanguage = Language::CPP11; break;
    default: mLanguage = Language::Other; break;
    }
}


void SubmissionMessage::setRuntime(int runTime)
{
    if (runTime < 0 || runTime >= 1000000000)
        runTime = 0;

    mRuntime = runTime;
}

void SubmissionMessage::setMemory(int memory)
{
    if (memory < 0 || memory >= 1000000000) 
        memory = 0;

    mMemory = memory;
}

