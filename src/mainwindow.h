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

        //get the problem map
        const Uhunt::ProblemMap& getProblemMap();
        //get problem number by problem id
        int getProblemNumberFromId(int problemId);
        //get problem id from problem number
        int getProblemIdFromNumber(int problemNumber);
        //get problem title by problem number
        QString getProblemTitle(int problemNumber);
        //get problem by id
        const Problem& getProblemById(int problemId);
        //get problem by problem number
        const Problem& getProblemByNumber(int problemNumber);

    public slots:

        void onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent, QVariant);

    private slots:

        void onProblemListByteArrayDownloaded(QByteArray data);

    private:

        //set the problem map
        void setProblemMap(Uhunt::ProblemMap problemMap);

        void initialize();

        void initializeData();

        void initializeWidgets();

        void loadProblemListFromFile(QString fileName);

        std::unique_ptr<Uhunt::ProblemMap> mProblems;

        std::unique_ptr<QMap<int, int> > mProblemIdToNumber;

        qint64 mMaxDaysUntilProblemListRedownload;

        std::vector<UVAArenaWidget*> mUVAArenaWidgets;

        Ui::MainWindow *ui;
        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        std::shared_ptr<Uhunt> mUhuntApi;

    };

}
