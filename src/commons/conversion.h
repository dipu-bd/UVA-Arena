#pragma once

#include "uvalib_global.h"
#include <QVariant>
#include <QDateTime>
#include "uhunt/submission.h"

namespace uva
{

    /**
        \brief Class for converting one type of data into another
        */
    class UVA_EXPORT Conversion
    {
    public:

		/*!
			\brief Get verdict name from Verdict.
			\param[in] verdict Enum value of verdict.
		 */
        static QString getVerdictName(Submission::Verdict verdict);

		/*!
			\brief Get language name from language.
			\param[in] language Enum value of language.
		*/
        static QString getLanguageName(Submission::Language language);

		/*! 
			\brief Get runtime converted into second upto three decimal points.
			\param[in] runtime Runtime in milliseconds.
		*/
        static QString getRuntime(int runtime);

		/*!
			\brief Get memory in B, KB, MB, or GB format.
			\param[in] memory Memory in bytes.
		*/
        static QString getMemory(qint64 memory);

		/*!
			\brief Converts given time and returns span from current time.
			\param[in] unixTime Time in unix timestamp.
		*/
        static QString getSubmissionTime(quint64 unixTime);

		/*!
			\brief Gets the time difference between two date and time.
			\param[in] first First DateTime
			\param[in] second Second DateTime
		*/
		static QString getTimeSpan(QDateTime first, QDateTime second);
    };
}
