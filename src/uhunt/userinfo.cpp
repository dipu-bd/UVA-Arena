#include "userinfo.h"

using namespace uva;

UserInfo::UserInfo()
{
    setUserId(0);
}

UserInfo UserInfo::fromJson(int userId, const QByteArray &data)
{
    QJsonDocument& doc = QJsonDocument::fromJson(data);
    return UserInfo::fromJsonObject(userId, doc.object());
}

UserInfo UserInfo::fromJsonObject(int userId, const QJsonObject &data)
{
    UserInfo info;

    info.setUserId(userId);
    info.setFullName(data["name"].toString());
    info.setUserName(data["uname"].toString());
    info.addUserSubmission(data["subs"].toArray());

    return info;
}

void UserInfo::addUserSubmission(const QByteArray& json)
{
    const QJsonDocument& doc = QJsonDocument::fromJson(json);
    const QJsonObject& data = doc.object();
    setFullName(data["name"].toString());
    setUserName(data["uname"].toString());
    addUserSubmission(data["subs"].toArray());
}

void UserInfo::addUserSubmission(const QJsonArray& arr)
{
    QJsonArray::const_iterator it = arr.begin();
    while(it != arr.end())
    {
        if(it->isArray())
            addUserSubmission(UserSubmission::fromJsonArray(it->toArray()));

        it++;
    }
}

void UserInfo::addUserSubmission(UserSubmission usub)
{
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

