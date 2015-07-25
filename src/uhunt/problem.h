#pragma once

#include <QList>
#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonArray>

#include "uvalib_global.h"

namespace uva
{
    struct UVA_EXPORT Problem
    {
        enum class ProblemStatus
        {
            UNAVAILABLE = 0,
            NORMAL = 1,
            SPECIAL_JUDGE = 2,
        };

        // Use the ProblemID field as the key for data structures
        typedef int Key;

        /*!
            \brief Creates a Problem struct from utf8 formatted json data

            \param[in] json The json data to read

            \return An instance of problem based off the json data
        */
        static Problem fromJson(const QByteArray& json);

        /*!
            \brief Creates a Problem from a QJsonObject.

            \param[in] jsonObject QJsonObject containing the Problem data.

            \return A Problem struct with values filled in from jsonObject.
        */
        static Problem fromJsonObject(const QJsonObject& jsonObject);

        /*!
            \brief Creates a Problem from a QJsonArray.

            \param[in] jsonArray QJsonArray containing the Problem data.

            \return A Problem struct with values filled in from jsonArray.
        */
        static Problem fromJsonArray(const QJsonArray& jsonArray);

        // 0. Problem ID
        Key ProblemID;
        // 1. Problem Number
        int ProblemNumber;
        // 2. Problem Title
        QString ProblemTitle;
        // 3. Number of Distinct Accepted User (DACU)
        int DACU;
        // 4. Best Runtime of an Accepted Submission
        int BestRuntime;
        // 5. Best Memory used of an Accepted Submission
        int BestMemory;
        // 6. Number of No Verdict Given (can be ignored)
        int NoVerdictCount;
        // 7. Number of Submission Error
        int SubmissionErrorCount;
        // 8. Number of Can't be Judged
        int CantBeJudgedCount;
        // 9. Number of In Queue
        int InQueueCount;
        // 10. Number of Compilation Error
        int CompileErrorCount;
        // 11. Number of Restricted Function
        int RestrictedFunctionCount;
        // 12. Number of Runtime Error
        int RuntimeErrorCount;
        // 13. Number of Output Limit Exceeded
        int OutputLimitExceededCount;
        // 14. Number of Time Limit Exceeded
        int TimeLimitExceededCount;
        // 15. Number of Memory Limit Exceeded
        int MemoryLimitExceededCount;
        // 16. Number of Wrong Answer
        int WrongAnswerCount;
        // 17. Number of Presentation Error
        int PresentationErrorCount;
        // 18. Number of Accepted
        int AcceptedCount;
        // 19. Time Limit (milliseconds)
        int RuntimeLimit;
        // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
        ProblemStatus Status;
    };

}
