#include "usersubmission.h"

using namespace uva;

UserSubmission UserSubmission::fromJsonObject(const QJsonObject& data)
{
    UserSubmission userSubmission;

    //name: full name of the user
    userSubmission.FullName = data["name"].toString();
    //uname: user name
    userSubmission.UserName = data["uname"].toString();
    
    // uid: user ID
    // The LiveEvents API returns json objects with the "uid" field,
    // but the User Submissions API does not since the userid field
    // is used as part of the URL
    if (!data["uid"].isNull())
        userSubmission.UserID = data["uid"].toInt();

    userSubmission.Submission = Submission::fromJsonObject(data);

    return userSubmission;
}
