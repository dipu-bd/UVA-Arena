#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include "models/problemstablemodel.h"
#include <QWidget>

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

        void showNewProblem(int problemNumber);

    private slots:

        void problemsTableDoubleClicked(QModelIndex index);

    private:

        void showPDFByProblemNumber(int problemNumber);

        void downloadPDF(QString url, QString fileName);

        QSortFilterProxyModel mProblemsFilterProxyModel;
        ProblemsTableModel mProblemsTableModel;
        Ui::ProblemsWidget *ui;
    };

}
