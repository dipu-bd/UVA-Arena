#pragma once

#include <QString>
#include <QJsonObject>

#include "uvalib_global.h"

#include "submission.h"

namespace uva
{
    // #TODO make member variables for all Uhunt classes camelCase
    struct UVA_EXPORT UserSubmission
    {

        /*!
            \brief Creates a UserSubmission from a QJsonObject.

            \param[in] jsonObject QJsonObject containing the UserSubmission data.

            \return A UserSubmission struct with values filled in from jsonObject.
        */
        static UserSubmission fromJsonObject(const QJsonObject& data);

        // Full name of the submitter
        QString FullName;
        // User name of the submitter
        QString UserName;
        // UserId of the submitter
        quint64 UserID;

        Submission Submission;
    };
}
