#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include "models/problemstablemodel.h"
#include "models/categorytreemodel.h"

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

        void setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap);

    public slots:

        virtual void onUVAArenaEvent(UVAArenaEvent, QVariant) override;

        void setFilterProblemsBy(QString columnName);

    private slots:

        void problemsTableDoubleClicked(QModelIndex index);

    private:

        void downloadCategoryIndex();
        void loadCategoryIndexFromFile(QString result);

        QSortFilterProxyModel mProblemsFilterProxyModel;
        ProblemsTableModel mProblemsTableModel;
        CategoryTreeModel mCategoryTreeModel;
        std::unique_ptr<Ui::ProblemsWidget> mUi;
    };

}
