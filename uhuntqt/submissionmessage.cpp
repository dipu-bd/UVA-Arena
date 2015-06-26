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
    setSid(data["sid"].toInt());
    //uid: user id
    setUid(data["uid"].toInt());
    //pid: Problem ID
    setPid(data["pid"].toInt());
    //ver: Verdict ID
    setVer(data["ver"].toInt());
    //lan: Language ID
    setLan(data["lan"].toInt());
    //run : Runtime
    setRun(data["run"].toInt());
    //mem: Memory taken
    setMem(data["mem"].toInt());
    //rank: Submission Rank
    setRank(data["rank"].toInt());
    //sbt: Submission Time (UNIX time stamp)
    setSbt(data["sbt"].toInt());
    //name: full username
    setName(data["name"].toString());
    //uname: user name
    setUname(data["uname"].toString());

}

void SubmissionMessage::setVer(int v)
{
    switch(v)
    {
    case 10: _ver = Verdict::SubError; break;
    case 15: _ver = Verdict::CannotBeJudge; break;
    case 20: _ver = Verdict::InQueue; break;
    case 30: _ver = Verdict::CompileError; break;
    case 35: _ver = Verdict::RestrictedFunction; break;
    case 40: _ver = Verdict::RuntimeError; break;
    case 45: _ver = Verdict::OutputLimit; break;
    case 50: _ver = Verdict::TimLimit; break;
    case 60: _ver = Verdict::MemoryLimit; break;
    case 70: _ver = Verdict::WrongAnswer; break;
    case 80: _ver = Verdict::PresentationError; break;
    case 90: _ver = Verdict::Accepted; break;
    default: _ver = Verdict::InQueue; break;
    }
}

void SubmissionMessage::setLan(int v)
{
    switch(v)
    {
    case 1: _lan = Language::C; break;
    case 2: _lan = Language::Java; break;
    case 3: _lan = Language::CPP; break;
    case 4: _lan = Language::Pascal; break;
    case 5: _lan = Language::CPP11; break;
    default: _lan = Language::Other; break;
    }
}


void SubmissionMessage::setRun(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    _run = v;
}

void SubmissionMessage::setMem(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    _mem = v;
}

