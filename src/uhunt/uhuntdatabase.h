#pragma once

#include <QMap>
#include <QList>
#include <QByteArray>

#include "problem.h"

namespace uva
{

    class UVA_EXPORT UhuntDatabase
    {
    public:
        static void setProblemList(const QList<Problem>& problemList);
        static int getProblemNumber(int problemId);
        static int getProblemId(int problemNumber);
        static QString getProblemTitleById(int problemId);
        static QString getProblemTitleByNumber(int problemNumber);
        static Problem& getProblemById(int problemId);
        static Problem& getProblemByNumber(int problemNumber);

        static QList<Problem> getProblemList();

    private:
        //map problem number to problem
        static QMap<int, Problem> problemMap;
        //problem id to problem number
        static QMap<int, int> problemIdList;
    };
}
