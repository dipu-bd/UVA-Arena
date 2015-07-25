#include "usersubmission.h"
#include <QVariant>

using namespace uva;

Submission Submission::fromJsonArray(const QJsonArray& data)
{
    Submission submission;

    submission.SubmissionID = data[0].toInt();
    submission.ProblemID = data[1].toInt();
    submission.SubmissionVerdict = (Submission::Verdict)data[2].toInt();
    submission.Runtime = data[3].toInt();
    //Submission Time (UNIX time stamp)
    submission.TimeSubmitted = data[4].toVariant().toULongLong();
    //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
    submission.SubmissionLanguage = (Submission::Language)data[5].toInt();
    submission.Rank = data[6].toInt();

    return submission;
}


Submission Submission::fromJsonObject(const QJsonObject& data)
{
    Submission submission;

    //sid: Submission ID
    submission.SubmissionID = data["sid"].toInt();
    //pid: Problem ID
    submission.ProblemID = data["pid"].toInt();
    //ver: Verdict ID
    submission.SubmissionVerdict = (Verdict)data["ver"].toInt();
    //run : Runtime
    submission.Runtime = data["run"].toInt();
    //sbt: Submission Time (UNIX time stamp)
    submission.TimeSubmitted = data["sbt"].toVariant().toULongLong();
    //lan: Language ID
    submission.SubmissionLanguage = (Language)data["lan"].toInt();
    //rank: Submission Rank
    submission.Rank = data["rank"].toInt();

    return submission;
}
