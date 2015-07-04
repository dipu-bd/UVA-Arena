#pragma once

#include "probleminfo.h"
#include "uhuntqt_global.h"
#include <QNetworkAccessManager>

namespace uhunqt
{

    class UHUNTQT_EXPORT Uhuntqt : public QObject
    {
        Q_OBJECT
    public:


        Uhuntqt(QNetworkAccessManager* manager);

        /**
            \brief Gets the UVA problem list. Emits problemListDownloaded()
                when finished.
        */
        void getProblemList();

    signals:

        void problemListDownloaded(QList<ProblemInfo>);

    private:

        /**
            \brief getProblemList Get the list of problem info.

            \param data Downloaded JSON data.
            \param size Size of the JSON data.
            \return List of problem info objects.
        */
        QList<ProblemInfo> problemListFromData(QByteArray data);

        QNetworkAccessManager* mNetworkManager;
    };
}
