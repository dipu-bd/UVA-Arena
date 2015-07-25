#pragma once

#include <memory>
#include <QNetworkAccessManager>

#include "problem.h"
#include "liveevent.h"
#include "userrank.h"
#include "usersubmission.h"
#include "submission.h"
#include "uvalib_global.h"

namespace uva
{

    class UVA_EXPORT Uhunt : public QObject
    {
        Q_OBJECT
    public:

        typedef QMap<Problem::Key, Problem> ProblemMap;

        typedef QMap<LiveEvent::Key, LiveEvent> LiveEventMap;

        /*!
            \brief Initialize the Uhunt object with the application's
                   QNetworkAccessManager.

            \param[in] manager Network access manager used for the application.
         */
        Uhunt(std::shared_ptr<QNetworkAccessManager> manager);

        /*!
            \brief Convert JSON data into a ProblemMap

            \param[in] json utf8 formatted json data

            \return A ProblemMap.
        */
        static ProblemMap problemMapFromJson(const QByteArray &json);

        /*!
            \brief Convert JSON data into a LiveEventMap.

            \param[in] json utf8 formatted json data

            \return  A LiveEventMap.
        */
        static LiveEventMap liveEventMapFromJson(const QByteArray &json);

        /*!
            \brief Converts JSON data into a list of UserRank objects.

            \param[in] json utf8 formatted json data.

            \return List of UserRank structs.
        */
        static QList<UserRank> userRankingsFromJson(const QByteArray &json);

        /*!
            \brief Converts JSON data into a list of user submissions to problems.
            \param[in] json utf8 formatted json data.
            \return List of UserSubmission structs.
        */
        static QList<UserSubmission> userSubmissionsFromJson(const QByteArray &json);

        /*!
            \brief Converts json data into a list of User Submissions.
        
            \param json utf8 formatted json data.
            \param userID UserID of the user

            \return A QList<UserSubmission>
        */
        static QList<UserSubmission> userSubmissionsFromJson(const QByteArray &json, int userID);

    signals:


        void problemByIdDownloaded(Problem);

        void allProblemsDownloaded(QByteArray);

        void liveEventsDownloaded(Uhunt::LiveEventMap);

        void userIDDownloaded(QString userName, int userID);

        void userSubmissionsJsonDownloaded(QByteArray, int userId, int lastSub);

        void userSubmissionsByUserIDDownloaded(QList<UserSubmission>);

        void userSubmissionsByProblemIDDownloaded(QList<UserSubmission>);

        void userRanksByPositionDownloaded(QList<UserRank>);

        void userRanksByUserIDDownloaded(QList<UserRank>);

        void userSubmissionsByProblemRankDownloaded(QList<UserSubmission>);

        void userRankOnProblemDownloaded(QList<UserSubmission>);

    public slots:

        /*!
            \brief Accesses the Uhunt API to get get a problem by its id.
            
            Emits problemByIdDownloaded() when finished successfully.
        */
        void problemById(int problemID);

        /*!
            \brief Accesses the Uhunt API to get all problem meta data available
                    in the UVA Online Judge as Json data. Use 
                    Uhunt::problemMapFromJson() to convert the data.
            
            The downloaded json should be saved to a file. This function
            should not be called often.

            The download is approximately 500kb. Emits allProblemsDownloaded()
            when finished successfully.
        */
        void allOnlineJudgeProblems();

        /*!
            \brief  Accesses the Uhunt API to get the live judging queue.
                    Use the latest live event ID from the downloaded events
                    for subsequent calls to this function.

            Emits liveEventsDownloaded() when finished.

            \param[in] lastSubmissionID ID of the submission from where the list
                        should begin. The value 0 means latest 100 submissions.
        */
        void liveEvents(quint64 lastSubmissionID = 0);

        /*!
            \brief Accesses the Uhunt API to get the ID of a user from
                   the username.

            Emits userIDDownloaded() when finished.

            \param[in] userName Username of the user.
        */
        void userIDFromUserName(const QString& userName);

        /*!
            \brief Accesses the Uhunt API to get the submissions to problems
                   of a user from the user's ID.

            Emits userSubmissionsByUserIDDownloaded() when finished.

            \param[in] userID ID of the user.
            \param[in] lastSub ID of the submission from where the list should
                       begin. The value 0 means the list should start from
                       the very first submission.
        */
        void userSubmissionsByUserID(int userID, int lastSubmissionID = 0);

        /*!
            \brief Accesses the Uhunt API to get rankings centered on a
                   specific user.

            set above and below to 0 to get the user with userID's ranking.

            Emits userRanksByUserIDDownloaded() when finished.

            \param[in] userID ID of the user
            \param[in] nAbove Number of users above the userId
            \param[in] nBelow Number of users below the userId
        */
        void userRanksByUserID(int userID, int above = 10, int below = 10);

        /*!
            \brief Accesses the Uhunt API to get user rankings starting from a
                   certain position, and ending at the starting rank + count - 1.

             Emits userRanksByPositionDownloaded() when finished.

            \param[in] startPos Rank from where the list starts.
            \param[in] count Number of rankings to retrieve.
        */
        void userRanksByPosition(int startPos = 1, int count = 100);

        /*!
            \brief Accesses the Uhunt API to get a list of submissions to a
                    specific problem.
            
            Emits userSubmissionsOnProblemDownloaded() when finished.

            \param[in] problemID ID of the problem
            \param[in] startTime Time from when to start in UNIX timestamp
            \param[in] endTime Time from when to stop in UNIX timestamp
        */
        void userSubmissionsByProblemID(int problemID, quint64 startTime, quint64 endTime);

        /*!
            \brief Accesses the Uhunt API to get a list of user submissions
                   to a problem based on the users rank to that problem.
            
            Emits userSubmissionsByProblemRankDownloaded() when finished.

            \param[in] problemID ID of the problem.
            \param[in] startRank Rank to start from.
            \param[in] count Number of submissions on the list.
        */
        void userSubmissionsByProblemRank(int problemId, int startRank = 1, int count = 25);

        /*!
            \brief Gets a list of submissions on a problem focusing the rank of an user.
            
            Emits userSubmissionsByProblemRankDownloaded() when finished.

            \param[in] problemID ID of the problem
            \param[in] userID Id of the user
            \param[in] above Number of user listed below
            \param[in] below Number of user listed above
        */
        //void userSubmissionsByProblem(int problemID, int userId, int above = 10, int below = 10);

    private:

        QNetworkReply* createNetworkRequest(QString url);

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
    };
}
