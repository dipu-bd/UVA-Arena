#include "userrank.h"

#include <QJsonArray>

using namespace uva;

UserRank UserRank::fromJsonObject(const QJsonObject& jsonObject)
{
    UserRank userRank;

    //rank : The rank of the user
    userRank.Rank = jsonObject["rank"].toInt();
    //old : Non zero if the user is an old UVa user that hasn't migrate
    userRank.IsMigratedAccount = jsonObject["old"].toBool();
    //name : The name of the user
    userRank.FullName = jsonObject["name"].toString();
    //username : The username of the user
    userRank.UserName = jsonObject["username"].toString();
    //ac : The number of accepted problems
    userRank.AcceptedSubmissionsCount = jsonObject["ac"].toInt();
    //nos : The number of submissions of the user
    userRank.TotalSubmissionsCount = jsonObject["nos"].toInt();
    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    QJsonArray activity = jsonObject["activity"].toArray();
    userRank.PastTwoDaysActivity = activity[0].toInt();
    userRank.PastWeekActivity = activity[1].toInt();
    userRank.PastMonthActivity = activity[2].toInt();
    userRank.PastThreeMonthsActivity = activity[3].toInt();
    userRank.PastYearActivity = activity[4].toInt();

    return userRank;
}
