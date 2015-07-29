#include "category.h"

using namespace uva;

Category::~Category()
{
    qDeleteAll(Problems);
    qDeleteAll(Branches);
}

Category * uva::Category::fromJson(const QByteArray &json)
{
    QJsonDocument document = QJsonDocument::fromJson(json);
    if (document.isNull() || !document.isObject())
        return nullptr;

    return fromJsonObject(document.object());
}

Category *Category::fromJsonObject(const QJsonObject& jsonObject)
{
    Category *node = new Category;
    node->Parent = nullptr;
    QJsonArray::const_iterator it;

    node->Name = jsonObject["name"].toString();
    node->Note = jsonObject["note"].toString();

    QJsonArray problems = jsonObject["problems"].toArray();

    for (it = problems.begin(); it != problems.end(); ++it) {
        QJsonObject problemJson = it->toObject();
        int problemNumber = problemJson["pnum"].toInt();
        node->Problems.insert(problemNumber, new CategoryProblem {
            problemNumber
            , problemJson["note"].toString()
            , problemJson["star"].toBool()
        });
    }

    //add branches
    QJsonArray branch = jsonObject["branches"].toArray();
    int row = 0;
    for(it = branch.begin(); it != branch.end(); ++it) {
        Category *child = Category::fromJsonObject(it->toObject());
        child->Parent = node;
        node->Branches.insert(child->Name, child);
    }

    return node;
}
