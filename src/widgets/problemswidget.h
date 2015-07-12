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

        Uhunt::ProblemMap getProblemMap();

    public slots:

        virtual void onUVAArenaEvent(UVAArenaEvent, QVariant) override;

    private slots:

        void onProblemListByteArrayDownloaded(QByteArray data);

    private:

        Uhunt::ProblemMap mProblems;

        qint64 mMaxDaysUntilProblemListRedownload;

        void loadProblemListFromFile(QString fileName);

        Ui::ProblemsWidget *ui;
        MuPDF::Document* pdfDocument;
    };

}
