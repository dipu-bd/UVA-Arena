#pragma once

#include <QMap>
#include <QHash>
#include <QJsonArray>
#include <QJsonObject>

#include "uvalib_global.h"
#include "problem.h"

namespace uva
{

    struct UVA_EXPORT Category
    {
        ~Category();
        struct CategoryProblem 
        {
            int Number;
            QString Note;
            bool IsStarred;
        };

        /**
            Initializes this object from the given from JSON.

            \param[in]  json    The JSON data.

            \return null if it fails, else a Category*.
        */

        static Category *fromJson(const QByteArray &json);

        /*!
            \brief Creates a CategoryNode from a QJsonObject.

            \param[in] jsonObject QJsonObject containing the Submission data.

            \return A CategoryNode struct with values filled in from jsonObject.
        */
        static Category *fromJsonObject(const QJsonObject& jsonObject);

        QString Name;                            ///< The name
        QString Note;                            ///< The note
        Category *Parent;                        ///< The parent
        QMap<int, CategoryProblem*> Problems;    ///< The problems. Key = problem number
        QHash<QString, Category*> Branches;      ///< The branches. Key = Name

    };
}
