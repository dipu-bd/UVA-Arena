#include "submissionstablemodel.h"
#include "commons\conversion.h"
#include "commons\colorizer.h"

uva::SubmissionsTableModel::SubmissionsTableModel()
{
    insertColumns({ "Submission ID", "Problem Number", "Problem Title"
        , "Verdict ID", "Runtime", "Language", "Rank" });
}

void uva::SubmissionsTableModel::setSubmissionsList(QList<UserSubmission> submissions)
{
    beginResetModel();
    mSubmissions = submissions;
    endResetModel();
}

QVariant uva::SubmissionsTableModel::dataAtIndex(const QModelIndex &index) const
{
    if (!mProblemMap)
        return QVariant();

    UserSubmission userSubmission = mSubmissions[index.row()];
    Problem problem = mProblemMap->value(userSubmission.Submission.ProblemID);

    //"Submission ID", "Problem Number", "Problem Title"
    //    , "Verdict ID", "Runtime", "Language", "Rank"

    switch (index.column())
    {
    case 0:
        return userSubmission.Submission.SubmissionID;
    case 1:
        return problem.ProblemNumber;
    case 2:
        return problem.ProblemTitle;
    case 3:
        return Conversion::getVerdictName(userSubmission.Submission.SubmissionVerdict);
    case 4:
        return Conversion::getRuntime(userSubmission.Submission.Runtime);
    case 5:
        return Conversion::getLanguageName(userSubmission.Submission.SubmissionLanguage);
    case 6:
        return userSubmission.Submission.Rank;

    default:
        break;
    }

    return QVariant();
}

int uva::SubmissionsTableModel::dataCount() const
{
    return mSubmissions.count();
}

QVariant uva::SubmissionsTableModel::style(const QModelIndex &index, int role) const
{
    UserSubmission userSubmission = mSubmissions[index.row()];
    switch (role)
    {
    case Qt::TextColorRole:
        if (index.column() == 3)
            return Colorizer::getVerdictColor(userSubmission.Submission.SubmissionVerdict);
    default:
        break;
    }

    return QVariant();
}

void uva::SubmissionsTableModel::setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    mProblemMap = problemMap;
}
