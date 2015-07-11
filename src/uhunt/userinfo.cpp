#include "userinfo.h"

using namespace uva;

UserInfo::UserInfo()
{

}

UserInfo::UserInfo(const QJsonObject &data)
{
    loadData(data);
}

UserInfo::UserInfo(const QByteArray &data)
{
    loadData(data);
}

void UserInfo::loadData(const QByteArray &data)
{
    loadData(QJsonDocument::fromJson(data).object());
}

void UserInfo::loadData(const QJsonObject &data)
{
    setFullName(data["name"].toString());
    setUserName(data["uname"].toString());

    const QJsonArray& jarr = data["subs"].toArray();

    for(int i = 0; i < jarr.count(); ++i)
    {
        // get instance of user submission
        UserSubmission usub(jarr[i].toArray());        

        //replace the old submission in the qmap
        mSubmissions[usub.getSubmissionID()] = usub;

        int pnum = usub.getProblemNumber();

        //the submission is accepted
        if(usub.isAccepted())
        {
            mSolved.insert(pnum);
            mTriedButFailed.remove(pnum);
        }
        //the submission is not inqueue and the poblem is not accepted
        else if(!usub.isInQueue() && !isAccepted(pnum))
        {
            mTriedButFailed.insert(pnum);
        }

        //update the last submission id
        if(!usub.isInQueue())
        {
            mLastSubmissionID = std::max(mLastSubmissionID, usub.getSubmissionID());
        }
    }
}

bool UserInfo::isAccepted(int problemNumber) const
{
    return mSolved.contains(problemNumber);
}

bool UserInfo::isTriedButNotSolved(int problemNumber) const
{
    return mTriedButFailed.contains(problemNumber);
}

bool UserInfo::isTried(int problemNumber) const
{
    return isTriedButNotSolved(problemNumber) || isAccepted(problemNumber);
}

