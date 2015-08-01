#include "problemsproxymodel.h"

bool uva::ProblemsProxyModel::filterAcceptsRow(int source_row, const QModelIndex &source_parent) const
{
    QModelIndex problemNumberIndex = sourceModel()->index(source_row, 0, source_parent);
    QModelIndex problemTitleIndex = sourceModel()->index(source_row, 1, source_parent);

    bool stringFilterMatches =
        sourceModel()->data(problemNumberIndex).toString().contains(filterRegExp())
        || sourceModel()->data(problemTitleIndex).toString().contains(filterRegExp());

    if (mProblemFilter.isEmpty()) {
        return stringFilterMatches;
    }

    int problemNumber = problemNumberIndex.data().toInt();

    return mProblemFilter.contains(problemNumber) && stringFilterMatches;
}

void uva::ProblemsProxyModel::setProblemFilter(QList<int> problemFilter)
{
    mProblemFilter = problemFilter;
    invalidateFilter();
}
