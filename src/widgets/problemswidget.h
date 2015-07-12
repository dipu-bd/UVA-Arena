#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include <QWidget>
#include <mupdf-qt.h>

namespace uva
{

    namespace Ui {
        class ProblemsWidget;
    }

    class UVA_EXPORT ProblemsWidget : public UVAArenaWidget
    {
        Q_OBJECT

    public:
        explicit ProblemsWidget(QWidget *parent = 0);
        ~ProblemsWidget();

        virtual void initialize() override;

        //get the problem map
        Uhunt::ProblemMap getProblemMap();
        //set the problem map
        void setProblemMap(Uhunt::ProblemMap);
        //get problem number by problem id
        int getProblemNumber(int problemId);
        //get problem id from problem number
        int getProblemId(int problemNumber);
        //get problem title by problem number
        QString getProbelmTitle(int problemNumber);
        //get problem by id
        Problem getProblemById(int problemId);
        //get problem by problem number
        Problem getProblemByNumber(int problemNumber);

    public slots:

        virtual void onUVAArenaEvent(UVAArenaEvent, QVariant) override;

    private slots:

        void onProblemListByteArrayDownloaded(QByteArray data);

    private:

        Uhunt::ProblemMap mProblems;
        QMap<int, int> mIdToNumber; //problem id -> problem number

        qint64 mMaxDaysUntilProblemListRedownload;

        void loadProblemListFromFile(QString fileName);

        Ui::ProblemsWidget *ui;
        MuPDF::Document* pdfDocument;
    };

}
