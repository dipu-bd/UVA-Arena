#pragma once

#include <QSettings>

#include "uvalib_global.h"

namespace uva
{
    // #TODO add a GUI interface for this class
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

        enum class ProblemFormat : int {
            HTML = 0,
            PDF = 1
        };

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

        /**
            Maximum problems table rows to fetch.
        
            \return A qint32.
        */
        qint32 maxProblemsTableRowsToFetch();

        /**
            Sets maximum problems table rows to fetch.
        
            \param  numRows Number of rows.
        */
        void setMaxProblemsTableRowsToFetch(qint32 numRows);

        /**
            Problem format preference.
        
            \return A ProblemFormat.
        */
        ProblemFormat problemFormatPreference();

        /**
            Sets problem format preference.
        
            \param  format  Describes the format to use.
        */
        void setProblemFormatPreference(ProblemFormat format);

        /*!
          \brief Gets the update interval of judge status list.
          \return Update interval in milliseconds.
         */
        int liveEventsUpdateInterval();

        /*!
          \brief Sets the update interval of judge status list.
          \param msecs Update interval in milliseconds.
         */
        void setLiveEventsUpdateInterval(int msecs);

        bool liveEventsAutoStart();

        void setLiveEventsAutoStart(bool autostart);

        /**
            Saves PDF documents on download.
        
            \return true if it succeeds, false if it fails.
        */
        bool savePDFDocumentsOnDownload();

        /**
            Sets save PDF documents on download.
        
            \param  autosave    true to autosave.
        */
        void setSavePDFDocumentsOnDownload(bool autosave);

        /**
            Maximum days until category index redownload.
        
            \return A qint64.
        */
        qint64 maxDaysUntilCategoryIndexRedownload();

        void setMaxDaysUntilCategoryIndexRedownload(qint64 days);

    private:
        QSettings settings;
    };
}
