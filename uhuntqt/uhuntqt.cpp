#include <QFile>
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>

#include <QNetworkRequest>
#include <QNetworkReply>

#include "uhuntqt.h"

using namespace uhunqt;

const QString API_BASE = "http://uhunt.felix-halim.net/api";

const QUrl API_PROBLEM_LIST = API_BASE + "/p";

Uhuntqt::Uhuntqt(QNetworkAccessManager* manager) :
    mNetworkManager(manager)
{

}

void Uhuntqt::getProblemList()
{
    if (mNetworkManager == nullptr)
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

QList<ProblemInfo> Uhuntqt::problemListFromData(QByteArray data)
{
    QJsonDocument doc = QJsonDocument::fromBinaryData(data);

    QList<ProblemInfo> problems;

    if (!doc.isArray()) return problems;

    QJsonArray arr = doc.array();
    for (int i = 0; i < arr.size(); ++i)
    {
        if (!arr[i].isArray()) continue;
        problems.append(ProblemInfo(arr[i].toArray()));
    }

    return problems;
}
