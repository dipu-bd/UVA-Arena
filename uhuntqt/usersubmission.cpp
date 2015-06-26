#include "usersubmission.h"

UserSubmission::UserSubmission()
{

}

UserSubmission::UserSubmission(const QJsonArray& data)
{
    loadData(data);
}

bool UserSubmission::isInQueue() const
{
    return (_ver == Verdict::InQueue);
}

bool UserSubmission::isAccepted() const
{
    return (_ver == Verdict::Accepted);
}

void UserSubmission::loadData(const QJsonArray& data)
{
    //Submission ID
    setSid(data[0].toInt());
    //Problem ID
    setPid(data[1].toInt());
    //Verdict ID
    setVer(data[2].toInt());
    //Runtime
    setRun(data[3].toInt());
    //Submission Time (UNIX time stamp)
    setSbt(data[4].toInt());
    //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
    setLan(data[5].toInt());
    //Submission Rank
    setRank(data[6].toInt());
}


void UserSubmission::setVer(int v)
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

void UserSubmission::setRun(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    _run = v;
}

void UserSubmission::setLan(int v)
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
