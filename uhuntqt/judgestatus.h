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
    qint64 getId() const { return _id; }
    void setId(qint64 v) { _id = v; }
    //type: user name
    QString getType() const { return _type; }
    void setType(QString v) { _type = v; }
private:
    //id: Global submission id
    qint64 _id;
    //type: type of submission
    QString _type;
};

#endif // JUDGESTATUS_H


