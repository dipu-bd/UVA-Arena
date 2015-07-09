#include <QFile>
#include <QDataStream>
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>
#include <QNetworkRequest>
#include <QNetworkReply>

#include "uhuntqt.h"

using namespace uva;

const QString API_BASE = "http://uhunt.felix-halim.net/api";
const QString API_PROBLEM_LIST = API_BASE + "/p";
const QString API_JUDGE_STATUS = API_BASE + "/poll/%1";             //%1 = userId
const QString API_USER_NAME_TO_ID = API_BASE + "/uname2uid/%1";     //%1 = userName
const QString API_USER_INFO = API_BASE + "/subs-user/%1/%2";        //%1 = userId, %2 = lastSubmissionId
const QString API_RANK_BY_USER = API_BASE + "/ranklist/%1/%2/%3";   //%1 = userId, %2 = nabove, %3 = nbelow
const QString API_RANK_BY_POS = API_BASE + "/rank/%1/%2";            //%1 = startPos, %2 = count

UhuntQt::UhuntQt(std::shared_ptr<QNetworkAccessManager> manager) :
    mNetworkManager(manager)
{
}

//
// SLOTS
//
void UhuntQt::getProblemList()
{
    if (!mNetworkManager)
        return;

    QNetworkRequest request;
    request.setUrl(QUrl(API_PROBLEM_LIST));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply] ()
    {
        emit this->problemListDownloaded(
                    this->problemListFromData(reply->readAll())
                    );
        reply->deleteLater();
    });
}

void UhuntQt::getJudgeStatus(int lastSubmissionID)
{
    if (!mNetworkManager)
        return;

    QString lastStatus = API_JUDGE_STATUS.arg(lastSubmissionID);

    QNetworkRequest request;
    request.setUrl(QUrl(lastStatus));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply] ()
    {
        emit this->judgeStatusDownloaded(
                    this->judgeStatusFromData(reply->readAll())
                    );
        reply->deleteLater();
    });
}

void UhuntQt::getUserID(QString userName)
{
    if (!mNetworkManager)
        return;

    QString url = API_USER_NAME_TO_ID.arg(userName);

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply, userName]()
    {
        int id = reply->readAll().toInt();
        emit this->userIdDownloaded(userName, id);
        reply->deleteLater();
    });
}

void UhuntQt::getUserInfo(int userId)
{
    if (!mNetworkManager)
        return;

    QString url = API_USER_INFO.arg(userId).arg(0);

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply, userId]()
    {
        UserInfo uinfo(reply->readAll());
        uinfo.setUserId(userId);
        emit this->userInfoDownloaded(uinfo);
        reply->deleteLater();
    });
}

void UhuntQt::updateUserInfo(UserInfo &uinfo)
{
    if (!mNetworkManager)
        return;

    QString url = API_USER_INFO.arg(uinfo.getUserId()).arg(uinfo.getLastSubmissionID());

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply, &uinfo]()
    {
        uinfo.loadData(reply->readAll());
        emit this->userInfoUpdated(uinfo);
        reply->deleteLater();
    });
}

void UhuntQt::getRankByUser(int userId, int nAbove, int nBelow)
{
    if (!mNetworkManager)
        return;

    QString url = API_RANK_BY_USER.arg(userId).arg(nAbove).arg(nBelow);

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply]()
    {
        emit this->rankByUserDownloaded(
                    this->rankListFromData(reply->readAll())
                );
        reply->deleteLater();
    });
}

void UhuntQt::getRankByPosition(int startPos, int count)
{
    if (!mNetworkManager)
        return;

    QString url = API_RANK_BY_POS.arg(startPos).arg(count);

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply]()
    {
        emit this->rankByPositionDownloaded(
                    this->rankListFromData(reply->readAll())
                );
        reply->deleteLater();
    });
}

//
// Other functions
//
QList<ProblemInfo> UhuntQt::problemListFromData(const QByteArray& data)
{
    /*
        The data is a javascript multidimensional array.
        The format is like so:
        [[...],[...],[...]]
    */
    QList<ProblemInfo> problems;

    // get a json document from data
    QJsonDocument& jdoc = QJsonDocument::fromJson(data);

    const QJsonArray& jarr = jdoc.array();
    for(int i = 0; i < jarr.count(); ++i)
    {
        problems.push_back(ProblemInfo(jarr[i].toArray()));
    }

    return problems;
}

QList<JudgeStatus> UhuntQt::judgeStatusFromData(const QByteArray &data)
{
    /*
        The data is a javascript array of dictionary of following format:
        [{...},{...}]
     */
    QList<JudgeStatus> status;

    //get a json document from data
    QJsonDocument& jdoc = QJsonDocument::fromJson(data);

    const QJsonArray& jarr = jdoc.array();
    for(int i = 0; i < jarr.count(); ++i)
    {
        status.push_back(JudgeStatus(jarr[i].toObject()));
    }

    return status;
}

QList<RankInfo> UhuntQt::rankListFromData(const QByteArray &data)
{
    /*
        Data is a javascript array of objects. Formatted like this-
        [{...},{...}]
     */
    QList<RankInfo> ranks;

    //get json document
    QJsonDocument& jdoc = QJsonDocument::fromJson(data);

    const QJsonArray& jarr = jdoc.array();
    for(int i = 0; i < jarr.count(); ++i)
    {
        ranks.push_back(RankInfo(jarr[i].toObject()));
    }

    return ranks;
}
