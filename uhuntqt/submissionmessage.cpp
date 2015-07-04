#include "submissionmessage.h"

using namespace uhuntqt;

SubmissionMessage::SubmissionMessage()
{

}

SubmissionMessage::SubmissionMessage(const QJsonObject &data)
{
    loadData(data);
}

void SubmissionMessage::loadData(const QJsonObject &data)
{
    //sid: Submission ID
    setSubmissionID(data["sid"].toInt());
    //uid: user id
    setUserID(data["uid"].toInt());
    //pid: Problem ID
    setProblemID(data["pid"].toInt());
    //ver: Verdict ID
    setVerdict(data["ver"].toInt());
    //lan: Language ID
    setLanguage(data["lan"].toInt());
    //run : Runtime
    setRuntime(data["run"].toInt());
    //mem: Memory taken
    setMemory(data["mem"].toInt());
    //rank: Submission Rank
    setRank(data["rank"].toInt());
    //sbt: Submission Time (UNIX time stamp)
    setSubmissionTime(data["sbt"].toInt());
    //name: full username
    setFullName(data["name"].toString());
    //uname: user name
    setUserName(data["uname"].toString());

}

void SubmissionMessage::setVerdict(int v)
{
    switch(v)
    {
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
    switch(v)
    {
    case 1: mLanguage = Language::C; break;
    case 2: mLanguage = Language::Java; break;
    case 3: mLanguage = Language::CPP; break;
    case 4: mLanguage = Language::Pascal; break;
    case 5: mLanguage = Language::CPP11; break;
    default: mLanguage = Language::Other; break;
    }
}


void SubmissionMessage::setRuntime(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    mRuntime = v;
}

void SubmissionMessage::setMemory(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    mMemory = v;
}

