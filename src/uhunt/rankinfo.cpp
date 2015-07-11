#include "rankinfo.h"

using namespace uva;

RankInfo::RankInfo()
{    
}

RankInfo RankInfo::fromJsonObject(const QJsonObject& data)
{
    RankInfo rank;

    //rank : The rank of the user
    rank.setRank(data["rank"].toInt());
    //old : Non zero if the user is an old UVa user that hasn't migrate
    rank.setOld(data["old"].toBool());
    //name : The name of the user
    rank.setFullName(data["name"].toString());
    //username : The username of the user
    rank.setUserName(data["username"].toString());
    //ac : The number of accepted problems
    rank.setAcceptedCount(data["ac"].toInt());
    //nos : The number of submissions of the user
    rank.setTotalSubmission(data["nos"].toInt());
    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    rank.setActivity(data["activity"].toArray());

    return rank;
}

void RankInfo::setActivity(const QJsonArray& data)
{
    setPast2day(data[0].toInt());
    setPast7day(data[1].toInt());
    setPast31day(data[2].toInt());
    setPast3month(data[3].toInt());
    setPast1year(data[4].toInt());
}
