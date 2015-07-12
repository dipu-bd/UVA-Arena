#include "judgestatus.h"

using namespace uva;

JudgeStatus::JudgeStatus()
{
    setUserID(0);
    setUserName("-");
    setProblemNumber(0);
    setProblemTitle("-");
}

JudgeStatus JudgeStatus::fromJsonObject(const QJsonObject &obj)
{
    JudgeStatus stat;

    //id : global id
    stat.setId(static_cast<qint64>(obj["id"].toDouble()));
    //type: type of submission
    stat.setType(obj["type"].toString());

    //get msg object
    const QJsonObject& data = obj["msg"].toObject();

    //sid: Submission ID
    stat.setSubmissionID(data["sid"].toInt());
    //uid: user id
    stat.setUserID(data["uid"].toInt());
    //pid: Problem ID
    stat.setProblemID(data["pid"].toInt());
    //ver: Verdict ID
    stat.setVerdict(data["ver"].toInt());
    //lan: Language ID
    stat.setLanguage(data["lan"].toInt());
    //run : Runtime
    stat.setRuntime(data["run"].toInt());
    //mem: Memory taken
    stat.setMemory(data["mem"].toInt());
    //rank: Submission Rank
    stat.setRank(data["rank"].toInt());
    //sbt: Submission Time (UNIX time stamp)
    stat.setSubmissionTime(data["sbt"].toInt());
    //name: full username
    stat.setFullName(data["name"].toString());
    //uname: user name
    stat.setUserName(data["uname"].toString());

    /*
      TODO:
        stat.setProblemNumber(...);
        stat.setProblemTitle(...);
    */

    return stat;
}
