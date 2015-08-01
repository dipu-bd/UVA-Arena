#pragma once

#include <QMap>
#include <QHash>
#include <QJsonArray>
#include <QJsonObject>

#include "uvalib_global.h"
#include "problem.h"

#include <memory>

namespace uva
{

    struct UVA_EXPORT Category
    {

        struct CategoryProblem
        {
            CategoryProblem(int number, QString note, bool starred)
                : Number(number), Note(note), IsStarred(starred)
            {

            }
            int Number;
            QString Note;
            bool IsStarred;
        };

        /**
            Initializes this object from the given from JSON.

            \param[in]  json    The JSON data.

            \return null if it fails, else a Category*.
        */
        static std::shared_ptr<Category> fromJson(const QByteArray &json);

        /*!
            \brief Creates a CategoryNode from a QJsonObject.

            \param[in] jsonObject QJsonObject containing the Submission data.

            \return A CategoryNode struct with values filled in from jsonObject.
        */
        static std::shared_ptr<Category> fromJsonObject(const QJsonObject& jsonObject);

        QString Name;                            ///< The name
        QString Note;                            ///< The note
        std::weak_ptr<Category> Parent;                        ///< The parent
        QMap<int, std::shared_ptr<CategoryProblem> > Problems;    ///< The problems. Key = problem number
        QHash<QString, std::shared_ptr<Category> > Branches;      ///< The branches. Key = Name

    };
}
