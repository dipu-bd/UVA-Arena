#pragma once

#include "submissionmessage.h"

namespace uhunqt
{

    class UHUNTQT_EXPORT JudgeStatus : public SubmissionMessage
    {
    public:
        JudgeStatus();
        JudgeStatus(const QJsonObject& data);

        void loadData(const QJsonObject& data);

        //id: Submission ID
        qint64 getId() const { return mID; }
        void setId(qint64 v) { mID = v; }
        //type: type of the submission
        QString getType() const { return mType; }
        void setType(QString v) { mType = v; }
    private:
        //id: Global submission id
        qint64 mID;
        //type: type of submission
        QString mType;
    };

}
