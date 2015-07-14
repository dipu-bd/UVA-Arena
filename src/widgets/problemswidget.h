#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include <QWidget>
#include "models/problemstablemodel.h"
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

        void setProblemsMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap);

    public slots:

        virtual void onUVAArenaEvent(UVAArenaEvent, QVariant) override;

    private slots:

    private:

        ProblemsTableModel mProblemsTableModel;
        Ui::ProblemsWidget *ui;
        MuPDF::Document* pdfDocument;
    };

}
