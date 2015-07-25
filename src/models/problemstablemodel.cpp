#include "problemstablemodel.h"

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

    // #TODO use something better than a switch here
    switch (index.column()) {
    case 0:
        return mProblemMap->value(mRowToId[index.row()]).ProblemNumber;

    case 1:
        return mProblemMap->value(mRowToId[index.row()]).ProblemTitle;

    case 2:
        return mProblemMap->value(mRowToId[index.row()]).DACU;

    case 3:
        return mProblemMap->value(mRowToId[index.row()]).RuntimeLimit;

    case 4:
        return mProblemMap->value(mRowToId[index.row()]).BestRuntime;

    case 5:
        return mProblemMap->value(mRowToId[index.row()]).AcceptedCount;

    case 6:
        return mProblemMap->value(mRowToId[index.row()]).WrongAnswerCount;
    }

    return QVariant();
}