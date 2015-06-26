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
    return (vVerdict == Verdict::InQueue);
}

bool UserSubmission::isAccepted() const
{
    return (vVerdict == Verdict::Accepted);
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

void UserSubmission::setRuntime(int v)
{
    if(v < 0 || v >= 1000000000) v = 0;
    vRuntime = v;
}

void UserSubmission::setLanguage(int v)
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
