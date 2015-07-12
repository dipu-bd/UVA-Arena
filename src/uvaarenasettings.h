#pragma once

#include <QSettings>

#include "uvalib_global.h"

namespace uva
{

    class UVA_EXPORT UVAArenaSettings
    {
    public:

        QString userName();
        void setUserName(QString userName);

        qint32 userId();
        void setUserId(qint32 userId);

        qint64 maxDaysUntilProblemListRedownload();
        void setMaxDaysUntilProblemListRedownload(qint64 days);

    private:
        QSettings settings;
    };
}
