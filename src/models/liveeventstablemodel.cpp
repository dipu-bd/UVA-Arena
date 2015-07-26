#include "liveeventstablemodel.h"
#include "commons/conversion.h"
#include "commons/colorizer.h"

using namespace uva;

LiveEventsTableModel::LiveEventsTableModel()
{
    insertColumns({
        "SID", "User Name", "Full Name", "Problem Number",
        "Problem Title", "Language", "Verdict",
        "Runtime", "Rank", "Submission Time"
    });
}

void LiveEventsTableModel::setStatusData(Uhunt::LiveEventMap statusData)
{
    beginResetModel();

    //add new data to the list
    Uhunt::LiveEventMap::iterator it;
    for (it = statusData.begin(); it != statusData.end(); ++it)
        mStatusData[it.key()] = it.value();

    //remove few if status data is > 100
    while (mStatusData.count() > 100)
        mStatusData.remove(mStatusData.firstKey());

    mRowToId.clear();

    for(it = mStatusData.begin(); it != mStatusData.end(); ++it) {

        //push to front to reverse the order
        mRowToId.push_front(it->LiveEventID);
        // #TODO fix judge status table model new status data
    }

    endResetModel();
}

void LiveEventsTableModel::setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    mProblemMap = problemMap;
}

int LiveEventsTableModel::getDataCount() const
{
    return mStatusData.count();
}

QVariant LiveEventsTableModel::getDataAtIndex(const QModelIndex &index) const
{
    if (!mProblemMap)
        return QVariant();

    if(mRowToId.count() <= index.row())
        return QVariant();

    /*
        "SID", "User Name", "Full Name", "Problem Number",
        "Problem Title", "Language", "Verdict",
        "Runtime", "Rank", "Submission Time"
    */

    const LiveEvent &event = mStatusData[mRowToId[index.row()]];
    const UserSubmission &userSubmission = event.UserSubmission;
    const Submission &submission = userSubmission.Submission;
    // #TODO use something better than a switch here
    switch (index.column()) {
    case 0:
        return submission.SubmissionID;

    case 1:
        return userSubmission.UserName;

    case 2:
        return userSubmission.FullName;

    case 3:
        return mProblemMap->value(submission.ProblemID).ProblemNumber; 

    case 4:
        return mProblemMap->value(submission.ProblemID).ProblemTitle;

    case 5:
        return Conversion::getLanguageName(
                    submission.SubmissionLanguage
                );

    case 6:
        return Conversion::getVerdictName(
                    submission.SubmissionVerdict
                );

    case 7:
        return Conversion::getRuntime(
                    submission.Runtime
                    );

    case 8:
        if(submission.Rank < 0)
            return "-";
        else
            return submission.Rank;

    case 9:
        return Conversion::getSubmissionTime(
                    submission.TimeSubmitted
                    );

    }

    return QVariant();
}

qint64 LiveEventsTableModel::getLastSubmissionId()
{
    if(mStatusData.count() > 0)
        return mStatusData.lastKey();
    return 0;
}

QVariant uva::LiveEventsTableModel::style(const QModelIndex &index, int role) const
{
    switch (role)
    {
    case Qt::ForegroundRole:
        switch (index.column())
        {
        case 0: //submission id
            return QBrush(Colorizer::tan);
        case 1: //username
            return QBrush(Colorizer::goldenRod);
        case 2: //full name
            return QBrush(Colorizer::lightCoral);
        case 3: //number
            return QBrush(Colorizer::antiqueWhite);
        case 4: //title
            return QBrush(Colorizer::cyan);
        case 5: //language
            return QBrush(Colorizer::burlyWood);
        case 6: //verdict
                return QBrush(Colorizer::getVerdictColor(
                    (Submission::Verdict)mStatusData[mRowToId[index.row()]]
                    .UserSubmission.Submission
                    .SubmissionVerdict));
        case 7: //runtime
            return QBrush(Colorizer::cornsilk);
        case 8: //rank
            return QBrush(Colorizer::gold);
        case 9: //submission time
            return QBrush(Colorizer::snow);
        default:
            return QBrush(Colorizer::white);
        }
    }

    return QVariant();
}
