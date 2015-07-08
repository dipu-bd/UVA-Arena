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
const QString API_JUDGE_STATUS = API_BASE + "/poll";
const QString API_USER_NAME_TO_ID = API_BASE + "/uname2uid";

Uhuntqt::Uhuntqt(std::shared_ptr<QNetworkAccessManager> manager) :
    mNetworkManager(manager)
{
}

//
// SLOTS
//
void Uhuntqt::getProblemList()
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
        emit problemListDownloaded(
                    this->problemListFromData(reply->readAll())
                    );

        reply->deleteLater();
    });
}

void Uhuntqt::getJudgeStatus(int lastSubmissionID)
{
    if (!mNetworkManager)
        return;

    QString lastStatus = API_JUDGE_STATUS + "/" + QString::number(lastSubmissionID);

    QNetworkRequest request;
    request.setUrl(QUrl(lastStatus));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply] ()
    {
        emit judgeStatusDownloaded(
                    this->judgeStatusFromData(reply->readAll())
                    );

        reply->deleteLater();
    });
}

void Uhuntqt::getUserID(QString userName)
{
    if (!mNetworkManager)
        return;

    QString url = API_USER_NAME_TO_ID + "/" + userName;

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply* reply = mNetworkManager->get(request);

    QObject::connect(reply,
                     &QNetworkReply::finished,
                     [this, reply, userName]()
    {
        int id = reply->readAll().toInt();
        emit userIdDownloaded(userName, id);
        reply->deleteLater();
    });
}

//
// Other functions
//
QList<ProblemInfo> Uhuntqt::problemListFromData(const QByteArray& data)
{
    /*
        The data is a javascript multidimensional array.
        The format is like so:
        [[...],[...],[...]]
    */
    QList<ProblemInfo> problems;

    // get a json document from data
    QJsonDocument jdoc = QJsonDocument::fromJson(data);

    QJsonArray jarr = jdoc.array();
    for(int i = 0; i < jarr.count(); ++i)
    {
        problems.push_back(ProblemInfo(jarr[i].toArray()));
    }

    return problems;
}

QList<JudgeStatus> Uhuntqt::judgeStatusFromData(const QByteArray &data)
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
