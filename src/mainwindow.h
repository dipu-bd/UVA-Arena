#pragma once

#include "uvalib_global.h"
#include <QMainWindow>
#include <QNetworkAccessManager>
#include <QString>
#include <memory>
#include <vector>

#include "uhunt/uhunt.h"
#include "widgets/uvaarenawidget.h"

#include "uvaarenasettings.h"

namespace uva
{

    namespace Ui {
        class MainWindow;
    }

    /*!
        \brief Main GUI element for the application. Initializes common 
               resources used by all widgets.
    */
    class UVA_EXPORT MainWindow : public QMainWindow
    {
        Q_OBJECT

    public:
        explicit MainWindow(std::shared_ptr<QNetworkAccessManager> networkManager,
                                QWidget *parent = 0);
        ~MainWindow();

        /*!
            \brief Returns the currently loaded problems
        */
        std::shared_ptr<Uhunt::ProblemMap> getProblemMap();

    public slots:

        /*!
            \brief Invoked when the newUVAArenaEvent() signal is emitted
        */
        void onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent, QVariant);

    private slots:

        /*!
            \brief Invoked when Uhunt::getProblemListAsByteArray finished
        */
        void onProblemListByteArrayDownloaded(QByteArray data);

    private:

        /*!
            \brief Sets the internal problem map and problem number
                   to id conversion data.
        */
        void setProblemMap(Uhunt::ProblemMap problemMap);

        /*!
            \brief Kicks off all initialization necessary for the entire
                   application. Invokes initializeData() first, and then
                   initializeWidgets().
        */
        void initialize();

        /*!
            \brief Initializes data related parts of the application. This
                   includes downloading data from the uhunt api and reading
                   from the files filesystem.
        */
        void initializeData();

        /*!
            \brief Connects all UVAArenaWidget signals/slots and invokes
                   UVAArenaWidget::initialize() on all the appropriate
                   UVAArenaWidget subclasses.
        */
        void initializeWidgets();

        /*!
            \brief Loads the current UVA problem list from a stored file.

            This function can instead redownload the problem list if the
            file stored in persistent storage is too old.

            \param[in] fileName The file name where the problem list is stored.
        */
        void loadProblemListFromFile(QString fileName);

        UVAArenaSettings mSettings;

        std::shared_ptr<Uhunt::ProblemMap> mProblems;

        std::vector<UVAArenaWidget*> mUVAArenaWidgets;

        Ui::MainWindow *ui;
        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        std::shared_ptr<Uhunt> mUhuntApi;

    };

}
