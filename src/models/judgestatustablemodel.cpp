#include "judgestatustablemodel.h"
#include "commons/conversion.h"

using namespace uva;

JudgeStatusTableModel::JudgeStatusTableModel() :
    mStatusData(nullptr)
{
    insertColumns({
        "SID", "User Name", "Full Name", "Problem Number",
        "Problem Title", "Language", "Verdict",
        "Runtime", "Rank", "Submission Time"
    });
}

void JudgeStatusTableModel::setStatusData(std::shared_ptr<QList<JudgeStatus> > statusData,
                                          std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    beginResetModel();

    mStatusData = statusData;

    QList<JudgeStatus>::iterator it = mStatusData->begin();
    QList<JudgeStatus>::const_iterator end = mStatusData->end();

    while (it != end) {
        it->setProblemNumber(problemMap->value(it->getProblemID()).getNumber());
        it->setProblemTitle(problemMap->value(it->getProblemID()).getTitle());
        ++it;
    }

    endResetModel();
}

int JudgeStatusTableModel::getDataCount() const
{
    if (!mStatusData)
        return 0;

    return mStatusData->count();
}

QVariant JudgeStatusTableModel::getDataAtIndex(const QModelIndex &index) const
{
    if (!mStatusData)
        return QVariant();

    /*
        "SID", "User Name", "Full Name", "Problem Number",
        "Problem Title", "Language", "Verdict",
        "Runtime", "Rank", "Submission Time"
    */

    switch (index.column()) {
    case 0:
        return mStatusData->at(index.row()).getSubmissionID();

    case 1:
        return mStatusData->at(index.row()).getUserName();

    case 2:
        return mStatusData->at(index.row()).getFullName();

    case 3:
       return mStatusData->at(index.row()).getProblemNumber();

    case 4:
        return mStatusData->at(index.row()).getProblemID();

    case 5:
        return Conversion::getLangaugeName(
                    mStatusData->value(index.row()).getLanguage()
                );

    case 6:
        return Conversion::getVerdictName(
                    mStatusData->at(index.row()).getVerdict()
                );

    case 7:
        return Conversion::getRuntime(
                    mStatusData->at(index.row()).getRuntime()
                    );

    case 8:
        return mStatusData->at(index.row()).getRank();

    case 9:
        return Conversion::getSubmissionTime(
                    mStatusData->at(index.row()).getSubmissionTime()
                    );

    }

    return QVariant();
}
