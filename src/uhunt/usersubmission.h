#pragma once

#include <QtCore/QString>
#include <QtCore/QJsonArray>

#include "uvalib_global.h"
#include "enums.h"

namespace uva
{

    class UVA_EXPORT UserSubmission
    {
    public:
        UserSubmission();

        /*!
           \brief Gets a new UserSubmission from QJsonArray data.
           \param[data] QJsonArray data containing information.
           \return New UserSubmission object.
         */
        static UserSubmission fromJsonArray(const QJsonArray& data);

        /*!
           \brief Checks if the submission is in judge queue.
         */
        bool isInQueue() const;
        /*!
           \brief Checks whether the verdict of the submission is accepted.
         */
        bool isAccepted() const;

        /*!
           \brief Gets the id of the submission.
         */
        int getSubmissionID() const { return mSubmissionID; }
        /*!
           \brief Sets the id of the submission
           \param[v] Submission id.
         */
        void setSubmissionID(int v) { mSubmissionID = v; }


        /*!
           \brief Gets the id of the submitted problem.
         */
        int getProblemID() const { return mProblemID; }
        /*!
           \brief Sets the id of the submitted problem.
           \param[v] Problem id.
         */
        void setProblemID(int v) { mProblemID = v; }

        /*!
           \brief Gets the verdict of the submission.
         */
        Verdict getVerdict() const { return mVerdict; }
        /*!
           \brief Sets the verdict of the submission.
           \param[v] Verdict.
         */
        void setVerdict(Verdict v) { mVerdict = v; }
        /*!
           \brief Sets the verdict of the submission.
           \param[v] Verdict.
         */
        void setVerdict(int v);


        /*!
           \brief Get the runtime of the submission in miliseconds.
         */
        int getRuntime() const { return mRuntime; }
        /*!
           \brief Sets the runtime of the submission.
           \param[v] Runtime.
         */
        void setRuntime(int v);

        /*!
           \brief Gets the time of submission in Unix Timestamp.
         */
        int getSubmissionTime() const { return mSubmissionTime; }
        /*!
           \brief Sets the time of submission.
           \param[v] Time in Unix Timestamp.
         */
        void setSubmissionTime(int v) { mSubmissionTime = v; }

        /*!
           \brief Gets the language of the submitted code
                  Language id is one of these values: (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11).
         */
        Language getLanguage() const { return mLanguage; }
        /*!
           \brief Sets the language of the submitted code.
           \param[v] Language id (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11).
         */
        void setLanguage(Language v) { mLanguage = v; }
        /*!
           \brief Sets the language of the submitted code.
           \param[v] Language id (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11).
         */
        void setLanguage(int v);

        /*!
           \brief Gets the rank of user in the submitted problem.
         */
        int getRank() const { return mRank; }
        /*!
           \brief Sets the rank of user in the submitted problem.
           \param[v] Rank.
         */
        void setRank(int v) { mRank = v; }

        /*!
           \brief Gets the number of the submitted problem.
                  Requires ProblemMap available.
         */
        int getProblemNumber() const { return mProblemNumber; }
        /*!
           \brief Sets the problem number of submitted problem.
           \param[v] Problem number.
         */
        void setProblemNumber(int v) { mProblemNumber = v; }

        /*!
           \brief Gets the title of the submitted problem.
                  Requires ProblemMap available.
         */
        QString getProblemTitle() const { return mProblemTitle; }
        /*!
           \brief Sets the problem title of submitted problem.
           \param[v] Problem title.
         */
        void setProblemTitle(QString v) { mProblemTitle = v; }

        /*!
           \brief Compares two UserSubmission objects
         */
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
