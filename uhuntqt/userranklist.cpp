#include "userranklist.h"

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
    setName(data["name"].toString());
    //username : The username of the user
    setUsername(data["username"].toString());
    //ac : The number of accepted problems
    setAc(data["ac"].toInt());
    //nos : The number of submissions of the user
    setNos(data["nos"].toInt());
    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    setActivity(data["activity"].toArray());
}

void UserRanklist::setActivity(const QJsonArray& data)
{
    set2day(data[0].toInt());
    set7day(data[1].toInt());
    set31day(data[2].toInt());
    set3month(data[3].toInt());
    set1year(data[4].toInt());
}
