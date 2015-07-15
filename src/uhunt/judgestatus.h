#pragma once

#include "submissionmessage.h"
#include <QVariant>

namespace uva
{

    class UVA_EXPORT JudgeStatus : public SubmissionMessage
    {
    public:
        JudgeStatus();

        static JudgeStatus fromJsonObject(const QJsonObject& data);

        /*!
          \brief Gets the global submission id.
         */
        quint64 getId() const { return mID; }
        /*!
          \brief Sets the global submission id.
          \param[v] Global submission id.
         */
        void setId(quint64 v) { mID = v; }

        /*!
          \brief Get the type of the user.
                  "old" means the user did not migrate into new system of UVA OJ.
         */
        QString getType() const { return mType; }
        /*!
          \brief Sets the type of the user.
          \param[v] Type of the user.
         */
        void setType(QString v) { mType = v; }

    private:
        //id: Global submission id
        quint64 mID;
        //type: type of submission
        QString mType;
    };

}
