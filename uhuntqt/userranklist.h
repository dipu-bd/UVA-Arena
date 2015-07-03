#pragma once

#include <QtCore/QString>
#include <QtCore/QJsonArray>
#include <QtCore/QJsonObject>

#include "uhuntqt_global.h"
#include "enums.h"

class UHUNTQT_EXPORT UserRanklist
{
public:
    UserRanklist();
    UserRanklist(const QJsonObject& data);

    //load data from json object
    void loadData(const QJsonObject& data);

    //rank : The rank of the user
    int getRank() { return mRank; }
    void setRank(int v) { mRank = v; }
    //old : Non zero if the user is an old UVa user that hasn't migrate
    bool isOld() { return vOld; }
    void setOld(bool v) { vOld = v; }
    //name : The name of the user
    QString getFullName() { return mFullName; }
    void setFullName(QString v) { mFullName = v; }
    //username : The username of the user
    QString getUserName() { return mUserName; }
    void setUserName(QString v) { mUserName = v; }
    //ac : The number of accepted problems
    int getAcceptedCount() { return vAccepted; }
    void setAcceptedCount(int v) { vAccepted = v; }
    //nos : The number of submissions of the user
    int getTotalSubmission() { return vTotalSubmission; }
    void setTotalSubmission(int v) { vTotalSubmission = v; }

    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    void setActivity(const QJsonArray& data);
    //past 2 days activity
    int getPast2day() { return v2days; }
    void setPast2day(int v) { v2days = v; }
    //past 7 days activity
    int getPast7day() { return v7day; }
    void setPast7day(int v) { v7day = v; }
    //past 31 days activity
    int getPast31day() { return v31day; }
    void setPast31day(int v) { v31day = v; }
    //past 3 months ctivity
    int getPast3month() { return v3month; }
    void setPast3month(int v) { v3month = v; }
    //past 1 years activity
    int getPast1year() { return v1year; }
    void setPast1year(int v) { v1year = v; }

private:
    //rank : The rank of the user
    int mRank;
    //old : Non zero if the user is an old UVa user that hasn't migrate
    bool vOld;
    //name : The name of the user
    QString mFullName;
    //username : The username of the user
    QString mUserName;
    //ac : The number of accepted problems
    int vAccepted;
    //nos : The number of submissions of the user
    int vTotalSubmission;

    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    //past 2 days activity
    int v2days;
    //past 7 days activity
    int v7day;
    //past 31 days activity
    int v31day;
    //past 3 months ctivity
    int v3month;
    //past 1 years activity
    int v1year;
};
