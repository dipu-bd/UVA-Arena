#ifndef USERSUBMISSION_H
#define USERSUBMISSION_H

#include <QtCore/QString>
#include <QtCore/QJsonArray>

#include "enums.h"

class UserSubmission
{    
public:
    UserSubmission();
    UserSubmission(const QJsonArray& data);

    void loadData(const QJsonArray& data);

    // true if the verdict is in queue
    bool isInQueue() const;
    // true if the verdict is accepted
    bool isAccepted() const;

    //Submission ID
    int getSubmissionID() const { return vSubmissionID; }
    void setSubmissionID(int v) { vSubmissionID = v; }
    //Problem ID
    int getProblemID() const { return vProblemID; }
    void setProblemID(int v) { vProblemID = v; }
    //Verdict ID
    Verdict getVerdict() const { return vVerdict; }
    void setVerdict(Verdict v) { vVerdict = v; }
    void setVerdict(int v);
    //Runtime
    int getRuntime() const { return vRuntime; }
    void setRuntime(int v);
    //Submission Time (UNIX time stamp)
    int getSubmissionTime() const { return vSubmissionTime; }
    void setSubmissionTime(int v) { vSubmissionTime = v; }
    //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
    Language getLanguage() const { return vLanguage; }
    void setLanguage(Language v) { vLanguage = v; }
    void setLanguage(int v);
    //Submission Rank
    int getRank() const { return vRank; }
    void setRank(int v) { vRank = v; }

    //problem number
    int getProblemNumber() const { return vProblemNumber; }
    void setProblemNumber(int v) { vProblemNumber = v; }
    //user name
    QString getUserName() const { return vUserName; }
    void setUserName(QString v) { vUserName = v; }
    //full username
    QString getFullName() const { return vFullName; }
    void setFullName(QString v) { vFullName = v; }
    //problem title
    QString getProblemTitle() const { return vProblemTitle; }
    void setProblemTitle(QString v) { vProblemTitle = v; }

private:
    //Submission ID
    int vSubmissionID;
    //Problem ID
    int vProblemID;
    //Verdict ID
    Verdict vVerdict;
    //Runtime
    int vRuntime;
    //Submission Time (UNIX time stamp)
    int vSubmissionTime;
    //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
    Language vLanguage;
    //Submission Rank
    int vRank;

    //problem number
    int vProblemNumber;
    //user name
    QString vUserName;
    //full username
    QString vFullName;
    //problem title
    QString vProblemTitle;
};

#endif // USERSUBMISSION_H
