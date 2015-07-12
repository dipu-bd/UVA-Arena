#pragma once

#include "uvalib_global.h"
#include <QMainWindow>
#include <QNetworkAccessManager>
#include <QString>
#include <memory>
#include <vector>

#include "uhunt/uhunt.h"
#include "widgets/uvaarenawidget.h"

namespace uva
{

    namespace Ui {
        class MainWindow;
    }

    class UVA_EXPORT MainWindow : public QMainWindow
    {
        Q_OBJECT

    public:
        explicit MainWindow(std::shared_ptr<QNetworkAccessManager> networkManager,
                                QWidget *parent = 0);
        ~MainWindow();

    public slots:

        void onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent, QVariant);

    private:

        void initialize();

        std::vector<UVAArenaWidget*> mUVAArenaWidgets;

        Ui::MainWindow *ui;
        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        std::shared_ptr<Uhunt> mUhuntApi;

    };

}
