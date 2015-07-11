#include "categorynode.h"
#include "uhuntdatabase.h"

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
    for(it = branch.begin(); it != branch.end(); ++it)
    {
        CategoryNode b = CategoryNode::fromJsonObject(it->toObject());
        node.addBranch(b);

        //also add problems from this sub branch to main branch
        for(Problem p : b.getProblems())
        {
            node.addProblem(p);
        }
    }

    //add problems
    const QJsonArray& prob = data["problems"].toArray();
    for(it = prob.begin(); it != prob.end(); ++it)
    {
        const QJsonObject& ob = it->toObject();
        int pnum = ob["pnum"].toInt();
        if(UhuntDatabase::isAvaiable())
        {
            Problem p = UhuntDatabase::getProblemByNumber(pnum);
            p.setStar(ob["star"].toBool());
            p.setNotes(ob["note"].toString());
            node.addProblem(p);
        }
    }

    return node;
}


