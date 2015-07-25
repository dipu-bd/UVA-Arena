#pragma once

#include "usersubmission.h"
#include <QVariant>
#include <QJsonObject>

namespace uva
{

    struct UVA_EXPORT LiveEvent
    {
        // Use the LiveEventID field as the key for data structures
        typedef quint64 Key;

        /*!
            \brief Creates a LiveEvent from a QJsonObject.

            \param[in] jsonObject QJsonObject containing the LiveEvent data.

            \return A LiveEvent struct with values filled in from jsonObject.
        */
        static LiveEvent fromJsonObject(const QJsonObject& jsonObject);

        //id: Live Event id
        Key LiveEventID;
        //type: type of submission
        QString Type;

        UserSubmission UserSubmission;
    };

}
