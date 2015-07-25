#pragma once

#include <QtCore/QString>
#include <QtCore/QJsonArray>
#include <QtCore/QJsonObject>

#include "uvalib_global.h"

namespace uva
{

    struct UVA_EXPORT UserRank
    {
        /*!
            \brief Creates a UserRank from a QJsonObject.

            \param[in] jsonObject QJsonObject containing the UserRank data.

            \return A UserRank struct with values filled in from jsonObject.
        */
        static UserRank fromJsonObject(const QJsonObject& jsonObject);


        //rank : The rank of the user
        int Rank;
        //old : Non zero if the user is an old UVa user that hasn't migrated
        bool IsMigratedAccount;
        //name : The name of the user
        QString FullName;
        //username : The username of the user
        QString UserName;
        //ac : The number of accepted problems
        int AcceptedSubmissionsCount;
        //nos : The number of submissions of the user
        int TotalSubmissionsCount;

        //activity : The number of accepted problems of the user
        //past 2 days activity
        int PastTwoDaysActivity;
        //past 7 days activity
        int PastWeekActivity;
        //past 31 days activity
        int PastMonthActivity;
        //past 3 months activity
        int PastThreeMonthsActivity;
        //past 1 years activity
        int PastYearActivity;
    };

}
