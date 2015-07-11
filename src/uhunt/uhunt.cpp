#include <QFile>
#include <QDataStream>
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>
#include <QNetworkRequest>
#include <QNetworkReply>

#include "uhunt.h"

using namespace uva;

const QString API_BASE = "http://uhunt.felix-halim.net/api";
const QString API_PROBLEM_LIST = API_BASE + "/p";
const QString API_PROBLEM_BY_ID = API_BASE + "/p/id/%1";            //%1 = problem id
const QString API_JUDGE_STATUS = API_BASE + "/poll/%1";             //%1 = userId
const QString API_USER_NAME_TO_ID = API_BASE + "/uname2uid/%1";     //%1 = userName
const QString API_USER_INFO = API_BASE + "/subs-user/%1/%2";        //%1 = userId, %2 = lastSubmissionId
const QString API_RANK_BY_USER = API_BASE + "/ranklist/%1/%2/%3";   //%1 = userId, %2 = nabove, %3 = nbelow
const QString API_RANK_BY_POS = API_BASE + "/rank/%1/%2";           //%1 = startPos, %2 = count

Uhunt::Uhunt(std::shared_ptr<QNetworkAccessManager> manager) :
    mNetworkManager(manager)
{
}

QNetworkReply* Uhunt::createNetworkRequest(QString url)
{
    if (!mNetworkManager)
        return nullptr;

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply* reply = mNetworkManager->get(request);

    return reply;
}

//
// SLOTS
//
void Uhunt::getProblemById(int id)
{
    QNetworkReply* reply = createNetworkRequest(API_PROBLEM_BY_ID.arg(id));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            if (reply->error() == QNetworkReply::NoError)
            {
                emit problemByIdDownloaded(
                        Problem::fromJson(
                            reply->readAll()
                        )
                    );
            }
        });
}

void Uhunt::getProblemList()
{
    QNetworkReply *reply = createNetworkRequest(API_PROBLEM_LIST);

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            if (reply->error() == QNetworkReply::NoError) {
                emit problemListDownloaded(
                    this->problemListFromData(reply->readAll())
                    );
            }

            reply->deleteLater();
        });
}

void Uhunt::getProblemListAsByteArray()
{
    QNetworkReply *reply = createNetworkRequest(API_PROBLEM_LIST);

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {

            if (reply->error() == QNetworkReply::NoError) {

                emit problemListByteArrayDownloaded(reply->readAll());

            }

        });
}

void Uhunt::getJudgeStatus(int lastSubmissionID)
{
    QNetworkReply* reply = createNetworkRequest(API_JUDGE_STATUS.arg(lastSubmissionID));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->judgeStatusDownloaded(
                        this->judgeStatusFromData(reply->readAll())
                        );
            reply->deleteLater();
        });
}

void Uhunt::getUserID(const QString &userName)
{
    QNetworkReply* reply = createNetworkRequest(API_USER_NAME_TO_ID.arg(userName));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply, userName]() {
            int id = reply->readAll().toInt();
            emit this->userIdDownloaded(userName, id);
            reply->deleteLater();
        });
}

void Uhunt::getUserInfo(int userId)
{
    QNetworkReply* reply = createNetworkRequest(API_USER_INFO.arg(userId).arg(0));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply, userId]() {
            UserInfo uinfo(reply->readAll());
            uinfo.setUserId(userId);
            emit this->userInfoDownloaded(uinfo);
            reply->deleteLater();
        });
}

void Uhunt::updateUserInfo(const UserInfo &uinfo)
{
    QNetworkReply* reply =
        createNetworkRequest(API_USER_INFO.arg(uinfo.getUserId()).arg(uinfo.getLastSubmissionID()));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            UserInfo info;
            info.loadData(reply->readAll());
            emit this->userInfoUpdated(info);
            reply->deleteLater();
        });
}

void Uhunt::getRankByUser(int userId, int nAbove, int nBelow)
{
    QNetworkReply* reply =
        createNetworkRequest(API_RANK_BY_USER.arg(userId).arg(nAbove).arg(nBelow));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->rankByUserDownloaded(
                        this->rankListFromData(reply->readAll())
                    );
            reply->deleteLater();
        });
}

void Uhunt::getRankByPosition(int startPos, int count)
{
    QNetworkReply* reply =
        createNetworkRequest(API_RANK_BY_POS.arg(startPos).arg(count));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->rankByPositionDownloaded(
                        this->rankListFromData(reply->readAll())
                    );
            reply->deleteLater();
        });
}

//
// Other functions
//
QList<Problem> Uhunt::problemListFromData(const QByteArray& data)
{
    QList<Problem> problems;

    QJsonDocument doc = QJsonDocument::fromJson(data);

    if (!doc.isArray())
        return problems;

    QJsonArray arr = doc.array();

    QJsonArray::const_iterator it = arr.begin();

    while (it != arr.end())
    {
        if (it->isArray())
            problems.push_back(Problem::fromJsonArray(it->toArray()));

        it++;
    }

    return problems;
}

QList<JudgeStatus> Uhunt::judgeStatusFromData(const QByteArray &data)
{
    /*
        The data is a javascript array of dictionary of following format:
        [{...},{...}]
     */
    QList<JudgeStatus> status;

    //get a json document from data
    QJsonDocument jdoc = QJsonDocument::fromJson(data);

    QJsonArray jarr = jdoc.array();
    for(int i = 0; i < jarr.count(); ++i)
    {
        status.push_back(JudgeStatus(jarr[i].toObject()));
    }

    return status;
}

QList<RankInfo> Uhunt::rankListFromData(const QByteArray &data)
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
