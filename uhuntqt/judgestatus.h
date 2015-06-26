#ifndef JUDGESTATUS_H
#define JUDGESTATUS_H

#include "submissionmessage.h"

class JudgeStatus : SubmissionMessage
{
public:
    JudgeStatus();
    JudgeStatus(const QJsonObject& data);

    void loadData(const QJsonObject& data);

    //id: Submission ID
    qint64 getId() const { return vID; }
    void setId(qint64 v) { vID = v; }
    //type: type of the submission
    QString getType() const { return vType; }
    void setType(QString v) { vType = v; }
private:
    //id: Global submission id
    qint64 vID;
    //type: type of submission
    QString vType;
};

#endif // JUDGESTATUS_H


