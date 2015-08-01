#pragma once
#include "QSortFilterProxyModel"
#include "uhunt/category.h"

namespace uva
{

    class ProblemsProxyModel : public QSortFilterProxyModel
    {
        typedef Category::CategoryProblem CategoryProblem;
    public:

        void setProblemFilter(QList<int> problemFilter);

    protected:

        virtual bool filterAcceptsRow(int source_row, const QModelIndex &source_parent) const override;

    private:
        QList<int> mProblemFilter;
    };
}
