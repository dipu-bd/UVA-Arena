#include "probleminfo.h"

ProblemInfo::ProblemInfo(const QJsonArray& data)
{
    loadData(data);
}

void ProblemInfo::loadData(const QJsonArray& data)
{
    // 0. Problem ID
    setProblemID(data[0].toInt());
    // 1. Problem Number
    setProblemNumber(data[1].toInt());
    // 2. Problem Title
    setProblemTitle(data[2].toString());
    // 3. Number of Distinct Accepted User (DACU)
    setDACU(data[3].toInt());
    // 4. Best Runtime of an Accepted Submission
    setBestRuntime(data[4].toInt());
    // 5. Best Memory used of an Accepted Submission
    setBestMemory(data[5].toInt());
    // 6. Number of No Verdict Given (can be ignored)
    setNoVerdictCount(data[6].toInt());
    // 7. Number of Submission Error
    setSubmissionErrorCount(data[7].toInt());
    // 8. Number of Can't be Judged
    setCantBeJudgedCount(data[8].toInt());
    // 9. Number of In Queue
    setInQueueCount(data[9].toInt());
    // 10. Number of Compilation Error
    setCompileErrorCount(data[10].toInt());
    // 11. Number of Restricted Function
    setRestrictedFunctionCount(data[11].toInt());
    // 12. Number of Runtime Error
    setRuntimeErrorCount(data[12].toInt());
    // 13. Number of Output Limit Exceeded
    setOutputLimitExceededCount(data[13].toInt());
    // 14. Number of Time Limit Exceeded
    setTimeLimitExceededCount(data[14].toInt());
    // 15. Number of Memory Limit Exceeded
    setMemoryLimitExceededCount(data[15].toInt());
    // 16. Number of Wrong Answer
    setWrongAnswerCount(data[16].toInt());
    // 17. Number of Presentation Error
    setPresentationErrorCount(data[17].toInt());
    // 18. Number of Accepted
    setAcceptedCount(data[18].toInt());
    // 19. Time Limit (milliseconds)
    setRuntimeLimitCount(data[19].toInt());
    // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
    setProblemStatus(data[20].toInt());

    mMarked = false;
    mSolved = false;
}

void ProblemInfo::setBestRuntime(int v)
{
    if (v < 0 || v >= 1000000000) v = 0;
    mBestRuntime = v;
}

void ProblemInfo::setBestMemory(int v)
{
    if (v < 0 || v >= 1000000000) v = 0;
    mBestMemory = v;
}

void ProblemInfo::setProblemStatus(int v)
{
    switch (v)
    {
    case 0: mProblemStatusCount = ProblemStatus::Unavaible; break;
    case 1: mProblemStatusCount = ProblemStatus::Normal; break;
    case 2: mProblemStatusCount = ProblemStatus::Special_Judge;break;
    default: mProblemStatusCount = ProblemStatus::Normal;break;
    }
}

int ProblemInfo::getTotalSubmission() const
{
    return mAcceptedCount + mWrongAnswerCount + mCantBeJudgedCount + mCompileErrorCount + mMemoryLimitExceededCount + mTimeLimitExceededCount + mOutputLimitExceededCount + mNoVerdictCount + mPresentationErrorCount + mRuntimeErrorCount + mRestrictedFunctionCount + mSubmissionErrorCount;
}
