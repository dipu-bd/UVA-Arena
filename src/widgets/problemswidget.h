#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include "models/problemstablemodel.h"
#include <QWidget>
#include <mupdf-qt.h>

#include <QWidget>
#include <QSortFilterProxyModel>

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

        void setFilterProblemsBy(QString columnName);

        void showNewProblem(QModelIndex index);

    private slots:

    private:
        QSortFilterProxyModel mProblemsFilterProxyModel;
        ProblemsTableModel mProblemsTableModel;
        Ui::ProblemsWidget *ui;
        MuPDF::Document* pdfDocument;
    };

}
