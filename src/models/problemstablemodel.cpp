#include "problemstablemodel.h"
#include "QBrush"
#include "QFont"
#include "commons\colorizer.h"

using namespace uva;

ProblemsTableModel::ProblemsTableModel()
    : mProblemMap(nullptr)
    , mAverageDACU(-1)
{
    insertColumns({ 
        "Problem Number", "Problem Title", "# Distinct accepted users"
        , "# Accepted", "# Wrong Answers"
        , "Time Limit (seconds)", "Best Runtime (seconds)"
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
    int dacuRows = 0;
    while (it != end) {
        if (it->DACU > 1e3) { // hard cut-off to preserve average
            dacuRows++;
            mAverageDACU += it->DACU;
        }
        mRowToId.insert(row++, it->ProblemID);
        it++;
    }

    mAverageDACU /= dacuRows;

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
    "Problem Number", "Problem Title", "# Distinct accepted users"
    , "# Accepted", "# Wrong Answers"
    , "Time Limit (seconds)", "Best Runtime (seconds)"
    */

    const Problem problem = mProblemMap->value(mRowToId[index.row()]);

    switch (index.column()) {
    case 0:
        return problem.ProblemNumber;

    case 1:
        return problem.ProblemTitle;

    case 2:
        return problem.DACU;

    case 3:
        return problem.AcceptedCount;

    case 4:
        return problem.WrongAnswerCount;

    case 5:
        return problem.RuntimeLimit / 1000.0f;

    case 6:
        return problem.BestRuntime / 1000.0f;
    }

    return QVariant();
}

void uva::ProblemsTableModel::setCategoryRoot(std::shared_ptr<Category> root)
{
    beginResetModel();
    mCategoryRoot = root;
    endResetModel();
}

QVariant uva::ProblemsTableModel::style(const QModelIndex &index, int role) const
{
    // Try to find the problem in the category tree
    typedef Category::CategoryProblem CategoryProblem;
    const Problem problem = mProblemMap->value(mRowToId[index.row()]);
    CategoryProblem *categoryProblem = nullptr;

    if (mCategoryRoot)
    {
        auto it = mCategoryRoot->Problems.find(problem.ProblemNumber);
        if (it != mCategoryRoot->Problems.end())
        {
            categoryProblem = it->get();
        }
    }

    if (categoryProblem) {
        static QFont font;
        font.setBold(true);
        switch (role)
        {
        case Qt::FontRole:
            if (categoryProblem->IsStarred)
                return font;
            else
                break;

        case Qt::ToolTipRole:
            if (!categoryProblem->Note.isEmpty())
                return categoryProblem->Note;
            else
                break;
        }
    }

    if (role == Qt::BackgroundRole) {
        return QColor(48
            , 47 + qMin((int)(40 * problem.DACU / mAverageDACU), 40)
            , 47);

    }

    return QVariant();
}
