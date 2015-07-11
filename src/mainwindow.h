#pragma once

#include "uvalib_global.h"
#include <QMainWindow>
#include <QNetworkAccessManager>
#include <QStandardPaths>
#include <QString>
#include <memory>

#include "uhunt/uhunt.h"
#include "uhunt/uhuntdatabase.h"

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

    private slots:

        void onProblemListByteArrayDownloaded(QByteArray data);

    private:

        void initialize();

        void loadProblemListFromFile(QString fileName);

        Ui::MainWindow *ui;
        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        std::shared_ptr<Uhunt> mUhuntApi;

        qint64 mMaxDaysUntilProblemListRedownload;
    };

}
