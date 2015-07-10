#include "problem.h"

using namespace uva;

Problem::Problem()
{
}

Problem Problem::fromJson(const QByteArray& json)
{
    QJsonDocument document = QJsonDocument::fromJson(json);

    if (document.isObject())
        return fromJsonObject(document.object());
    else if (document.isArray())
        return fromJsonArray(document.array());

    return Problem();
}

Problem Problem::fromJsonObject(const QJsonObject& object)
{
    Problem problem;

    // 0. Problem ID
    problem.setID(object["pid"].toInt());
    // 1.  Number
    problem.setNumber(object["num"].toInt());
    // 2.  Title
    problem.setTitle(object["title"].toString());
    // 3. Number of Distinct Accepted User (DACU)
    problem.setDACU(object["dacu"].toInt());
    // 4. Best Runtime of an Accepted Submission
    problem.setBestRuntime(object["mrun"].toInt());
    // 5. Best Memory used of an Accepted Submission
    problem.setBestMemory(object["mmem"].toInt());
    // 6. Number of No Verdict Given (can be ignored)
    problem.setNoVerdictCount(object["nover"].toInt());
    // 7. Number of Submission Error
    problem.setSubmissionErrorCount(object["sube"].toInt());
    // 8. Number of Can't be Judged
    problem.setCantBeJudgedCount(object["noj"].toInt());
    // 9. Number of In Queue
    problem.setInQueueCount(object["inq"].toInt());
    // 10. Number of Compilation Error
    problem.setCompileErrorCount(object["ce"].toInt());
    // 11. Number of Restricted Function
    problem.setRestrictedFunctionCount(object["rf"].toInt());
    // 12. Number of Runtime Error
    problem.setRuntimeErrorCount(object["re"].toInt());
    // 13. Number of Output Limit Exceeded
    problem.setOutputLimitExceededCount(object["ole"].toInt());
    // 14. Number of Time Limit Exceeded
    problem.setTimeLimitExceededCount(object["tle"].toInt());
    // 15. Number of Memory Limit Exceeded
    problem.setMemoryLimitExceededCount(object["mle"].toInt());
    // 16. Number of Wrong Answer
    problem.setWrongAnswerCount(object["wa"].toInt());
    // 17. Number of Presentation Error
    problem.setPresentationErrorCount(object["pe"].toInt());
    // 18. Number of Accepted solutions
    problem.setAcceptedCount(object["ac"].toInt());
    // 19. Time Limit (milliseconds)
    problem.setRuntimeLimitCount(object["rtl"].toInt());
    // 20.  Status (0 = unavailable, 1 = normal, 2 = special judge)
    problem.setStatus(object["status"].toInt());

    problem.setSolved(false);
    problem.setMarked(false);

    problem.calculateLevel();

    return problem;
}

Problem Problem::fromJsonArray(const QJsonArray& arr)
{
    Problem problem;

    // 0.  ID
    problem.setID(arr[0].toInt());
    // 1.  Number
    problem.setNumber(arr[1].toInt());
    // 2.  Title
    problem.setTitle(arr[2].toString());
    // 3. Number of Distinct Accepted User (DACU)
    problem.setDACU(arr[3].toInt());
    // 4. Best Runtime of an Accepted Submission
    problem.setBestRuntime(arr[4].toInt());
    // 5. Best Memory used of an Accepted Submission
    problem.setBestMemory(arr[5].toInt());
    // 6. Number of No Verdict Given (can be ignored)
    problem.setNoVerdictCount(arr[6].toInt());
    // 7. Number of Submission Error
    problem.setSubmissionErrorCount(arr[7].toInt());
    // 8. Number of Can't be Judged
    problem.setCantBeJudgedCount(arr[8].toInt());
    // 9. Number of In Queue
    problem.setInQueueCount(arr[9].toInt());
    // 10. Number of Compilation Error
    problem.setCompileErrorCount(arr[10].toInt());
    // 11. Number of Restricted Function
    problem.setRestrictedFunctionCount(arr[11].toInt());
    // 12. Number of Runtime Error
    problem.setRuntimeErrorCount(arr[12].toInt());
    // 13. Number of Output Limit Exceeded
    problem.setOutputLimitExceededCount(arr[13].toInt());
    // 14. Number of Time Limit Exceeded
    problem.setTimeLimitExceededCount(arr[14].toInt());
    // 15. Number of Memory Limit Exceeded
    problem.setMemoryLimitExceededCount(arr[15].toInt());
    // 16. Number of Wrong Answer
    problem.setWrongAnswerCount(arr[16].toInt());
    // 17. Number of Presentation Error
    problem.setPresentationErrorCount(arr[17].toInt());
    // 18. Number of Accepted
    problem.setAcceptedCount(arr[18].toInt());
    // 19. Time Limit (milliseconds)
    problem.setRuntimeLimitCount(arr[19].toInt());
    // 20.  Status (0 = unavailable, 1 = normal, 2 = special judge)
    problem.setStatus(arr[20].toInt());

    problem.setMarked(false);
    problem.setMarked(false);

    problem.calculateLevel();

    return problem;
}

void Problem::calculateLevel()
{
    /*
       Level will vary between 1 to 10.
       Level increases with difficulty.
       Level will be calculated by taking log of DACU.
     */
    const double MAX_LEVEL = 9.0;

    double level;
    if(mDACU <= 0) //no one solved this
    {
        level = MAX_LEVEL;
    }
    else
    {
        level = MAX_LEVEL - std::min(MAX_LEVEL, floor(log(mDACU)));
    }

    level = 1.0 + level;
    mLevel = int(level);
}

void Problem::setBestRuntime(int v)
{
    if (v < 0 || v >= 1000000000) v = 0;
    mBestRuntime = v;
}

void Problem::setBestMemory(int v)
{
    if (v < 0 || v >= 1000000000) v = 0;
    mBestMemory = v;
}

void Problem::setStatus(int v)
{
    if (v < 0 || v >= ProblemStatus::ProblemStatusEnumCount)
        mStatus = ProblemStatus::Unavailable;

    mStatus = (ProblemStatus)v;
}

int Problem::getTotalSubmission() const
{
    return mAcceptedCount + mWrongAnswerCount + mCantBeJudgedCount + mCompileErrorCount + mMemoryLimitExceededCount + mTimeLimitExceededCount + mOutputLimitExceededCount + mNoVerdictCount + mPresentationErrorCount + mRuntimeErrorCount + mRestrictedFunctionCount + mSubmissionErrorCount;
}
