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
        /*!
            Keys are problem IDs.
        */
        typedef QMap<int, Problem> ProblemMap;                

        /*!
           \brief Uhunt Initialize a new uhunt api object with a given network access manager.
           \param[manager] Network access manager used to download data.
         */
        Uhunt(std::shared_ptr<QNetworkAccessManager> manager);

        /*!
          \brief Convert JSON data into map of problem number to problem info objects.
          \param[data] JSON data in this format: [ [...], [...], ... ]
          \return Map of problem number to problem info objects.
         */
        static ProblemMap problemMapFromData(const QByteArray &data);

        /*!
           \brief Convert JSON data into list of JudgeStatus objects.
           \param[data] JSON data in this format: [ {...}, {...}, ... ]
           \return List of JudgeStatus objects.
         */
        static QList<JudgeStatus> judgeStatusFromData(const QByteArray &data);

        /*!
           \brief Converts JSON data into a list of RankInfo objects.
           \param[data] JSON data in this format: [ {...}, {...}, ... ]
           \return List of RankInfo objects.
         */
        static QList<RankInfo> rankListFromData(const QByteArray &data);

        /*!
           \brief Converts JSON data into a list of SubmissionMessage objects.
           \param[data] JSON data in this format: [ {...}, {...}, ... ]
           \return List of SubmissionMessage objects.
         */
        static QList<SubmissionMessage> submissionListFromData(const QByteArray &data);

        /*!
           \brief Convert JSON data into an UserInfo object.
           \param[userId] ID of the user.
           \param[data] JSON data in this format: {.., .., [ [..], [..], ...] }
           \return An UserInfo object.
         */
        static UserInfo userSubsOnProblemFromData(int userId, const QByteArray &data);

    signals:

        //signal emitted after problem is downloaded
        void problemByIdDownloaded(Problem);
        //signal emitted after problem list is downloaded
        void problemListDownloaded(Uhunt::ProblemMap);
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

        /*!
           \brief Gets a problem by it's id.
                  Emits problemByIdDownloaded() when finished successfully.
         */
        void getProblemById(int id);

        /*!
           \brief Gets the problem list.
                  Emits problemListDownloaded() when finished successfully.
         */
        void getProblemList();
        
        /*!
           \brief Gets the problem list as a raw QByteArray. Used for writing to files.
                  Emits problemListByteArrayDownloaded() when finished successfully.
         */
        void getProblemListAsByteArray();

        /*!
           \brief Gets the status current judging queue.
                  Emits judgeStatusDownloaded() when finished.
           \param[lastSubmissionID] ID of the submission from where the list should begin.
                  The value 0 means latest 100 submissions.
         */
        void getJudgeStatus(int lastSubmissionID = 0);

        /*!
           \brief Gets ID of an user from username.
                  Emits userIdDownloaded() when finished.
           \param[userName] Username of the user
         */
        void getUserID(const QString& userName);

        /*!
           \brief Gets submissions of an user.
                  Emits userInfoDataDownloaded() when finished.
           \param[userId] ID of the user.
           \param[lastSub] ID of the submission from where the list should begin.
                  The value 0 means the list will start from beginning.
         */
        void getUserInfoData(int userId, int lastSub = 0);

        /*!
           \brief Gets the ranklist centered on the specific user.
           \param[userId] ID of the user
           \param[nAbove] Number of users above the userId
           \param[nBelow] Number of users below the userId
         */
        void getRankByUser(int userId, int nAbove = 10, int nBelow = 10);

        /*!
           \brief Gets the ranklist starting from a certain position.
                  Emits rankByPositionDownloaded() when finished.
           \param[startPos] Rank from where the list starts.
           \param[count] Number of users on the list.
         */
        void getRankByPosition(int startPos = 1, int count = 100);

        /*!
           \brief Gets a list of submissions on a specific problem.
                  Emits submissionOnProblemDownloaded() when finished.
           \param[problemId] ID of the problem
           \param[startTime] Time from when to start in UNIX timestamp
           \param[endTime] Time from when to stop in UNIX timestamp
         */
        void getSubmissionOnProblem(int problemId, int startTime, int endTime);

        /*!
           \brief Gets a list of submissions on a problem focusing rank.
                  Emits ranklistOnProblemDownloaded() when finished.
           \param[problemId] ID of the problem.
           \param[startRank] Rank to start from.
           \param[count] Number of submissions on the list.
         */
        void getRanklistOnProblem(int problemId, int startRank = 1, int count = 25);

        /*!
           \brief Gets a list of submissions on a problem focusing the rank of an user.
                  Emits userRankOnProblemDownloaded() when finished.
           \param[problemId] ID of problem
           \param[userId] Id of the user
           \param[nAbove] Number of user listed below
           \param[nBelow] Number of user listed above
         */
        void getUserRankOnProblem(int problemId, int userId, int nAbove = 10, int nBelow = 10);

        /*!
           \brief Gets a list of submissions on a problem by specific user.
                  Emits userSubmissionOnProblemDownloaded() when finished.
           \param[userId Id] of the user
           \param[problemId]  ID of problem
           \param[minSubsID] Minimum submission id from where to begin
         */
        void getUserSubmissionOnProblem(int userId, int problemId, int minSubsID = 0);

    private:

        QNetworkReply* createNetworkRequest(QString url);

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
    };
}
