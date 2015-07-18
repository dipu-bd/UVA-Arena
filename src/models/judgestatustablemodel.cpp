#include "judgestatustablemodel.h"
#include "commons/conversion.h"

using namespace uva;

JudgeStatusTableModel::JudgeStatusTableModel()
{
    insertColumns({
        "SID", "User Name", "Full Name", "Problem Number",
        "Problem Title", "Language", "Verdict",
        "Runtime", "Rank", "Submission Time"
    });
}

void JudgeStatusTableModel::setStatusData(Uhunt::JudgeStatusMap statusData,
                                          std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    beginResetModel();

    updateStatusData(statusData);

    mRowToId.clear();

    Uhunt::JudgeStatusMap::iterator it;
    for(it = mStatusData.begin(); it != mStatusData.end(); ++it) {

        //push to front to reverse the order
        mRowToId.push_front(it->getSubmissionID());

        //update number and title
        it->setProblemNumber(problemMap->value(it->getProblemID()).getNumber());
        it->setProblemTitle(problemMap->value(it->getProblemID()).getTitle());
    }

    endResetModel();
}

int JudgeStatusTableModel::getDataCount() const
{
    return mStatusData.count();
}

QVariant JudgeStatusTableModel::getDataAtIndex(const QModelIndex &index) const
{
    if(mRowToId.count() <= index.row())
        return QVariant();

    /*
        "SID", "User Name", "Full Name", "Problem Number",
        "Problem Title", "Language", "Verdict",
        "Runtime", "Rank", "Submission Time"
    */

    switch (index.column()) {
    case 0:
        return mStatusData[mRowToId[index.row()]].getSubmissionID();

    case 1:
        return mStatusData[mRowToId[index.row()]].getUserName();

    case 2:
        return mStatusData[mRowToId[index.row()]].getFullName();

    case 3:
       return mStatusData[mRowToId[index.row()]].getProblemNumber();

    case 4:
        return mStatusData[mRowToId[index.row()]].getProblemTitle();

    case 5:
        return Conversion::getLangaugeName(
                    mStatusData[mRowToId[index.row()]].getLanguage()
                );

    case 6:
        return Conversion::getVerdictName(
                    mStatusData[mRowToId[index.row()]].getVerdict()
                );

    case 7:
        return Conversion::getRuntime(
                    mStatusData[mRowToId[index.row()]].getRuntime()
                    );

    case 8:
        if(mStatusData[mRowToId[index.row()]].getRank() < 0)
            return "-";
        else
            return mStatusData[mRowToId[index.row()]].getRank();

    case 9:
        return Conversion::getSubmissionTime(
                    mStatusData[mRowToId[index.row()]].getSubmissionTime()
                    );

    }

    return QVariant();
}

qint64 JudgeStatusTableModel::getLastSubmissionId()
{
    if(mStatusData.count() > 0)
        return mStatusData.lastKey();
    return 0;
}

QVariant JudgeStatusTableModel::getModelDataAtIndex(const QModelIndex &index) const
{
    if (mRowToId.count() <= index.row())
        return QVariant();

    /*
    "SID", "User Name", "Full Name", "Problem Number",
    "Problem Title", "Language", "Verdict",
    "Runtime", "Rank", "Submission Time"
    */

    switch (index.column()) {
    case 0:
        return mStatusData[mRowToId[index.row()]].getSubmissionID();

    case 1:
        return mStatusData[mRowToId[index.row()]].getUserName();

    case 2:
        return mStatusData[mRowToId[index.row()]].getFullName();

    case 3:
        return mStatusData[mRowToId[index.row()]].getProblemNumber();

    case 4:
        return mStatusData[mRowToId[index.row()]].getProblemTitle();

    case 5:
        return mStatusData[mRowToId[index.row()]].getLanguage();

    case 6:
        return mStatusData[mRowToId[index.row()]].getVerdict();

    case 7:
        return mStatusData[mRowToId[index.row()]].getRuntime();

    case 8:
        return mStatusData[mRowToId[index.row()]].getRank();

    case 9:
        return mStatusData[mRowToId[index.row()]].getSubmissionTime();
    }

    return ArenaTableModel::getModelDataAtIndex(index);
}

void JudgeStatusTableModel::updateStatusData(Uhunt::JudgeStatusMap statusData)
{
    //add new data to the list
    Uhunt::JudgeStatusMap::iterator it;
    for (it = statusData.begin(); it != statusData.end(); ++it)
    {
        mStatusData[it.key()] = it.value();
    }

    //remove few if status data is > 100
    while(mStatusData.count() > 100)
    {
        mStatusData.remove(mStatusData.firstKey());
    }
}
