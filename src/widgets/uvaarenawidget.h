#pragma once

#include "uvalib_global.h"
#include <QWidget>
#include <QNetworkAccessManager>
#include <memory>

#include "uvaarenasettings.h"
#include "uhunt/uhunt.h"

namespace uva
{
    class ProblemsWidget;
    class CodesWidget;
    class JudgeStatusWidget;
    class ProfilesWidget;
    class MainWindow;

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
            \brief Events that all UVAArenaWidget subclasses should emit

            If a UVAArenaWidget needs to notify other widgets of an event,
            the newUVAArenaEvent() signal should be emitted.
        */
        enum class UVAArenaEvent : int {
            UPDATE_STATUS,
            SHOW_PROBLEM,
            SHOW_PROBLEM_DESCRIPTION,
            SHOW_CODE,
            SHOW_STATUS,
            SHOW_PROFILE,
            TOTAL_EVENTS,
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

        /*!
            \brief Get the problemswidget instance to use across all widgets
         */
        ProblemsWidget* problemsWidget();

        /*!
            \brief Set the problemswidget instance to use across all widgets

            \param[in] problemsWidget The ProblemsWidget instance
         */
        void setProblemsWidget(ProblemsWidget* problemsWidget);

        /*!
            \brief Get the codeswidget instance to use across all widgets
         */
        CodesWidget* codesWidget();

        /*!
            \brief Set the codeswidget instance to use across all widgets

            \param[in] codesWidget The CodesWidget instance
        */
        void setCodesWidget(CodesWidget* codesWidget);

        /*!
             \brief Get the judgestatuswidget instance to use across all widgets
         */
        JudgeStatusWidget* judgeStatusWidget();

        /*!
            \brief Set the judgestatuswidget instance to use across all widgets

            \param[in] judgeStatusWidget the JudgeStatusWidget instance
        */
        void setJudgeStatusWidget(JudgeStatusWidget* judgeStatusWidget);

        /*!
             \brief Get the profileswidget instance to use across all widgets
         */
        ProfilesWidget* profilesWidget();

        /*!
             \brief Set the profileswidget instance to use across all widgets

             \param[in] profilesWidget the ProfilesWidget instance
         */
        void setProfilesWidget(ProfilesWidget* profilesWidget);

        /*!
            \brief Get the current mainWindow instance
        */
        MainWindow* mainWindow();

        /*!
            \brief Set the current mainWindow instance

            \param[in] mainWindow the MainWindow instance
        */
        void setMainWindow(MainWindow* mainWindow);

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
            \brief Invoked when a new UVAArenaEvent is emitted.
         
            A widget that emits the newUVAArenaEvent() signal
            must not receive the signal in it's own slot. This is 
            to avoid unnecessary signal/slot connections.
         */
        virtual void onUVAArenaEvent(UVAArenaEvent, QVariant) = 0;

	protected:

        UVAArenaSettings mSettings;
        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        std::shared_ptr<Uhunt> mUhuntApi;

    private:

        MainWindow* mMainWindow;
        ProblemsWidget* mProblemsWidget;
        CodesWidget* mCodesWidget;
        JudgeStatusWidget* mJudgeStatusWidget;
        ProfilesWidget* mProfilesWidget;

	};

}
