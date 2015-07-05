#pragma once

#include <memory>
#include "probleminfo.h"
#include "uvalib_global.h"
#include <QNetworkAccessManager>

namespace uva
{

    class UVA_EXPORT Uhuntqt : public QObject
    {
        Q_OBJECT
    public:

        Uhuntqt(std::shared_ptr<QNetworkAccessManager> manager);

        /**
            \brief getProblemList Get the list of problem info.

            \param data Downloaded javascript array data
            \return List of problem info objects.
        */
        QList<ProblemInfo> problemListFromData(const QByteArray& data);


    signals:

        void problemListDownloaded(QList<ProblemInfo>);

    public slots:

        /**
        \brief Gets the UVA problem list. Emits problemListDownloaded()
        when finished.
        */
        void getProblemList();

    private:

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
    };
}
