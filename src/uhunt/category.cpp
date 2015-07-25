#include "category.h"

using namespace uva;

Category Category::fromJsonObject(const QJsonObject& jsonObject)
{
    Category node;
    QJsonArray::const_iterator it;

    node.Name = jsonObject["name"].toString();
    node.Note = jsonObject["note"].toString();

    QJsonArray problems = jsonObject["problems"].toArray();

    for (it = problems.begin(); it != problems.end(); ++it) {
        QJsonObject problemJson = it->toObject();
        int problemNumber = problemJson["pnum"].toInt();
        node.Problems.insert(problemNumber, {
            problemNumber
            , problemJson["note"].toString()
            , problemJson["star"].toBool()
        });
    }

    //add branches
    QJsonArray branch = jsonObject["branches"].toArray();
    for(it = branch.begin(); it != branch.end(); ++it) {
        Category child = Category::fromJsonObject(it->toObject());
        node.Branches.insert(child.Name, child);

        //also add problems from this sub branch to main branch
        for(CategoryProblem problem : child.Problems)
            node.Problems.insert(problem.Number, problem);

    }

    return node;
}
