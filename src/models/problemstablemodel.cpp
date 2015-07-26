#include "problemstablemodel.h"
#include "QBrush"

using namespace uva;

ProblemsTableModel::ProblemsTableModel()
    : mProblemMap(nullptr)
{
    insertColumns({ 
        "Problem Number", "Problem Title", "DACU",
        "Time Limit", "Best", "# Accepted",
        "# Wrong Answers"
    });
}

void ProblemsTableModel::setUhuntProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    beginResetModel();
    mProblemMap = problemMap;

    Uhunt::ProblemMap::const_iterator it =
        mProblemMap->constBegin();

    Uhunt::ProblemMap::const_iterator end =
        mProblemMap->constEnd();

    int row = 0;
    while (it != end) {
        mRowToId.insert(row++, it->ProblemID);
        it++;
    }
    endResetModel();
}

int ProblemsTableModel::getDataCount() const
{
    if (!mProblemMap)
        return 0;

    return mProblemMap->count();
}

QVariant ProblemsTableModel::getDataAtIndex(const QModelIndex &index) const
{
    if (!mProblemMap)
        return QVariant();

    /*
        "Problem Number", "Problem Title", "DACU",
       "Time Limit", "Best", "Total Submissions",
       "# Accepted", "# Wrong Answers"
    */

    Problem problem = mProblemMap->value(mRowToId[index.row()]);

    switch (index.column()) {
    case 0:
        return problem.ProblemNumber;

    case 1:
        return problem.ProblemTitle;

    case 2:
        return problem.DACU;

    case 3:
        return problem.RuntimeLimit;

    case 4:
        return problem.BestRuntime;

    case 5:
        return problem.AcceptedCount;

    case 6:
        return problem.WrongAnswerCount;
    }

    return QVariant();
}

QVariant uva::ProblemsTableModel::style(const QModelIndex &index, int role) const
{
    switch (role)
    {
    case Qt::ForegroundRole:
        switch (index.column())
        {
        case 0:
        case 1:
        case 2:
            return QBrush(Qt::red);

        case 3:
            return QBrush(Qt::cyan);

        default:
            return QBrush(Qt::magenta);
        }
    }

    return QVariant();
}
