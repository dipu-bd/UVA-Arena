#pragma once

#include <QString>
#include <QJsonObject>

#include "uvalib_global.h"
#include "enums.h"

namespace uva
{

    class UVA_EXPORT SubmissionMessage
    {
    public:
        SubmissionMessage();

        /*!
            \brief Converts the QJsonObject into a SubmissionMessage object.

            \param[in] data SubmissionMessage data in JSON format.
            \return A new SubmissionMessage object.
         */
        static SubmissionMessage fromJsonObject(const QJsonObject& data);

        /*!
            \brief Gets the id of submission.
         */
		qint64 getSubmissionID() const { return mSubmissionID; }

        /*!
           \brief Sets the id of submission.
           \param[in] id ID of Submission.
         */
		void setSubmissionID(qint64 id) { mSubmissionID = id; }

        /*!
            \brief Gets the id of user.
         */
        int getUserID() const { return mUserID; }

        /*!
           \brief Sets the id of the user.

           \param[in] id ID of the user.
         */
        void setUserID(int id) { mUserID = id; }

        /*!
            \brief Gets the id of submitted problem.
         */
        int getProblemID() const { return mProblemID; }        
        /*!
            \brief Sets the id of submitted problem.

            \param[in] id ID of problem.
         */
        void setProblemID(int v) { mProblemID = v; }

        /*!
            \brief Gets the verdict of submission.
         */
        Verdict getVerdict() const { return mVerdict; }

        /*!
            \brief Sets the verdict enum value for the submission.

            \param[in] verdict Value from the Verdict enum.
         */
        void setVerdict(Verdict verdict) { mVerdict = verdict; }

        /*!
            \brief Sets the verdict of the submission.
            \param[in] verict Value from the Verdict enum.
         */
        void setVerdict(int verdict);

        /*!
            \brief Gets the programming language of the submitted code.
         */
        Language getLanguage() const { return mLanguage; }

        /*!
            \brief Sets the programming language of the submitted code.

            \param[in] language Programming language used for the submission.
         */
        void setLanguage(Language language) { mLanguage = language; }

        /*!
            \brief Sets the programming language of the submitted code.

            \param[in] language Programming language used for the submission.
         */
        void setLanguage(int v);

        /*!
            \brief Gets the runtime in milliseconds of the submission.
         */
        int getRuntime() const { return mRuntime; }

        /*!
            \brief Sets the runtime of the submission.
            \param[in] runtime Runtime in milliseconds.
         */
        void setRuntime(int runtime);

        /*!
            \brief Gets the memory needed in bytes of the runtime for this submission.
                   WARNING: This value is no longer supported by UVA OJ.
         */
        int getMemory() const { return mMemory; }

        /*!
            \brief Sets the memory needed on runtime in this submission.
            \param[in] memory Memory in bytes.
         */
        void setMemory(int memory);

        /*!
            \brief Gets the rank of the user for this submission.
         */
        int getRank() const { return mRank; }

        /*!
            \brief Sets the rank of the user in this submission.
            \param[in] rank Rank of user.
         */
        void setRank(int rank) { mRank = rank; }

        /*!
            \brief Gets the time of submission in UNIX timestamp.
         */
		quint64 getSubmissionTime() const { return mSubmissionTime; }

        /*!
           \brief Sets the time of submission in UNIX timestamp.
           \param[in] time Time in UNIX timestamp.
         */
		void setSubmissionTime(quint64 time) { mSubmissionTime = time; }

        /*!
            \brief Gets the full name of the user.
         */
        QString getFullName() const { return mFullName; }

        /*!
            \brief Sets the full name of the user.
            \param[in] fullName Full name of the user.
         */
        void setFullName(QString fullName) { mFullName = fullName; }

        /*!
            \brief Gets the user name.
         */
        QString getUserName() const { return mUserName; }

        /*!
            \brief Sets the user name.
            \param[in] userName The user name.
         */
        void setUserName(QString userName) { mUserName = userName; }

        /*!
            \brief Gets the number of submitted problem.
                   REQUIRES: UvaArenaWidget to be avaiable.
         */
        int getProblemNumber() const { return mProblemNumber; }

        /*!
            \brief Sets the number of submitted problem.
            \param[in] problemNumber Problem number.
         */
        void setProblemNumber(int problemNumber) { mProblemNumber = problemNumber; }

        /*!
            \brief Gets the title of the submitted problem.
                   REQUIRES: UvaArenaWidget to be avaiable.
         */
        QString getProblemTitle() const { return mProblemTitle; }

        /*!
            \brief Sets the title of the submitted problem.
            \param[in] problemTitle Problem title.
         */
        void setProblemTitle(QString problemTitle) { mProblemTitle = problemTitle; }

    private:
        //sid: Submission ID
        qint64 mSubmissionID;
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
        quint64 mSubmissionTime;
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
