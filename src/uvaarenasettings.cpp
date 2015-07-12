#include "uvaarenasettings.h"

using namespace uva;

const QString KeyUserName = "UserName";
const QString DefaultUserName = "";

const QString KeyUserId = "UserId";
const qint32 DefaultUserId = -1;

const QString KeyMaxDaysUntilProblemListRedownload =
    "MaxDaysUntilProblemListRedownload";
const qint64 DefaultMaxDaysUntilProblemListRedownload = 1;

QString UVAArenaSettings::userName()
{
    return settings.value(KeyUserName, DefaultUserName).toString();
}

void UVAArenaSettings::setUserName(QString userName)
{
    settings.setValue(KeyUserName, userName);
}

qint32 UVAArenaSettings::userId()
{
    return settings.value(KeyUserId, DefaultUserId).toInt();
}

void UVAArenaSettings::setUserId(qint32 userId)
{
    settings.setValue(KeyUserId, userId);
}

qint64 UVAArenaSettings::maxDaysUntilProblemListRedownload()
{
    return settings.value(KeyMaxDaysUntilProblemListRedownload,
        DefaultMaxDaysUntilProblemListRedownload).toLongLong();
}

void UVAArenaSettings::setMaxDaysUntilProblemListRedownload(qint64 days)
{
    settings.setValue(KeyMaxDaysUntilProblemListRedownload, days);
}
