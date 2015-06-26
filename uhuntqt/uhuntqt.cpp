#include <QFile>
#include <QJsonArray>
#include <QJsonDocument>
#include <QJsonObject>

#include "uhuntqt.h"


Uhuntqt::Uhuntqt()
{
}

QList<ProblemInfo>* Uhuntqt::getProblemList(const char* data, int size)
{
    QJsonDocument doc(QJsonDocument::fromRawData(data, size));

    QList<ProblemInfo>* problems = new QList<ProblemInfo>();
    if(!doc.isArray()) return problems;

    QJsonArray arr = doc.array();
    for(int i = 0; i < arr.size(); ++i)
    {
        if(!arr[i].isArray()) continue;
        problems->append(ProblemInfo(arr[i].toArray()));
    }

    return problems;
}
