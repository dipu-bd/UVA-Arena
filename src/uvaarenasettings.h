#pragma once

#include <QSettings>

#include "uvalib_global.h"

namespace uva
{
    /*!
        \brief Convenience class that wraps QSettings with
               application-specific settings.

        No variables are stored internally within this class. An
        underlying QSettings instance provides access and storage
        to all of the settings. This allows for multiple instances
        across the entire application to always have the most 
        up-to-date settings.
    */
    class UVA_EXPORT UVAArenaSettings
    {
    public:

        /*!
            \brief Get the currently stored UVA user name.
                   Default is a blank string.
        */
        QString userName();

        /*!
            \brief Set the UVA user name.

            \param[in] userName the user name to save
        */
        void setUserName(QString userName);

        /*!
            \brief Get the currently stored UVA user id. Default
                   is -1.
        */
        qint32 userId();

        /*!
            \brief Set the UVA user id.

            \param[in] userId The user id to save
        */
        void setUserId(qint32 userId);

        /*!
            \brief Get the maximum number of days to wait until the
                   problem list is redownloaded. Default is 1 day.
        */
        qint64 maxDaysUntilProblemListRedownload();

        /*!
            \brief Set the maximum number of days to wwait until the
                   problem list is redownloaded.
        */
        void setMaxDaysUntilProblemListRedownload(qint64 days);

        qint32 maxProblemsTableRowsToFetch();

        void setMaxProblemsTableRowsToFetch(qint32 numRows);

    private:
        QSettings settings;
    };
}
