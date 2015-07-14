#pragma once

#include "uhunt/enums.h"
#include "uvalib_global.h"
#include <QVariant>

namespace uva
{

    /**
        \brief Class for converting one type of data into another
        */
    class UVA_EXPORT Conversion
    {
    public:

        static QString getVerdictName(Verdict verdict);
        static QString getLangaugeName(Language language);
        static QString getRuntime(int runtime);
        static QString getMemory(int memory);
        static QString getSubmissionTime(int unixTime);

    };
}
