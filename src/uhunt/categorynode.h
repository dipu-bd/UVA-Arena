#pragma once

#include <QMap>
#include <QJsonArray>
#include <QJsonObject>

#include "uvalib_global.h"
#include "problem.h"

namespace uva
{

    class UVA_EXPORT CategoryNode
    {
    public:
        CategoryNode();

        //get a new category node from jsonObject
        static CategoryNode fromJsonObject(const QJsonObject& data);

        //name
        QString getName() const { return mName; }
        void setName(QString v) { mName = v; }
        //note
        QString getNote() const { return mNote; }
        void setNote(QString v) { mNote = v; }

        //problems
        QList<Problem> getProblems() { return mProblems.values(); }
        QList<int> getProblemNumbers() { return mProblems.keys(); }
        void addProblem(Problem prob) { mProblems[prob.getNumber()] = prob; }

        //branches
        QList<CategoryNode> getCategorNodes() { return mBranches.values(); }
        QList<QString> getCategoryNames() { return mBranches.keys(); }
        void addBranch(CategoryNode node) { mBranches[node.getName()] = node; }

    private:
        //name
        QString mName;
        //note
        QString mNote;
        //problems
        QMap<int, Problem> mProblems;
        //branches
        QMap<QString, CategoryNode> mBranches;
    };

}
