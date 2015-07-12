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

    //add branches
    const QJsonArray& branch = data["branches"].toArray();
    for(it = branch.begin(); it != branch.end(); ++it) {
        CategoryNode b = CategoryNode::fromJsonObject(it->toObject());
        node.addBranch(b);

        //also add problems from this sub branch to main branch
        for(Problem p : b.getProblems()) {
            node.addProblem(p);
        }
    }

    return node;
}
