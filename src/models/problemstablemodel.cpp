#include "problemstablemodel.h"

using namespace uva;

ProblemsTableModel::ProblemsTableModel()
    : mProblemMap(nullptr)
{
    insertColumns({ 
        "Problem Number", "Problem Title", "Level", "DACU",
        "Time Limit", "Best", "Total Submissions",
        "# Accepted", "# Wrong Answers" 
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
        mRowToId.insert(row++, it->getID());
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
        "Problem Number", "Problem Title", "Level", "DACU",
       "Time Limit", "Best", "Total Submissions",
       "# Accepted", "# Wrong Answers"
    */

    switch (index.column()) {
    case 0:
        return mProblemMap->value(mRowToId[index.row()]).getNumber();

    case 1:
        return mProblemMap->value(mRowToId[index.row()]).getTitle();

    case 2:
        return mProblemMap->value(mRowToId[index.row()]).getLevel();

    case 3:
        return mProblemMap->value(mRowToId[index.row()]).getDACU();

    case 4:
        return mProblemMap->value(mRowToId[index.row()]).getRuntimeLimit();

    case 5:
        return mProblemMap->value(mRowToId[index.row()]).getBestRuntime();

    case 6:
        return mProblemMap->value(mRowToId[index.row()]).getTotalSubmission();

    case 7:
        return mProblemMap->value(mRowToId[index.row()]).getAcceptedCount();

    case 8:
        return mProblemMap->value(mRowToId[index.row()]).getWrongAnswerCount();
    }

    return QVariant();
}