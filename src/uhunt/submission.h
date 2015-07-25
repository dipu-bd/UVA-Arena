#pragma once

#include <QtCore/QString>
#include <QtCore/QJsonArray>

#include "uvalib_global.h"

namespace uva
{

    struct UVA_EXPORT Submission
    {
        enum class Verdict
        {
            SUBMISSION_ERROR = 10,
            CANNOT_BE_JUDGED = 15,
            IN_QUEUE = 20,
            COMPILE_ERROR = 30,
            RESTRICTED_FUNCTION = 35,
            RUNTIME_ERROR = 40,
            OUTPUT_LIMIT = 45,
            TIME_LIMIT = 50,
            MEMORY_LIMIT = 60,
            WRONG_ANSWER = 70,
            PRESENTATION_ERROR = 80,
            ACCEPTED = 90,
        };

        enum class Language
        {
            OTHER = 0,
            C = 1,
            JAVA = 2,
            CPP = 3,
            PASCAL = 4,
            CPP11 = 5,
        };

        /*!
            \brief Gets a new UserSubmission from QJsonArray data.

            \param[in] data QJsonArray data containing information.

            \return A Submission struct with values filled in from data.
         */
        static Submission fromJsonArray(const QJsonArray& data);

        /*!
            \brief Gets a new UserSubmission from a QJsonObject.

            \param[in] data QJsonObject containing the Submission data.

            \return A Submission struct with values filled in from data.
        */
        static Submission fromJsonObject(const QJsonObject& data);

        //Submission ID
        int SubmissionID;
        //Problem ID
        int ProblemID;
        //Verdict ID
        Verdict SubmissionVerdict;
        //Runtime
        int Runtime;
        //Submission Time (UNIX time stamp)
        quint64 TimeSubmitted;
        //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
        Language SubmissionLanguage;
        //Submission Rank
        int Rank;
    };

}
