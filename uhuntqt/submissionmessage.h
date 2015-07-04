#pragma once

#include <QtCore/QString>
#include <QtCore/QJsonObject>

#include "uhuntqt_global.h"
#include "enums.h"

namespace uhunqt
{

    class UHUNTQT_EXPORT SubmissionMessage
    {
    public:
        SubmissionMessage();
        SubmissionMessage(const QJsonObject& data);

        void loadData(const QJsonObject& data);

        //sid: Submission ID
        int getSubmissionID() const { return mSubmissionID; }
        void setSubmissionID(int v) { mSubmissionID = v; }
        //uid: user id
        int getUserID() const { return mUserID; }
        void setUserID(int v) { mUserID = v; }
        //pid: Problem ID
        int getProblemID() const { return mProblemID; }
        void setProblemID(int v) { mProblemID = v; }
        //ver: Verdict ID
        Verdict getVerdict() const { return mVerdict; }
        void setVerdict(Verdict v) { mVerdict = v; }
        void setVerdict(int v);
        //lan: Language ID
        Language getLanguage() const { return mLanguage; }
        void setLanguage(Language v) { mLanguage = v; }
        void setLanguage(int v);
        //run : Runtime
        int getRuntime() const { return mRuntime; }
        void setRuntime(int v);
        //mem: Memory taken
        int getMemory() const { return mMemory; }
        void setMemory(int v);
        //rank: Submission Rank
        int getRank() const { return mRank; }
        void setRank(int v) { mRank = v; }
        //sbt: Submission Time (UNIX time stamp)
        int getSubmissionTime() const { return vSubmissionTime; }
        void setSubmissionTime(int v) { vSubmissionTime = v; }
        //name: full username
        QString getFullName() const { return mFullName; }
        void setFullName(QString v) { mFullName = v; }
        //uname: user name
        QString getUserName() const { return mUserName; }
        void setUserName(QString v) { mUserName = v; }

        //problem number
        int getProblemNumber() const { return mProblemNumber; }
        void setProblemNumber(int v) { mProblemNumber = v; }
        //problem title
        QString getProblemTitle() const { return mProblemTitle; }
        void setProblemTitle(QString v) { mProblemTitle = v; }

    private:
        //sid: Submission ID
        int mSubmissionID;
        //uid: user id
        int mUserID;
        //pid: Problem ID
        int mProblemID;
        //ver: Verdict ID
        Verdict mVerdict;
        //lan: Language ID
        Language mLanguage;
        //run : Runtime
        int mRuntime;
        //mem: Memory taken
        int mMemory;
        //rank: Submission Rank
        int mRank;
        //sbt: Submission Time (UNIX time stamp)
        int vSubmissionTime;
        //name: full username
        QString mFullName;
        //uname: user name
        QString mUserName;

        //problem number
        int mProblemNumber;
        //problem title
        QString mProblemTitle;
    };

}
