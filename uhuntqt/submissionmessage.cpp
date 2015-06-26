#include "submissionmessage.h"

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
    case 10: vVerdict = Verdict::SubError; break;
    case 15: vVerdict = Verdict::CannotBeJudge; break;
    case 20: vVerdict = Verdict::InQueue; break;
    case 30: vVerdict = Verdict::CompileError; break;
    case 35: vVerdict = Verdict::RestrictedFunction; break;
    case 40: vVerdict = Verdict::RuntimeError; break;
    case 45: vVerdict = Verdict::OutputLimit; break;
    case 50: vVerdict = Verdict::TimLimit; break;
    case 60: vVerdict = Verdict::MemoryLimit; break;
    case 70: vVerdict = Verdict::WrongAnswer; break;
    case 80: vVerdict = Verdict::PresentationError; break;
    case 90: vVerdict = Verdict::Accepted; break;
    default: vVerdict = Verdict::InQueue; break;
    }
}

void SubmissionMessage::setLanguage(int v)
{
    switch(v)
    {
    case 1: vLanguage = Language::C; break;
    case 2: vLanguage = Language::Java; break;
    case 3: vLanguage = Language::CPP; break;
    case 4: vLanguage = Language::Pascal; break;
    case 5: vLanguage = Language::CPP11; break;
    default: vLanguage = Language::Other; break;
    }
}


void SubmissionMessage::setRuntime(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    vRuntime = v;
}

void SubmissionMessage::setMemory(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    vMemory = v;
}

