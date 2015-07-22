#include "categorynode.h"

using namespace uva;

CategoryNode::CategoryNode()
{

}

CategoryNode CategoryNode::fromJsonObject(const QJsonObject& data)
{
    CategoryNode node;
    QJsonArray::const_iterator it;

    node.setName(data["name"].toString());
    node.setNote(data["note"].toString());

    const QJsonArray& problems = data["problems"].toArray();

    for (it = problems.begin(); it != problems.end(); ++it) {
        QJsonObject problemJson = it->toObject();

        node.addProblem({ 
            problemJson["note"].toString(),
            problemJson["pnum"].toInt(),
            problemJson["star"].toBool() 
        });
    }

    //add branches
    const QJsonArray& branch = data["branches"].toArray();
    for(it = branch.begin(); it != branch.end(); ++it) {
        CategoryNode child = CategoryNode::fromJsonObject(it->toObject());
        node.addBranch(child);

        //also add problems from this sub branch to main branch
        for(CategoryProblem problem : child.getProblems()) {
            node.addProblem(problem);
        }
    }

    return node;
}
