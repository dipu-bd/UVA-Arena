#include "userranklist.h"

using namespace uhuntqt;

UserRanklist::UserRanklist()
{

}

UserRanklist::UserRanklist(const QJsonObject& data)
{
    loadData(data);
}

void UserRanklist::loadData(const QJsonObject& data)
{
    //rank : The rank of the user
    setRank(data["rank"].toInt());
    //old : Non zero if the user is an old UVa user that hasn't migrate
    setOld(data["old"].toBool());
    //name : The name of the user
    setFullName(data["name"].toString());
    //username : The username of the user
    setUserName(data["username"].toString());
    //ac : The number of accepted problems
    setAcceptedCount(data["ac"].toInt());
    //nos : The number of submissions of the user
    setTotalSubmission(data["nos"].toInt());
    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    setActivity(data["activity"].toArray());
}

void UserRanklist::setActivity(const QJsonArray& data)
{
    setPast2day(data[0].toInt());
    setPast7day(data[1].toInt());
    setPast31day(data[2].toInt());
    setPast3month(data[3].toInt());
    setPast1year(data[4].toInt());
}
