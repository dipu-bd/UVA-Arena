#include "problemstablemodel.h"
#include "QBrush"
#include "QFont"

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

    const Problem problem = mProblemMap->value(mRowToId[index.row()]);

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

void uva::ProblemsTableModel::setCategoryRoot(std::shared_ptr<Category> root)
{
    beginResetModel();
    mCategoryRoot = root;
    endResetModel();
}

QVariant uva::ProblemsTableModel::style(const QModelIndex &index, int role) const
{
    // #TODO Style things aesthetically
    // #TODO Style rows based on the category it belongs to
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

    return QVariant();
}
