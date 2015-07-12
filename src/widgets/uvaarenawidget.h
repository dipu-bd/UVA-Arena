#pragma once

#include "uvalib_global.h"
#include <QWidget>
#include <QNetworkAccessManager>
#include <memory>

#include "uhunt/uhunt.h"

namespace uva
{
    class ProblemsWidget;
    class CodesWidget;
    class JudgeStatusWidget;
    class ProfilesWidget;

	/**
		\brief base class for all UVA-Arena widgets. This class facilitates
			   communication between widgets through signals and slots, and by providing
			   methods to access common functionality.
	*/
	class UVA_EXPORT UVAArenaWidget : public QWidget
	{
        Q_OBJECT
	public:

        enum class UVAArenaEvent : int {
            UPDATE_STATUS,
            TOTAL_EVENTS,
        };

        explicit UVAArenaWidget(QWidget *parent = 0);
        virtual ~UVAArenaWidget();

        /**
         * @brief Initialize the widget. Place all data initialization from the uhunt api here.
         */
        virtual void initialize() = 0;

        /**
         * @brief Set the network manager to be used across all widgets.
         */
        virtual void setNetworkManager(std::shared_ptr<QNetworkAccessManager> networkManager);

        /**
         * @brief Set the Uhunt instance to use across all widgets
         */
        virtual void setUhuntApi(std::shared_ptr<Uhunt> uhuntApi);

        /**
         * @brief Get the problemswidget instance to use across all widgets
         */
        ProblemsWidget* problemsWidget();

        /**
         * @brief Set the problemswidget instance to use across all widgets
         */
        void setProblemsWidget(ProblemsWidget* problemsWidget);

        /**
         * @brief Get the codeswidget instance to use across all widgets
         */
        CodesWidget* codesWidget();

        /**
        * @brief Set the codeswidget instance to use across all widgets
        */
        void setCodesWidget(CodesWidget* codesWidget);

        /**
         * @brief Get the judgestatuswidget instance to use across all widgets
         */
        JudgeStatusWidget* judgeStatusWidget();

        /**
        * @brief Set the judgestatuswidget instance to use across all widgets
        */
        void setJudgeStatusWidget(JudgeStatusWidget* judgeStatusWidget);

        /**
         * @brief Get the profileswidget instance to use across all widgets
         */
        ProfilesWidget* profilesWidget();

        /**
         * @brief Set the profileswidget instance to use across all widgets
         */
        void setProfilesWidget(ProfilesWidget* profilesWidget);

    signals:

        /**
         * @brief Emitted when a new UVAArean event has occurred.
         *
         * This slot is used to communicate meta information across
         * all widgets. For example, when the status bar of the
         * MainWindow should be updated, the newUVAArenaEvent() signal
         * should be emitted, and the appropriate slot within MainWindow
         * should update the status bar.
         *
         * See the UVAArenaEvent enum for all possible events.
         */
        void newUVAArenaEvent(UVAArenaEvent, QVariant);

    public slots:

        /**
         * @brief Invoked when a new UVAArenaEvent is emitted.
         *
         * A widget that emits the newUVAArenaEvent() signal
         * must not receive the signal in it's own slot. This is 
         * to avoid unnecessary signal/slot connections.
         */
        virtual void onUVAArenaEvent(UVAArenaEvent, QVariant) = 0;

	protected:

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        std::shared_ptr<Uhunt> mUhuntApi;

    private:

        ProblemsWidget* mProblemsWidget;
        CodesWidget* mCodesWidget;
        JudgeStatusWidget* mJudgeStatusWidget;
        ProfilesWidget* mProfilesWidget;

	};

}
