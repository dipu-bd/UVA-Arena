#pragma once

#include <QtCore/QString>
#include <QtCore/QJsonArray>

#include "uvalib_global.h"
#include "enums.h"
#include "uhuntdatabase.h"

namespace uva
{

    class UVA_EXPORT UserSubmission
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
        int getSubmissionID() const { return mSubmissionID; }
        void setSubmissionID(int v) { mSubmissionID = v; }
        //Problem ID
        int getProblemID() const { return mProblemID; }
        void setProblemID(int v) { mProblemID = v; }
        //Verdict ID
        Verdict getVerdict() const { return mVerdict; }
        void setVerdict(Verdict v) { mVerdict = v; }
        void setVerdict(int v);
        //Runtime
        int getRuntime() const { return mRuntime; }
        void setRuntime(int v);
        //Submission Time (UNIX time stamp)
        int getSubmissionTime() const { return mSubmissionTime; }
        void setSubmissionTime(int v) { mSubmissionTime = v; }
        //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
        Language getLanguage() const { return mLanguage; }
        void setLanguage(Language v) { mLanguage = v; }
        void setLanguage(int v);
        //Submission Rank
        int getRank() const { return mRank; }
        void setRank(int v) { mRank = v; }

        //problem number
        int getProblemNumber() const { return mProblemNumber; }
        void setProblemNumber(int v) { mProblemNumber = v; }
        //problem title
        QString getProblemTitle() const { return mProblemTitle; }
        void setProblemTitle(QString v) { mProblemTitle = v; }

        bool operator < (const UserSubmission& rhs) const
        {
            return mSubmissionID < rhs.mSubmissionID;
        }
    private:
        //Submission ID
        int mSubmissionID;
        //Problem ID
        int mProblemID;
        //Verdict ID
        Verdict mVerdict;
        //Runtime
        int mRuntime;
        //Submission Time (UNIX time stamp)
        int mSubmissionTime;
        //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
        Language mLanguage;
        //Submission Rank
        int mRank;

        //problem number
        int mProblemNumber;
        //problem title
        QString mProblemTitle;
    };

}
