#pragma once

#include <QList>
#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonArray>

#include "uvalib_global.h"
#include "enums.h"

namespace uva
{
    class UVA_EXPORT Problem
    {
    public:
        Problem();

        /**
        * @brief Creates a json object from utf8 formatted data
        * @param json The json data to read
        * @return An instance of problem based off the json data
        */
        static Problem fromJson(const QByteArray& json);

        /**
        * @brief Creates a json object from utf8 formatted data
        * @param object The QJsonObject to use
        * @return An instance of problem based off the json data
        */
        static Problem fromJsonObject(const QJsonObject& object);

        /**
        * @brief Creates a json object from utf8 formatted data
        * @param object The QJSonArray to use
        * @return An instance of problem based off the json data
        */
        static Problem fromJsonArray(const QJsonArray& arr);

        // 0. Problem ID
        int getID() const { return mID; }
        void setID(int v) { mID = v; }
        // 1. Problem Number
        int getNumber() const { return mNumber; }
        void setNumber(int v) { mNumber = v; }
        // 2. Problem Title
        QString getTitle() const { return mTitle; }
        void setTitle(QString v) { mTitle = v; }
        // 3. Number of Distinct Accepted User (DACU)
        int getDACU() const { return mDACU; }
        void setDACU(int v) { mDACU = v; }
        // 4. Best Runtime of an Accepted Submission
        int getBestRuntime() const { return mBestRuntime; }
        void setBestRuntime(int v);
        // 5. Best Memory used of an Accepted Submission
        int getBestMemory() const { return mBestMemory; }
        void setBestMemory(int v);
        // 6. Number of No Verdict Given (can be ignored)
        int getNoVerdictCount() const { return mNoVerdictCount; }
        void setNoVerdictCount(int v) { mNoVerdictCount = v; }
        // 7. Number of Submission Error
        int getSubmissionErrorCount() const { return mSubmissionErrorCount; }
        void setSubmissionErrorCount(int v) { mSubmissionErrorCount = v; }
        // 8. Number of Can't be Judged
        int getCantBeJudgeCount() const { return mCantBeJudgedCount; }
        void setCantBeJudgedCount(int v) { mCantBeJudgedCount = v; }
        // 9. Number of In Queue
        int getInQueueCount() const { return mInQueueCount; }
        void setInQueueCount(int v) { mInQueueCount = v; }
        // 10. Number of Compilation Error
        int getCompileErrorCount() const { return mCompileErrorCount; }
        void setCompileErrorCount(int v) { mCompileErrorCount = v; }
        // 11. Number of Restricted Function
        int getRestrictedFunctionCount() const { return mRestrictedFunctionCount; }
        void setRestrictedFunctionCount(int v) { mRestrictedFunctionCount = v; }
        // 12. Number of Runtime Error
        int getRuntimeErrorCount() const { return mRuntimeErrorCount; }
        void setRuntimeErrorCount(int v) { mRuntimeErrorCount = v; }
        // 13. Number of Output Limit Exceeded
        int getOutputLimitExceededCount() const { return mOutputLimitExceededCount; }
        void setOutputLimitExceededCount(int v) { mOutputLimitExceededCount = v; }
        // 14. Number of Time Limit Exceeded
        int getTimeLimitExceededCount() const { return mTimeLimitExceededCount; }
        void setTimeLimitExceededCount(int v) { mTimeLimitExceededCount = v; }
        // 15. Number of Memory Limit Exceeded
        int getMemoryLimitExceededCount() const { return mMemoryLimitExceededCount; }
        void setMemoryLimitExceededCount(int v) { mMemoryLimitExceededCount = v; }
        // 16. Number of Wrong Answer
        int getWrongAnswerCount() const { return mWrongAnswerCount; }
        void setWrongAnswerCount(int v) { mWrongAnswerCount = v; }
        // 17. Number of Presentation Error
        int getPresentationErrorCount() const { return mPresentationErrorCount; }
        void setPresentationErrorCount(int v) { mPresentationErrorCount = v; }
        // 18. Number of Accepted
        int getAcceptedCount() const { return mAcceptedCount; }
        void setAcceptedCount(int v) { mAcceptedCount = v; }
        // 19. Time Limit (milliseconds)
        int getRuntimeLimitCount() const { return mRuntimeLimitCount; }
        void setRuntimeLimitCount(int v) { mRuntimeLimitCount = v; }
        // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
        ProblemStatus getStatus() const { return mStatus; }
        void setStatus(ProblemStatus v) { mStatus = v; }
        void setStatus(int v);

        // get volume number of the problem
        int getVolume() const { return mNumber / 100; }
        // get the total submissions
        int getTotalSubmission() const;

        // True if default user solved this problem
        bool isSolved() const { return mSolved; }
        void setSolved(bool v) { mSolved = v; }
        // get the hardness level of the problem
        double getLevel() const { return mLevel; }
        // True if problem is marked as favorite
        bool isMarked() const { return mMarked; }
        void setMarked(bool v) { mMarked = v; }

        //True if this is a starred problem
        bool isStarred() const { return mStar; }
        void setStar(bool v) { mStar = v; }
        //notes on this problem
        QString getNotes() const { return mNotes; }
        void setNotes(QString v) { mNotes = v; }

    private:
        // 0. Problem ID
        int mID;
        // 1. Problem Number
        int mNumber;
        // 2. Problem Title
        QString mTitle;
        // 3. Number of Distinct Accepted User (DACU)
        int mDACU;
        // 4. Best Runtime of an Accepted Submission
        int mBestRuntime;
        // 5. Best Memory used of an Accepted Submission
        int mBestMemory;
        // 6. Number of No Verdict Given (can be ignored)
        int mNoVerdictCount;
        // 7. Number of Submission Error
        int mSubmissionErrorCount;
        // 8. Number of Can't be Judged
        int mCantBeJudgedCount;
        // 9. Number of In Queue
        int mInQueueCount;
        // 10. Number of Compilation Error
        int mCompileErrorCount;
        // 11. Number of Restricted Function
        int mRestrictedFunctionCount;
        // 12. Number of Runtime Error
        int mRuntimeErrorCount;
        // 13. Number of Output Limit Exceeded
        int mOutputLimitExceededCount;
        // 14. Number of Time Limit Exceeded
        int mTimeLimitExceededCount;
        // 15. Number of Memory Limit Exceeded
        int mMemoryLimitExceededCount;
        // 16. Number of Wrong Answer
        int mWrongAnswerCount;
        // 17. Number of Presentation Error
        int mPresentationErrorCount;
        // 18. Number of Accepted
        int mAcceptedCount;
        // 19. Time Limit (milliseconds)
        int mRuntimeLimitCount;
        // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
        ProblemStatus mStatus;

        //set true if this problem is solved by default user
        bool mSolved;
        //set true if this problem is marked as favorite
        bool mMarked;
        //set the level of this problem (value between 1 and 10~14)
        double mLevel;        
        //set true if this is a starred problem
        bool mStar;
        //notes on this problem
        QString mNotes;

        // calculates and sets the level of the problem
        void calculateLevel();
    };

}
