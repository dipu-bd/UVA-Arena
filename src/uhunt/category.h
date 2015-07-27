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

        /*!
            \brief Creates a CategoryNode from a QJsonObject.

            \param[in] jsonObject QJsonObject containing the Submission data.

            \return A CategoryNode struct with values filled in from jsonObject.
        */
        static Category *fromJsonObject(const QJsonObject& jsonObject);

        QString Name;
        QString Note;
        Category *Parent;
        // Key = Problem Number
        QMap<int, CategoryProblem*> Problems;
        // Key = Category Name
        QHash<QString, Category*> Branches;

    };
}
