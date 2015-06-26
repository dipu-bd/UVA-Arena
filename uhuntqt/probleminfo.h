#ifndef PROBLEMINFO_H
#define PROBLEMINFO_H

#include <QtCore/QJsonArray>

#include "enums.h"

class ProblemInfo
{
public:
    ProblemInfo(const QJsonArray& data);

    // Load data from a json array
    void loadData(const QJsonArray& data);

    // 0. Problem ID
    int getProblemID() const { return vProblemID; }
    void setProblemID(int v) { vProblemID = v; }
    // 1. Problem Number
    int getProblemNumber() const { return vProblemNumber; }
    void setProblemNumber(int v) { vProblemNumber = v; }
    // 2. Problem Title
    QString getProblemTitle() const { return vProblemTitle; }
    void setProblemTitle(QString v) { vProblemTitle = v; }
    // 3. Number of Distinct Accepted User (DACU)
    int getDACU() const { return vDACU; }
    void setDACU(int v) { vDACU = v; }
    // 4. Best Runtime of an Accepted Submission
    int getBestRuntime() const { return vBestRuntime; }
    void setBestRuntime(int v);
    // 5. Best Memory used of an Accepted Submission
    int getBestMemory() const { return vBestMemory; }
    void setBestMemory(int v);
    // 6. Number of No Verdict Given (can be ignored)
    int getNoVerdictCount() const { return vNoVerdictCount; }
    void setNoVerdictCount(int v) { vNoVerdictCount = v; }
    // 7. Number of Submission Error
    int getSubmissionErrorCount() const { return vSubmissionErrorCount; }
    void setSubmissionErrorCount(int v) { vSubmissionErrorCount = v; }
    // 8. Number of Can't be Judged
    int getCantBeJudgeCount() const { return vCantBeJudgedCount; }
    void setCantBeJudgedCount(int v) { vCantBeJudgedCount = v; }
    // 9. Number of In Queue
    int getInQueueCount() const { return vInQueueCount; }
    void setInQueueCount(int v) { vInQueueCount = v; }
    // 10. Number of Compilation Error
    int getCompileErrorCount() const { return vCompileErrorCount; }
    void setCompileErrorCount(int v) { vCompileErrorCount = v; }
    // 11. Number of Restricted Function
    int getRestrictedFunctionCount() const { return vRestrictedFunctionCount; }
    void setRestrictedFunctionCount(int v) { vRestrictedFunctionCount = v; }
    // 12. Number of Runtime Error
    int getRuntimeErrorCount() const { return vRuntimeErrorCount; }
    void setRuntimeErrorCount(int v) { vRuntimeErrorCount = v; }
    // 13. Number of Output Limit Exceeded
    int getOutputLimitExceededCount() const { return vOutputLimitExceededCount; }
    void setOutputLimitExceededCount(int v) { vOutputLimitExceededCount = v; }
    // 14. Number of Time Limit Exceeded
    int getTimeLimitExceededCount() const { return vTimeLimitExceededCount; }
    void setTimeLimitExceededCount(int v) { vTimeLimitExceededCount = v; }
    // 15. Number of Memory Limit Exceeded
    int getMemoryLimitExceededCount() const { return vMemoryLimitExceededCount; }
    void setMemoryLimitExceededCount(int v) { vMemoryLimitExceededCount = v; }
    // 16. Number of Wrong Answer
    int getWrongAnswerCount() const { return vWrongAnswerCount; }
    void setWrongAnswerCount(int v) { vWrongAnswerCount = v; }
    // 17. Number of Presentation Error
    int getPresentationErrorCount() const { return vPresentationErrorCount; }
    void setPresentationErrorCount(int v) { vPresentationErrorCount = v; }
    // 18. Number of Accepted
    int getAcceptedCount() const { return vAcceptedCount; }
    void setAcceptedCount(int v) { vAcceptedCount = v; }
    // 19. Time Limit (milliseconds)
    int getRuntimeLimitCount() const { return vRuntimeLimitCount; }
    void setRuntimeLimitCount(int v) { vRuntimeLimitCount = v; }
    // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
    ProblemStatus getProblemStatus() const { return vProblemStatusCount; }
    void setProblemStatus(ProblemStatus v) { vProblemStatusCount = v; }
    void setProblemStatus(int v);

    // get volume number of the problem
    int getVolume() const { return vProblemNumber / 100; }
    // get the total submissions
    int getTotalSubmission() const;
    // True if default user solved this problem
    bool isSolved() const { return solved; }
    void setSolved(bool v) { solved = v; }
    // get the hardness level of the problem
    double getLevel() const { return level; }
    void setLevel(double v) { level = v; }
    // True if problem is marked as favorite
    bool isMarked() const { return marked; }
    void setMarked(bool v) { marked = v; }

private:
    // 0. Problem ID
    int vProblemID;
    // 1. Problem Number
    int vProblemNumber;
    // 2. Problem Title
    QString vProblemTitle;
    // 3. Number of Distinct Accepted User (DACU)
    int vDACU;
    // 4. Best Runtime of an Accepted Submission
    int vBestRuntime;
    // 5. Best Memory used of an Accepted Submission
    int vBestMemory;
    // 6. Number of No Verdict Given (can be ignored)
    int vNoVerdictCount;
    // 7. Number of Submission Error
    int vSubmissionErrorCount;
    // 8. Number of Can't be Judged
    int vCantBeJudgedCount;
    // 9. Number of In Queue
    int vInQueueCount;
    // 10. Number of Compilation Error
    int vCompileErrorCount;
    // 11. Number of Restricted Function
    int vRestrictedFunctionCount;
    // 12. Number of Runtime Error
    int vRuntimeErrorCount;
    // 13. Number of Output Limit Exceeded
    int vOutputLimitExceededCount;
    // 14. Number of Time Limit Exceeded
    int vTimeLimitExceededCount;
    // 15. Number of Memory Limit Exceeded
    int vMemoryLimitExceededCount;
    // 16. Number of Wrong Answer
    int vWrongAnswerCount;
    // 17. Number of Presentation Error
    int vPresentationErrorCount;
    // 18. Number of Accepted
    int vAcceptedCount;
    // 19. Time Limit (milliseconds)
    int vRuntimeLimitCount;
    // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
    ProblemStatus vProblemStatusCount;

    //set true if this problem is solved by default user
    bool solved;
    //set true if this problem is marked as favorite
    bool marked;
    //set the level of this problem (value between 1 and 10~14)
    double level;

};

#endif // PROBLEMINFO_H
