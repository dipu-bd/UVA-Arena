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
