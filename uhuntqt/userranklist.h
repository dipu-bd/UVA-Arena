#pragma once

#include <QtCore/QString>
#include <QtCore/QJsonArray>
#include <QtCore/QJsonObject>

#include "uhuntqt_global.h"
#include "enums.h"

namespace uhuntqt
{

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
        bool isOld() { return mOld; }
        void setOld(bool v) { mOld = v; }
        //name : The name of the user
        QString getFullName() { return mFullName; }
        void setFullName(QString v) { mFullName = v; }
        //username : The username of the user
        QString getUserName() { return mUserName; }
        void setUserName(QString v) { mUserName = v; }
        //ac : The number of accepted problems
        int getAcceptedCount() { return mAccepted; }
        void setAcceptedCount(int v) { mAccepted = v; }
        //nos : The number of submissions of the user
        int getTotalSubmission() { return mTotalSubmission; }
        void setTotalSubmission(int v) { mTotalSubmission = v; }

        //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
        void setActivity(const QJsonArray& data);
        //past 2 days activity
        int getPast2day() { return m2days; }
        void setPast2day(int v) { m2days = v; }
        //past 7 days activity
        int getPast7day() { return m7day; }
        void setPast7day(int v) { m7day = v; }
        //past 31 days activity
        int getPast31day() { return m31day; }
        void setPast31day(int v) { m31day = v; }
        //past 3 months ctivity
        int getPast3month() { return m3month; }
        void setPast3month(int v) { m3month = v; }
        //past 1 years activity
        int getPast1year() { return m1year; }
        void setPast1year(int v) { m1year = v; }

    private:
        //rank : The rank of the user
        int mRank;
        //old : Non zero if the user is an old UVa user that hasn't migrate
        bool mOld;
        //name : The name of the user
        QString mFullName;
        //username : The username of the user
        QString mUserName;
        //ac : The number of accepted problems
        int mAccepted;
        //nos : The number of submissions of the user
        int mTotalSubmission;

        //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
        //past 2 days activity
        int m2days;
        //past 7 days activity
        int m7day;
        //past 31 days activity
        int m31day;
        //past 3 months ctivity
        int m3month;
        //past 1 years activity
        int m1year;
    };

}
