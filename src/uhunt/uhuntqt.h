#pragma once

#include <memory>
#include <QNetworkAccessManager>

#include "probleminfo.h"
#include "judgestatus.h"
#include "uvalib_global.h"

namespace uva
{

    class UVA_EXPORT Uhuntqt : public QObject
    {
        Q_OBJECT
    public:

        Uhuntqt(std::shared_ptr<QNetworkAccessManager> manager);

        /**
            \brief getProblemList Get the list of problem info.

            \param data Downloaded javascript array data.
            \return List of problem info objects.
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

    public slots:

        /**
        \brief Gets the UVA problem list. Emits problemListDownloaded()
               when finished.
        */
        void getProblemList();

        /**
         * @brief getJudgeStatus    Gets the status current judging queue.
             Emits judgeStatusDownloaded() when finished.
         * @param lastSubmissionID  Submission ID from where the download should begin.
             0 means latest 100 submissions.
         */
        void getJudgeStatus(int lastSubmissionID = 0);

    private:

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
    };
}
