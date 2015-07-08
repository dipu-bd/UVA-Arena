#include <QFile>
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>

#include <QNetworkRequest>
#include <QNetworkReply>

#include "uhuntqt.h"

using namespace uva;

const QString API_BASE = "http://uhunt.felix-halim.net/api";

const QUrl API_PROBLEM_LIST = API_BASE + "/p";

Uhuntqt::Uhuntqt(std::shared_ptr<QNetworkAccessManager> manager) :
    mNetworkManager(manager)
{
}

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

QList<ProblemInfo> Uhuntqt::problemListFromData(const QByteArray& data)
{
    /*
            The data is a javascript multidimensional array.
            The format is like so:
            [[...],[...],[...]]
        */
    QList<ProblemInfo> problems;

    QByteArray::const_iterator it = data.begin();

    // First character should be the beginning of the 2d array
    if (*it != '[')
        return QList<ProblemInfo>();

    // The iterator is now at the first array in the 2d array
    it++;

    while (it != data.end())
    {
        // make sure this is the beginning of the array
        if (*it != '[')
            continue;

        // consume open bracket
        it++;

        // now grab all the data in between [ ]'s
        QByteArray text;

        // true if we're current parsing a string (e.g., a title, a name)
        bool parsingString = false;

        while ((*it != ']' || parsingString) && it != data.end())
        {
            // at a new string or end of string
            if (*it == '"')
            {
                parsingString = !parsingString;
                // consume quotation marks
                it++;
            }

            text.append(*it);
            it++;
        }

        problems.push_back(ProblemInfo(text));

        // consume close bracket
        it++;

        // another closed bracket signals end of 2d array
        if (*it == ']')
            break;

        // consume comma
        it++;
    }

    return problems;
}
