#pragma once

#include "uvalib_global.h"
#include <QWidget>
#include <QNetworkAccessManager>
#include <memory>

#include "uvaarenasettings.h"
#include "uhunt/uhunt.h"

namespace uva
{

    /*!
        \brief base class for all UVA-Arena widgets. This class facilitates
               communication between widgets through signals and slots, and by providing
               methods to access common functionality.
        */
    class UVA_EXPORT UVAArenaWidget : public QWidget
    {
        Q_OBJECT
    public:

        /*!
            \brief Events that all UVAArenaWidget subclasses should emit or
                   receive.

            UVAArenaEvents are global events that have the potential to affect
            all UVAArenaWidgets. This includes changing user settings or
            displaying a problem from the Online Judge.

            If a UVAArenaWidget needs to notify other widgets of an event,
            the newUVAArenaEvent() signal should be emitted.
            */
        enum class UVAArenaEvent : int {
            UPDATE_STATUS,
            SHOW_PROBLEM,
            UPDATE_SETTINGS,
        };

        explicit UVAArenaWidget(QWidget *parent = 0);
        virtual ~UVAArenaWidget();

        /*!
            \brief Initialize the widget. Place all data initialization from
            the uhunt api here.
            */
        virtual void initialize() = 0;

        /*!
            \brief Set the network manager to be used across all widgets.

            \param[in] networkManager Shared pointer to a QNetworkAccessManager object
            */
        virtual void setNetworkManager(std::shared_ptr<QNetworkAccessManager> networkManager);

        /*!
            \brief Set the Uhunt instance to use across all widgets

            \param[in] uhuntApi Shared pointer to a Uhunt object
            */
        virtual void setUhuntApi(std::shared_ptr<Uhunt> uhuntApi);

    signals:

        /*!
            \brief Emitted when a new UVAArean event has occurred.

            This slot is used to communicate meta information across
            all widgets. For example, when the status bar of the
            MainWindow should be updated, the newUVAArenaEvent() signal
            should be emitted, and the appropriate slot within MainWindow
            should update the status bar.

            See the UVAArenaEvent enum for all possible events.
            */
        void newUVAArenaEvent(UVAArenaEvent, QVariant);

    public slots:

        /*!
            \brief Invoked when the newUVAArenaEvent() signal is emitted and
                   connected to this slot.
        */
        virtual void onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent, QVariant) = 0;

    protected:

        UVAArenaSettings mSettings;
        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        std::shared_ptr<Uhunt> mUhuntApi;

    };

}
