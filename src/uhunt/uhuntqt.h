#pragma once

#include <memory>
#include <QNetworkAccessManager>

#include "probleminfo.h"
#include "judgestatus.h"
#include "userinfo.h"
#include "usersubmission.h"
#include "uvalib_global.h"

namespace uva
{

    class UVA_EXPORT Uhuntqt : public QObject
    {
        Q_OBJECT
    public:

        Uhuntqt(std::shared_ptr<QNetworkAccessManager> manager);

        /**
         * @brief getProblemList Get the list of problem info.
         * @param data Downloaded javascript array data.
         * @return List of problem info objects.
        */
        QList<ProblemInfo> problemListFromData(const QByteArray& data);

        /**
         * @brief judgeStatusFromData   Gets the latest judge status.
         * @param data Downloaded javascript array data.
         * @return List of judge status objects.
         */
        QList<JudgeStatus> judgeStatusFromData(const QByteArray& data);

    signals:

        //signal emitted after judge status is downloaded
        void judgeStatusDownloaded(QList<JudgeStatus>);
        //signal emitted after problem list is downloaded
        void problemListDownloaded(QList<ProblemInfo>);
        //signal emitted after user id is downloaded
        void userIdDownloaded(QString userName, int userID);
        //signal emitted after user info is downloaded
        void userInfoDownloaded(UserInfo);
        //signal emitted after user info is updated
        void userInfoUpdated(UserInfo&);

    public slots:

        /**
         * @brief Gets the UVA problem list. Emits problemListDownloaded() when finished.
        */
        void getProblemList();

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
        void getUserID(QString userName);

        /**
         * @brief getUserSubmissions Get all submissions of a specific user starting from minId
         * @param userId Userid of user
         */
        void getUserInfo(int userId);

        /**
         * @brief updatedUserInfo Update the user info to latest data
         * @param uinfo UserInfo object to be updated
         */
        void updatedUserInfo(UserInfo& uinfo);

    private:

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
    };
}
