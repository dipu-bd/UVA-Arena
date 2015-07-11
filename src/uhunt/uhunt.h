#pragma once

#include <memory>
#include <QNetworkAccessManager>

#include "problem.h"
#include "judgestatus.h"
#include "userinfo.h"
#include "rankinfo.h"
#include "usersubmission.h"
#include "uvalib_global.h"

namespace uva
{

    class UVA_EXPORT Uhunt : public QObject
    {
        Q_OBJECT
    public:

        Uhunt(std::shared_ptr<QNetworkAccessManager> manager);

        /**
         * @brief getProblemList Get the list of problem info.
         * @param data Downloaded javascript array data.
         * @return List of problem info objects.
         */
        static QList<Problem> problemListFromData(const QByteArray &data);

        /**
         * @brief judgeStatusFromData   Gets the latest judge status.
         * @param data Downloaded javascript array data.
         * @return List of judge status objects.
         */
        static QList<JudgeStatus> judgeStatusFromData(const QByteArray &data);

        /**
         * @brief rankListFromData Gets the rank list from the json data.
         * @param data JSon string of data.
         * @return List of RankInfo objects.
         */
        static QList<RankInfo> rankListFromData(const QByteArray &data);

        /**
         * @brief submissionListFromData Converts json data to a list of submission message
         * @param data Json data to parse.
         * @return List of SubmissionMessage object
         */
        static QList<SubmissionMessage> submissionListFromData(const QByteArray &data);

        /**
         * @brief userSubsOnProblemFromData Parse the user submission data on a problem
         * @param userId Id of user involved
         * @param data Data to parse
         * @return UserInfo objecet
         */
        static UserInfo userSubsOnProblemFromData(int userId, const QByteArray &data);

    signals:

        //signal emitted after problem is downloaded
        void problemByIdDownloaded(Problem);
        //signal emitted after problem list is downloaded
        void problemListDownloaded(QList<Problem>);
        // signal emitted after problem list is downloaded
        void problemListByteArrayDownloaded(QByteArray);
        //signal emitted after judge status is downloaded
        void judgeStatusDownloaded(QList<JudgeStatus>);
        //signal emitted after user id is downloaded
        void userIdDownloaded(QString userName, int userID);
        //signal emitted after user info is downloaded
        void userInfoDataDownloaded(const QByteArray& data, int userId, int lastSub);
        //signal emitted after rank by position is downloaded
        void rankByPositionDownloaded(QList<RankInfo>);
        //signal emitted after rank by user is downloaded
        void rankByUserDownloaded(QList<RankInfo>);
        //signal emitted after submission list on problem is downloaded
        void submissionOnProblemDownloaded(QList<SubmissionMessage>);
        //signal emitted after rank list on problem is downloaded
        void ranklistOnProblemDownloaded(QList<SubmissionMessage>);
        //signal emitted after user rank on problem is downloaded
        void userRankOnProblemDownloaded(QList<SubmissionMessage>);
        //signal emitted after user submissions on problem is downloaded
        void userSubmissionOnProblemDownloaded(UserInfo);

    public slots:

        /**
         * @brief Get a problem by it's id. Emits problemByIdDownloaded() when finished.
         */
        void getProblemById(int id);

        /**
         * @brief Gets the UVA problem list. Emits problemListDownloaded() when finished.
         *
         * Emits problemListByteArrayDownloaded when finished successfully.
         */
        void getProblemList();
        
        /**
         * @brief Gets the UVA problem as a raw  QByteArray. Used for writing to files.
         */
        void getProblemListAsByteArray();

        /**
         * @brief getJudgeStatus    Gets the status current judging queue.
         *   Emits judgeStatusDownloaded() when finished.
         * @param lastSubmissionID  Submission ID from where the download should begin.
         *      0 means latest 100 submissions.
         */
        void getJudgeStatus(int lastSubmissionID = 0);

        /**
         * @brief getUserID returns id of an user from username
         * @param userName Username of the user
         */
        void getUserID(const QString& userName);

        /**
         * @brief getUserSubmissions Get all submissions of a specific user starting from minId
         * @param userId Userid of user
         * @param lastSub Submission id from where the list begins
         */
        void getUserInfoData(int userId, int lastSub = 0);

        /**
         * @brief getRankByUser Gets the ranklist centered on the specific user.
         * @param userId ID of the user
         * @param nAbove Number of users above the userId
         * @param nBelow Number of users below the userId
         */
        void getRankByUser(int userId, int nAbove = 10, int nBelow = 10);

        /**
         * @brief getRankByPosition Gets the ranklist starting from a certain position.
         * @param startPos Rank from where the list starts.
         * @param count Number of users on the list.
         */
        void getRankByPosition(int startPos = 1, int count = 100);

        /**
         * @brief getSubmissionOnProblem  Emits submissionOnProblemDownloaded
         * @param problemId ID of the problem
         * @param startTime Time from when to start in unix timestamp
         * @param endTime Time from when to stop in unix timestamp
         */
        void getSubmissionOnProblem(int problemId, int startTime, int endTime);

        /**
         * @brief getRanklistOnProblem Emits ranklistOnProblemDownloaded()
         * @param problemId ID of the problem
         * @param startRank Rank to start from
         * @param count Number of people on the list
         */
        void getRanklistOnProblem(int problemId, int startRank = 1, int count = 25);

        /**
         * @brief getUserRankOnProblem Emits userRankOnProblemDownloaded()
         * @param problemId ID of problem
         * @param userId Id of the user
         * @param nAbove Number of user listed below
         * @param nBelow Number of user listed above
         */
        void getUserRankOnProblem(int problemId, int userId, int nAbove = 10, int nBelow = 10);

        /**
         * @brief getUserSubmissionOnProblem Emits userSubmissionOnProblemDownloaded()
         * @param userId Id of the user
         * @param problemId  ID of problem
         * @param minSubsID Minimum submission id from where to begin
         */
        void getUserSubmissionOnProblem(int userId, int problemId, int minSubsID = 0);

    private:

        QNetworkReply* createNetworkRequest(QString url);

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
    };
}
