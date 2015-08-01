#include "category.h"

using namespace uva;

std::shared_ptr<Category> uva::Category::fromJson(const QByteArray &json)
{
    QJsonDocument document = QJsonDocument::fromJson(json);
    if (document.isNull() || !document.isObject())
        return nullptr;

    return fromJsonObject(document.object());
}

std::shared_ptr<Category> uva::Category::fromJsonObject(const QJsonObject& jsonObject)
{
using namespace std;

    shared_ptr<Category> node(new Category);

    QJsonArray::const_iterator it;

    node->Name = jsonObject["name"].toString();
    node->Note = jsonObject["note"].toString();

    QJsonArray problems = jsonObject["problems"].toArray();

    for (it = problems.begin(); it != problems.end(); ++it) {
        QJsonObject problemJson = it->toObject();
        int problemNumber = problemJson["pnum"].toInt();

        node->Problems.insert(problemNumber, 
            make_shared<CategoryProblem>(CategoryProblem {
            problemNumber
            , problemJson["note"].toString()
            , problemJson["star"].toBool()
        }));
    }

    //add branches
    QJsonArray branch = jsonObject["branches"].toArray();
    int row = 0;

    for(it = branch.begin(); it != branch.end(); ++it) {
        std::shared_ptr<Category> child = Category::fromJsonObject(it->toObject());
        child->Parent = node;
        node->Branches.insert(child->Name, child);

        // add all of the child's problems
        
        for (auto problem : child->Problems)
            node->Problems.insert(problem->Number, problem);

    }

    return node;
}
