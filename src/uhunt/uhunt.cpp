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
const QString API_USER_NAME_TO_ID = API_BASE + "/uname2uid/%1";     //%1 = userName
const QString API_PROBLEM_LIST = API_BASE + "/p";
const QString API_PROBLEM_BY_ID = API_BASE + "/p/id/%1";            //%1 = problem id
const QString API_SUBMISSIONS_TO_SPECIFIC_PROBLEMS = API_BASE + "/p/subs/%1/%2/%3"; // Not implemented
const QString API_PROBLEM_RANKLIST = API_BASE + "/p/rank/%1/%2/%3";   //%1 = pid, %2 = starting rank, %3 = count
const QString API_LIVE_EVENTS = API_BASE + "/poll/%1";  //%1 = userId
const QString API_USER_SUBMISSIONS = API_BASE + "/subs-user/%1/%2";       //%1 = userId, %2 = minimum Submission Id
const QString API_RANK_BY_USERID = API_BASE + "/ranklist/%1/%2/%3";   //%1 = userId, %2 = nabove, %3 = nbelow
const QString API_RANK_BY_POSITION = API_BASE + "/rank/%1/%2";           //%1 = startPos, %2 = count

// #TODO the following API calls can be implemented in the future
const QString API_USER_PROBLEM_RANKLIST = API_BASE + "/p/ranklist/%1/%2/%3/%4"; // Other name //%1 = pid-csv, %2= userids-csv, %3 = nabove %4 = nbelow
const QString API_PROBLEM_SUBS = API_BASE + "/p/subs/%1/%2/%3";   //%1 = pid, %2 = start time, %3 = end time
const QString API_USER_SUBMISSIONS_SPECIFIC_PROBLEMS = API_BASE + "/subs-pids/%1/%2/%3"; //%1 = userids-csv, %2 = pid-csv, %3 = min-subs-id
const QString API_USER_SUBMISSIONS_LAST = API_BASE + "/subs-user-last/%1/%2"; // Not implemented


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

Uhunt::ProblemMap Uhunt::problemMapFromJson(const QByteArray &json)
{
    Uhunt::ProblemMap problems;

    QJsonDocument doc = QJsonDocument::fromJson(json);

    if (!doc.isArray())
        return problems;

    QJsonArray arr = doc.array();
    QJsonArray::const_iterator it = arr.begin();

    while (it != arr.end()) {
        if (it->isArray()) {

            Problem problem = Problem::fromJsonArray(it->toArray());
            problems[problem.ProblemID] = problem;
        }
        it++;
    }

    return problems;
}

Uhunt::LiveEventMap Uhunt::liveEventMapFromJson(const QByteArray &json)
{
    Uhunt::LiveEventMap liveEventMap;

    //get a json document from data
    QJsonDocument doc = QJsonDocument::fromJson(json);

    const QJsonArray& arr = doc.array();
    QJsonArray::const_iterator it = arr.begin();

    while(it != arr.end())
    {
        if(it->isObject())
        {
            LiveEvent liveEvent = LiveEvent::fromJsonObject(it->toObject());
            liveEventMap[liveEvent.LiveEventID] = liveEvent;
        }

        it++;
    }

    return liveEventMap;
}

QList<UserRank> Uhunt::userRankingsFromJson(const QByteArray &json)
{
    QList<UserRank> ranks;

    //get json document
    QJsonDocument& jdoc = QJsonDocument::fromJson(json);

    const QJsonArray& jsonArray = jdoc.array();
    QJsonArray::const_iterator it = jsonArray.begin();

    while(it != jsonArray.end())
    {
        if(it->isObject())
            ranks.push_back(UserRank::fromJsonObject(it->toObject()));

        it++;
    }

    return ranks;
}

QList<UserSubmission> Uhunt::userSubmissionsFromJson(const QByteArray &json)
{
    QList<UserSubmission> submissions;

    //get json document
    QJsonDocument& jdoc = QJsonDocument::fromJson(json);

    QJsonArray arr = jdoc.array();
    QJsonArray::const_iterator it = arr.begin();

    while(it != arr.end())
    {
        if(it->isObject())
            submissions.push_back(UserSubmission::fromJsonObject(it->toObject()));

        it++;
    }

    return submissions;
}

QList<UserSubmission> Uhunt::userSubmissionsFromJson(const QByteArray &json, int userID)
{
    QList<UserSubmission> submissions;

    QJsonDocument doc = QJsonDocument::fromJson(json);

    QJsonObject jsonObject = doc.object();

    QString fullName = jsonObject["name"].toString();
    QString userName = jsonObject["uname"].toString();

    QJsonArray jsonArray = jsonObject["subs"].toArray();

    QJsonArray::const_iterator it = jsonArray.begin();

    while (it != jsonArray.end()) {

        if (it->isArray()) {
            UserSubmission submission;
            // ok to do all these assignments because Qt uses implicit sharing
            submission.FullName = fullName;
            submission.UserName = userName;
            submission.UserID = userID;
            submission.Submission = Submission::fromJsonArray(it->toArray());
        }

        it++;
    }

    return submissions;
}

void Uhunt::problemById(int problemID)
{
    QNetworkReply* reply = 
        createNetworkRequest(API_PROBLEM_BY_ID.arg(problemID));

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

void Uhunt::allOnlineJudgeProblems()
{
    QNetworkReply *reply = createNetworkRequest(API_PROBLEM_LIST);

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {

        if (reply->error() == QNetworkReply::NoError) {
            emit allProblemsDownloaded(reply->readAll());
        }

    });
}

void Uhunt::liveEvents(quint64 lastSubmissionID)
{
    QNetworkReply* reply =
            createNetworkRequest(API_LIVE_EVENTS.arg(lastSubmissionID));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->liveEventsDownloaded(
                        this->liveEventMapFromJson(reply->readAll())
                        );
            reply->deleteLater();
        });
}

void Uhunt::userIDFromUserName(const QString &userName)
{
    QNetworkReply* reply = 
        createNetworkRequest(API_USER_NAME_TO_ID.arg(userName));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply, userName]() {
            int id = reply->readAll().toInt();
            emit this->userIDDownloaded(userName, id);
            reply->deleteLater();
        });
}

void Uhunt::userSubmissionsByUserID(int userID, int lastSubmissionID)
{
    QNetworkReply* reply =
            createNetworkRequest(
                API_USER_SUBMISSIONS.arg(userID).arg(lastSubmissionID)
            );

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply, userID]() {
            emit this->userSubmissionsByUserIDDownloaded(
                userSubmissionsFromJson(reply->readAll(), userID));
            reply->deleteLater();
        });
}

void Uhunt::userRanksByUserID(int userID, int above /*= 10*/, int below /*= 10*/)
{
    QNetworkReply* reply =
        createNetworkRequest(
            API_RANK_BY_USERID.arg(userID).arg(above).arg(below)
            );

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->userRanksByUserIDDownloaded(
                        this->userRankingsFromJson(reply->readAll())
                    );
            reply->deleteLater();
        });
}

void Uhunt::userRanksByPosition(int startPos /*= 1*/, int count /*= 100*/)
{
    QNetworkReply* reply =
        createNetworkRequest(API_RANK_BY_POSITION.arg(startPos).arg(count));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->userRanksByPositionDownloaded(
                        this->userRankingsFromJson(reply->readAll())
                    );
            reply->deleteLater();
        });
}

void Uhunt::userSubmissionsByProblemID(int problemID, quint64 startTime, quint64 endTime)
{
    QNetworkReply* reply =
        createNetworkRequest(
        API_SUBMISSIONS_TO_SPECIFIC_PROBLEMS.arg(problemID)
                                            .arg(startTime)
                                            .arg(endTime));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->userSubmissionsByProblemIDDownloaded(
                    userSubmissionsFromJson(reply->readAll())
                    );
            reply->deleteLater();
        });
}

void Uhunt::userSubmissionsByProblemRank(int problemId, int startRank /*= 1*/, int count /*= 25*/)
{
    QNetworkReply* reply =
            createNetworkRequest(API_PROBLEM_RANKLIST.arg(problemId).arg(startRank).arg(count));

    if (reply == nullptr)
        return;

    QObject::connect(reply,
        &QNetworkReply::finished,
        [this, reply]() {
            emit this->userSubmissionsByProblemRankDownloaded(
                        userSubmissionsFromJson(reply->readAll())
                    );
            reply->deleteLater();
        });
}
