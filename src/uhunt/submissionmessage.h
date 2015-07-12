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
           \brief Converts the QJsonObject type data into SubmissionMessage object.
           \param[data] SubmissionMessage data in JSON format.
           \return A new SubmissionMessage object.
         */
        static SubmissionMessage fromJsonObject(const QJsonObject& data);

        /*!
          \brief Gets the id of submission.
         */
        int getSubmissionID() const { return mSubmissionID; }
        /*!
           \brief Sets the id of submission.
           \param[v] ID of Submission.
         */
        void setSubmissionID(int v) { mSubmissionID = v; }

        /*!
          \brief Gets the id of user.
         */
        int getUserID() const { return mUserID; }
        /*!
           \brief Sets the id of user.
           \param[v] ID of user.
         */
        void setUserID(int v) { mUserID = v; }

        /*!
          \brief Gets the id of submitted problem.
         */
        int getProblemID() const { return mProblemID; }        
        /*!
           \brief Sets the id of submitted problem.
           \param[v] ID of problem.
         */
        void setProblemID(int v) { mProblemID = v; }

        /*!
          \brief Gets the verdict of submission.
         */
        Verdict getVerdict() const { return mVerdict; }
        /*!
           \brief Sets the verdict of submission.
           \param[v] Verdict.
         */
        void setVerdict(Verdict v) { mVerdict = v; }
        /*!
           \brief Sets the verdict of submission.
           \param[v] Verdict.
         */
        void setVerdict(int v);

        /*!
          \brief Gets the language of submitted code.
         */
        Language getLanguage() const { return mLanguage; }
        /*!
           \brief Sets the language of submitted code.
           \param[v] Language.
         */
        void setLanguage(Language v) { mLanguage = v; }
        /*!
           \brief Sets the language of submitted code.
           \param[v] Language.
         */
        void setLanguage(int v);

        /*!
          \brief Gets the runtime in milliseconds of submission.
         */
        int getRuntime() const { return mRuntime; }
        /*!
           \brief Sets the runtime of submission.
           \param[v] Runtime in milliseconds.
         */
        void setRuntime(int v);

        /*!
          \brief Gets the memory needed in bytes on runtime in this submission.
                 WARNING: This value is no longer supported by UVA OJ.
         */
        int getMemory() const { return mMemory; }
        /*!
           \brief Sets the memory needed on runtime in this submission.
           \param[v] Memory in bytes.
         */
        void setMemory(int v);

        /*!
          \brief Gets the rank of the user in this submission.
         */
        int getRank() const { return mRank; }
        /*!
           \brief Sets the rank of the user in this submission.
           \param[v] Rank of user.
         */
        void setRank(int v) { mRank = v; }

        /*!
          \brief Gets the time of submission in UNIX timestamp.
         */
        int getSubmissionTime() const { return vSubmissionTime; }
        /*!
           \brief Sets the time of submission in UNIX timestamp.
           \param[v] Time in UNIX timestamp.
         */
        void setSubmissionTime(int v) { vSubmissionTime = v; }

        /*!
          \brief Gets the full name of the user.
         */
        QString getFullName() const { return mFullName; }
        /*!
           \brief Sets the full name of the user.
           \param[v] Full name of the user.
         */
        void setFullName(QString v) { mFullName = v; }

        /*!
          \brief Gets the user name.
         */
        QString getUserName() const { return mUserName; }
        /*!
           \brief Sets the user name.
           \param[v] User name.
         */
        void setUserName(QString v) { mUserName = v; }


        /*!
          \brief Gets the number of submitted problem.
                 REQUIRES: UvaArenaWidget to be avaiable.
         */
        int getProblemNumber() const { return mProblemNumber; }
        /*!
           \brief Sets the number of submitted problem.
           \param[v] Problem number.
         */
        void setProblemNumber(int v) { mProblemNumber = v; }

        /*!
          \brief Gets the title of the submitted problem.
                 REQUIRES: UvaArenaWidget to be avaiable.
         */
        QString getProblemTitle() const { return mProblemTitle; }
        /*!
           \brief Sets the title of the submitted problem.
           \param[v] Problem title.
         */
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
