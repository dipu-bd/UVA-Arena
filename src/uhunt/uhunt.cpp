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
const QString API_PROBLEM_SUBS = API_PROBLEM_LIST + "/subs/%1/%2/%3";   //%1 = pid, %2 = start time, %3 = end time
const QString API_PROBLEM_RANK = API_PROBLEM_LIST + "/rank/%1/%2/%3";   //%1 = pid, %2 = starting rank, %3 = count
const QString API_PROBLEM_USER_RANK = API_PROBLEM_LIST + "/ranklist/%1/%2/%3/%4";    //%1 = pid-csv, %2= userids-csv, %3 = nabove %4 = nbelow
const QString API_USER_PROBLEM_SUBS = API_BASE + "/subs-pids/%1/%2/%3"; //%1 = userids-csv, %2 = pid-csv, %3 = min-subs-id

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

Uhunt::ProblemMap Uhunt::problemMapFromData(const QByteArray& data)
{
    ProblemMap problems;

    QJsonDocument doc = QJsonDocument::fromJson(data);

    if (!doc.isArray())
        return problems;

    QJsonArray arr = doc.array();
    QJsonArray::const_iterator it = arr.begin();

    while (it != arr.end()) {
        if (it->isArray()) {

            Problem problem = Problem::fromJsonArray(it->toArray());
            problems[problem.getNumber()] = problem;
        }
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

    const QJsonArray& arr = jdoc.array();
    QJsonArray::const_iterator it = arr.begin();

    while(it != arr.end())
    {
        if(it->isObject())
            status.push_back(JudgeStatus::fromJsonObject(it->toObject()));

        it++;
    }

    return status;
}

QList<RankInfo> Uhunt::rankListFromData(const QByteArray &data)
{
    QList<RankInfo> ranks;

    //get json document
    QJsonDocument& jdoc = QJsonDocument::fromJson(data);

    const QJsonArray& arr = jdoc.array();
    QJsonArray::const_iterator it = arr.begin();

    while(it != arr.end())
    {
        if(it->isObject())
            ranks.push_back(RankInfo::fromJsonObject(it->toObject()));

        it++;
    }

    return ranks;
}

QList<SubmissionMessage> Uhunt::submissionListFromData(const QByteArray &data)
{
    QList<SubmissionMessage> subs;

    //get json document
    QJsonDocument& jdoc = QJsonDocument::fromJson(data);

    const QJsonArray& arr = jdoc.array();
    QJsonArray::const_iterator it = arr.begin();

    while(it != arr.end())
    {
        if(it->isObject())
            subs.push_back(SubmissionMessage::fromJsonObject(it->toObject()));

        it++;
    }

    return subs;
}

UserInfo Uhunt::userSubsOnProblemFromData(int userId, const QByteArray &data)
{
    //get json document
    QJsonDocument& doc = QJsonDocument::fromJson(data);

    //get main object
    const QJsonObject& obj = doc.object();

    //get userinfo object
    const QJsonObject& user = obj[QString::number(userId)].toObject();

    return UserInfo::fromJsonObject(userId, user);
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
                    this->problemMapFromData(reply->readAll())
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

void Uhunt::getUserInfoData(int userId, int lastSub)
{
    QNetworkReply* reply =
            createNetworkRequest(API_USER_INFO.arg(userId).arg(lastSub));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply, userId, lastSub]() {
            emit this->userInfoDataDownloaded(reply->readAll(), userId, lastSub);
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

void Uhunt::getSubmissionOnProblem(int problemId, int startTime, int endTime)
{
    QNetworkReply* reply =
            createNetworkRequest(API_PROBLEM_SUBS.arg(problemId).arg(startTime).arg(endTime));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->submissionOnProblemDownloaded(
                    submissionListFromData(reply->readAll())
                    );
            reply->deleteLater();
        });
}

void Uhunt::getRanklistOnProblem(int problemId, int startRank, int count)
{
    QNetworkReply* reply =
            createNetworkRequest(API_PROBLEM_RANK.arg(problemId).arg(startRank).arg(count));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->ranklistOnProblemDownloaded(
                    submissionListFromData(reply->readAll())
                    );
            reply->deleteLater();
        });
}

void Uhunt::getUserRankOnProblem(int problemId, int userId, int nAbove, int nBelow)
{
    QNetworkReply* reply =
            createNetworkRequest(API_PROBLEM_USER_RANK.arg(problemId).arg(userId).arg(nAbove).arg(nBelow));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->userRankOnProblemDownloaded(
                    submissionListFromData(reply->readAll())
                    );
            reply->deleteLater();
        });
}

void Uhunt::getUserSubmissionOnProblem(int userId, int problemId, int minSubsID)
{
    QNetworkReply* reply =
            createNetworkRequest(API_USER_PROBLEM_SUBS.arg(userId).arg(problemId).arg(minSubsID));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply, userId]() {

            emit this->userSubmissionOnProblemDownloaded(
                        userSubsOnProblemFromData(userId, reply->readAll())
                    );
            reply->deleteLater();
        });
}
