#include "submissionstablemodel.h"
#include "commons\conversion.h"
#include "commons\colorizer.h"

uva::SubmissionsTableModel::SubmissionsTableModel()
{
    insertColumns({ "User ID", "Username", "Submission ID"
        , "Problem ID", "Verdict ID"
        , "Runtime", "Language", "Rank" });
}

void uva::SubmissionsTableModel::setSubmissionsList(QList<UserSubmission> submissions)
{
    beginResetModel();
    mSubmissions = submissions;
    endResetModel();
}

QVariant uva::SubmissionsTableModel::dataAtIndex(const QModelIndex &index) const
{
    UserSubmission userSubmission = mSubmissions[index.row()];

    switch (index.column())
    {
    case 0:
        return userSubmission.UserID;
    case 1:
        return userSubmission.UserName;
    case 2:
        return userSubmission.Submission.SubmissionID;
    case 3:
        return userSubmission.Submission.ProblemID;
    case 4:
        return Conversion::getVerdictName(userSubmission.Submission.SubmissionVerdict);
    case 5:
        return Conversion::getRuntime(userSubmission.Submission.Runtime);
    case 6:
        return Conversion::getLanguageName(userSubmission.Submission.SubmissionLanguage);
    case 7:
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
        if (index.column() == 4)
            return Colorizer::getVerdictColor(userSubmission.Submission.SubmissionVerdict);
    default:
        break;
    }

    return QVariant();
}
