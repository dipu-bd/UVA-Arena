#include "usersubmission.h"

using namespace uhuntqt;

UserSubmission::UserSubmission()
{

}

UserSubmission::UserSubmission(const QJsonArray& data)
{
    loadData(data);
}

bool UserSubmission::isInQueue() const
{
    return (mVerdict == Verdict::InQueue);
}

bool UserSubmission::isAccepted() const
{
    return (mVerdict == Verdict::Accepted);
}

void UserSubmission::loadData(const QJsonArray& data)
{
    //Submission ID
    setSubmissionID(data[0].toInt());
    //Problem ID
    setProblemID(data[1].toInt());
    //Verdict ID
    setVerdict(data[2].toInt());
    //Runtime
    setRuntime(data[3].toInt());
    //Submission Time (UNIX time stamp)
    setSubmissionTime(data[4].toInt());
    //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
    Language(data[5].toInt());
    //Submission Rank
    setRank(data[6].toInt());
}


void UserSubmission::setVerdict(int v)
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

void UserSubmission::setRuntime(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    mRuntime = v;
}

void UserSubmission::setLanguage(int v)
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
