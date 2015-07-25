#include "problem.h"

using namespace uva;

Problem Problem::fromJson(const QByteArray& json)
{
    QJsonDocument document = QJsonDocument::fromJson(json);

    if (document.isObject())
        return fromJsonObject(document.object());
    else if (document.isArray())
        return fromJsonArray(document.array());

    return Problem();
}

Problem Problem::fromJsonObject(const QJsonObject& jsonObject)
{
    Problem problem;

    // 0. Problem ID
    problem.ProblemID = jsonObject["pid"].toInt();
    // 1.  Number
    problem.ProblemNumber = jsonObject["num"].toInt();
    // 2.  Title
    problem.ProblemTitle = jsonObject["title"].toString();
    // 3. Number of Distinct Accepted User (DACU)
    problem.DACU = jsonObject["dacu"].toInt();
    // 4. Best Runtime of an Accepted Submission
    problem.BestRuntime = jsonObject["mrun"].toInt();
    // 5. Best Memory used of an Accepted Submission
    problem.BestRuntime = jsonObject["mmem"].toInt();
    // 6. Number of No Verdict Given (can be ignored)
    problem.NoVerdictCount = jsonObject["nover"].toInt();
    // 7. Number of Submission Error
    problem.SubmissionErrorCount = jsonObject["sube"].toInt();
    // 8. Number of Can't be Judged
    problem.CantBeJudgedCount = jsonObject["noj"].toInt();
    // 9. Number of In Queue
    problem.InQueueCount = jsonObject["inq"].toInt();
    // 10. Number of Compilation Error
    problem.CompileErrorCount = jsonObject["ce"].toInt();
    // 11. Number of Restricted Function
    problem.RestrictedFunctionCount = jsonObject["rf"].toInt();
    // 12. Number of Runtime Error
    problem.RuntimeErrorCount = jsonObject["re"].toInt();
    // 13. Number of Output Limit Exceeded
    problem.OutputLimitExceededCount = jsonObject["ole"].toInt();
    // 14. Number of Time Limit Exceeded
    problem.TimeLimitExceededCount = jsonObject["tle"].toInt();
    // 15. Number of Memory Limit Exceeded
    problem.MemoryLimitExceededCount = jsonObject["mle"].toInt();
    // 16. Number of Wrong Answer
    problem.WrongAnswerCount = jsonObject["wa"].toInt();
    // 17. Number of Presentation Error
    problem.PresentationErrorCount = jsonObject["pe"].toInt();
    // 18. Number of Accepted solutions
    problem.AcceptedCount = jsonObject["ac"].toInt();
    // 19. Time Limit (milliseconds)
    problem.RuntimeLimit = jsonObject["rtl"].toInt();
    // 20.  Status (0 = unavailable, 1 = normal, 2 = special judge)
    problem.Status = (Problem::ProblemStatus)jsonObject["status"].toInt();

    return problem;
}

Problem Problem::fromJsonArray(const QJsonArray& jsonArray)
{
    Problem problem;

    // 0. Problem ID
    problem.ProblemID = jsonArray[0].toInt();
    // 1.  Number
    problem.ProblemNumber = jsonArray[1].toInt();
    // 2.  Title
    problem.ProblemTitle = jsonArray[2].toString();
    // 3. Number of Distinct Accepted User (DACU)
    problem.DACU = jsonArray[3].toInt();
    // 4. Best Runtime of an Accepted Submission
    problem.BestRuntime = jsonArray[4].toInt();
    // 5. Best Memory used of an Accepted Submission
    problem.BestMemory = jsonArray[5].toInt();
    // 6. Number of No Verdict Given (can be ignored)
    problem.NoVerdictCount = jsonArray[6].toInt();
    // 7. Number of Submission Error
    problem.SubmissionErrorCount = jsonArray[7].toInt();
    // 8. Number of Can't be Judged
    problem.CantBeJudgedCount = jsonArray[8].toInt();
    // 9. Number of In Queue
    problem.InQueueCount = jsonArray[9].toInt();
    // 10. Number of Compilation Error
    problem.CompileErrorCount = jsonArray[10].toInt();
    // 11. Number of Restricted Function
    problem.RestrictedFunctionCount = jsonArray[11].toInt();
    // 12. Number of Runtime Error
    problem.RuntimeErrorCount = jsonArray[12].toInt();
    // 13. Number of Output Limit Exceeded
    problem.OutputLimitExceededCount = jsonArray[13].toInt();
    // 14. Number of Time Limit Exceeded
    problem.TimeLimitExceededCount = jsonArray[14].toInt();
    // 15. Number of Memory Limit Exceeded
    problem.MemoryLimitExceededCount = jsonArray[15].toInt();
    // 16. Number of Wrong Answer
    problem.WrongAnswerCount = jsonArray[16].toInt();
    // 17. Number of Presentation Error
    problem.PresentationErrorCount = jsonArray[17].toInt();
    // 18. Number of Accepted solutions
    problem.AcceptedCount = jsonArray[18].toInt();
    // 19. Time Limit (milliseconds)
    problem.RuntimeLimit = jsonArray[19].toInt();
    // 20.  Status (0 = unavailable, 1 = normal, 2 = special judge)
    problem.Status = (Problem::ProblemStatus)jsonArray[20].toInt();

    return problem;
}
