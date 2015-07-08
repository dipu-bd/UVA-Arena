#pragma once

#include <QSet>
#include <QString>
#include <QMap>
#include <QJsonObject>
#include <QJsonArray>
#include <QJsonDocument>

#include "uvalib_global.h"
#include "usersubmission.h"

namespace uva
{

    class UVA_EXPORT UserInfo
    {

    public:
        UserInfo();
        UserInfo(const QByteArray& data);
        UserInfo(const QJsonObject& data);

        //load data from the given array
        void loadData(const QByteArray& data);
        void loadData(const QJsonObject& data);

        //user id
        int getUserId() const { return mUserID; }
        void setUserId(int v) { mUserID = v; }
        //fullname
        QString getFullName() const { return mFullName; }
        void setFullName(QString v) { mFullName = v; }
        //username
        QString getUserName() const { return mUserName; }
        void setUserName(QString v) { mUserName = v; }

        //last submission id
        int getLastSubmissionID() const { return mLastSubmissionID; }

        //submission list
        QMap<int, UserSubmission>& getSubmissionList() { return mSubmissions; }
        const QMap<int, UserSubmission>& getSubmissionList() const { return mSubmissions; }
        UserSubmission& getSubmission(int problemNumber) { return mSubmissions[problemNumber]; }
        const UserSubmission getSubmission(int problemNumber) const { return mSubmissions[problemNumber]; }
        int getTotalSubmissionCount() const { return mSubmissions.count(); }
        //accepted list
        QSet<int>& getSolvedList() { return mSolved; }
        int getTotalSolvedCount() const { return mSolved.count(); }
        //tried but not accepted list
        QSet<int>& getTriedButFailedList() { return mTriedButFailed; }
        int getTriedButFailedCount() const { return mTriedButFailed.count(); }

        //check if a problem is accepted by this user
        bool isAccepted(int problemNumber) const;
        //check if a problem is tried by this user
        bool isTried(int problemNumber) const;
        //check if a problem is tried but not solved by this user
        bool isTriedButNotSolved(int problemNumber) const;

    private:        
        //user id
        int mUserID;
        //full name
        QString mFullName;
        //user name
        QString mUserName;
        //submission list for fast access | SubmissionId -> UserSubmission
        QMap<int, UserSubmission> mSubmissions;

        //last submission id that has a verdict other than in-queue
        int mLastSubmissionID;

        //accepted
        QSet<int> mSolved;
        //tried but not accepted
        QSet<int> mTriedButFailed;
    };

}
