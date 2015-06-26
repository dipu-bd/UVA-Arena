#ifndef SUBMISSIONMESSAGE_H
#define SUBMISSIONMESSAGE_H

#include <QtCore/QString>
#include <QtCore/QJsonObject>

#include "enums.h"

class SubmissionMessage
{    
public:
    SubmissionMessage();
    SubmissionMessage(const QJsonObject& data);

    void loadData(const QJsonObject& data);

    //sid: Submission ID
    int getSubmissionID() const { return vSubmissionID; }
    void setSubmissionID(int v) { vSubmissionID = v; }
    //uid: user id
    int getUserID() const { return vUserID; }
    void setUserID(int v) { vUserID = v; }
    //pid: Problem ID
    int getProblemID() const { return vProblemID; }
    void setProblemID(int v) { vProblemID = v; }
    //ver: Verdict ID
    Verdict getVerdict() const { return vVerdict; }
    void setVerdict(Verdict v) { vVerdict = v; }
    void setVerdict(int v);
    //lan: Language ID
    Language getLanguage() const { return vLanguage; }
    void setLanguage(Language v) { vLanguage = v; }
    void setLanguage(int v);
    //run : Runtime
    int getRuntime() const { return vRuntime; }
    void setRuntime(int v);
    //mem: Memory taken
    int getMemory() const { return vMemory; }
    void setMemory(int v);
    //rank: Submission Rank
    int getRank() const { return vRank; }
    void setRank(int v) { vRank = v; }
    //sbt: Submission Time (UNIX time stamp)
    int getSubmissionTime() const { return vSubmissionTime; }
    void setSubmissionTime(int v) { vSubmissionTime = v; }
    //name: full username
    QString getFullName() const { return vFullName; }
    void setFullName(QString v) { vFullName = v; }
    //uname: user name
    QString getUserName() const { return vUserName; }
    void setUserName(QString v) { vUserName = v; }

    //problem number
    int getProblemNumber() const { return vProblemNumber; }
    void setProblemNumber(int v) { vProblemNumber = v; }
    //problem title
    QString getProblemTitle() const { return vProblemTitle; }
    void setProblemTitle(QString v) { vProblemTitle = v; }

private:
    //sid: Submission ID
    int vSubmissionID;
    //uid: user id
    int vUserID;
    //pid: Problem ID
    int vProblemID;
    //ver: Verdict ID
    Verdict vVerdict;
    //lan: Language ID
    Language vLanguage;
    //run : Runtime
    int vRuntime;
    //mem: Memory taken
    int vMemory;
    //rank: Submission Rank
    int vRank;
    //sbt: Submission Time (UNIX time stamp)
    int vSubmissionTime;
    //name: full username
    QString vFullName;
    //uname: user name
    QString vUserName;

    //problem number
    int vProblemNumber;
    //problem title
    QString vProblemTitle;
};

#endif // SUBMISSIONMESSAGE_H
