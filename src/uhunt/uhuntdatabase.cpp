#include "uhuntdatabase.h"

using namespace uva;

bool UhuntDatabase::mAvailable = false;
QMap<int, Problem> UhuntDatabase::problemMap = QMap<int, Problem>();
QMap<int, int> UhuntDatabase::problemIdList = QMap<int, int>();

void UhuntDatabase::setProblemList(const QList<Problem>& problemList)
{
    //clear data
    problemMap.clear();
    problemIdList.clear();

    //build up list with data
    for(Problem p : problemList)
    {
        problemMap[p.getNumber()] = p;
        problemIdList[p.getID()] = p.getNumber();
    }

    //set avaiability
    mAvailable = true;
}

bool UhuntDatabase::isAvaiable()
{
    return mAvailable;
}

QList<Problem> UhuntDatabase::getProblemList()
{
    return problemMap.values();
}

int UhuntDatabase::getProblemNumber(int problemId)
{
    if(!problemIdList.contains(problemId))
        return 0;
    return problemIdList[problemId];
}

int UhuntDatabase::getProblemId(int problemNumber)
{
    if(!problemMap.contains(problemNumber))
        return 0;
    return problemMap[problemNumber].getID();
}

QString UhuntDatabase::getProblemTitleById(int problemId)
{
    return getProblemTitleByNumber(getProblemNumber(problemId));
}

QString UhuntDatabase::getProblemTitleByNumber(int problemNumber)
{
    if(!problemMap.contains(problemNumber))
        return 0;
    return problemMap[problemNumber].getTitle();
}

Problem UhuntDatabase::getProblemById(int problemId)
{
    return getProblemByNumber(getProblemNumber(problemId));
}

Problem UhuntDatabase::getProblemByNumber(int problemNumber)
{
    //TODO: decide what to return on invalid problemNumber
    if(!problemMap.contains(problemNumber))
        return *(new Problem());
    return problemMap[problemNumber];
}

