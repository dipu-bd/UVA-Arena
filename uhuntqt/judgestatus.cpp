#include "judgestatus.h"

JudgeStatus::JudgeStatus()
{

}

JudgeStatus::JudgeStatus(const QJsonObject &data)
{
    loadData(data);
}

void JudgeStatus::loadData(const QJsonObject &obj)
{
    //id : global id
    setId(static_cast<qint64>(obj["id"].toDouble()));
    //type: type of submission
    setType(obj["type"].toString());

    //get msg object
    const QJsonObject& data = obj["msg"].toObject();

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
