#include "conversion.h"

using namespace uva;

QString Conversion::getVerdictName(Verdict verdict)
{
    switch(verdict)
    {
    case Verdict::SubError:
        return "Submission Error";

    case Verdict::CannotBeJudge:
        return "Can not be judged";

    case Verdict::CompileError:
        return "Compile Error";

    case Verdict::RestrictedFunction:
        return "Restricted Function";

    case Verdict::RuntimeError:
        return "Runtime Error";

    case Verdict::OutputLimit:
        return "Output Limit Exceeded";

    case Verdict::TimLimit:
        return "Time Limit Exceeded";

    case Verdict::MemoryLimit:
        return "Memory Limit Exceeded";

    case Verdict::WrongAnswer:
        return "Wrong Answer";

    case Verdict::PresentationError:
        return "Presentation Error";

    case Verdict::Accepted:
        return "Accepted";

    default:
        return "-In Queue-";
    }
}

QString Conversion::getLangaugeName(Language language)
{
    switch(language)
    {
    case Language::C:
        return "Ansi C";

    case Language::Java:
        return "Java";

    case Language::CPP:
        return "C++";

    case Language::Pascal:
        return "Pascal";

    case Language::CPP11:
        return "C++11";

    default:
        return "Other";
    }
}

QString Conversion::getRuntime(int runtime)
{
    double time = static_cast<double>(runtime) / 1000.0;
    return QString::number(time, 'f', 3) + "sec";
}

QString Conversion::getMemory(qint64 memory)
{
    int index = 0;
    double mem = static_cast<double>(memory);
    while(mem > 1024.0)
    {
        mem /= 1024.0;
        index++;
    }

    QString val = QString::number(mem, 'f', 2);
    switch(index)
    {
    case 0:
        return val + "B";
    case 1:
        return val + "KB";
    case 2:
        return val + "MB";
    case 3:
        return val + "GB";
    case 4:
        return val + "TB";
    default:
        return val + "ARE YOU MAD!!!";
    }
}

QString Conversion::getSubmissionTime(quint64 unixTime)
{
	QDateTime unix = QDateTime::fromTime_t(unixTime);
	QDateTime current = QDateTime::currentDateTime();	
    return getTimeSpan(current, unix) + " ago";
}

QString Conversion::getTimeSpan(QDateTime first, QDateTime second)
{    
    /*
        This function returns string like "2 years 5 days" or "1 hour 2 mins" or "5 secs".
    */

    QString out = "";
	bool space = false;

    qint64 daysTo = abs(first.daysTo(second));
    qint64 secsTo = abs(first.secsTo(second));

	if (daysTo >= 1)
	{
		qint64 year = (qint64)(daysTo / 365);
		qint64 month = (qint64)((daysTo - year * 365) / 30);
		qint64 day = (qint64)(daysTo - year * 365 - month * 30);

		if (year > 0)
		{
			space = true;
			out += QString::number(year);
			if (year > 1) out += " years";
			else out += " year";
		}
		if (month > 0)
		{
			if (space) out += " ";
			space = true;
			out += QString::number(month);
			if (month > 1) out += " months";
			else out += " month";
		}
		//add day when total day is less than 30
		if (daysTo < 30)
		{
			if (day > 0)
			{
				if (space) out += " ";
				space = true;
				out += QString::number(day);
				if (day > 1) out += " days";
				else out += " day";
			}
		}
	}
	else //add hours and minutes when total day is less than 1
	{
		qint64 hour = (qint64)(secsTo / 3600);
		qint64 minute = (qint64)((secsTo - hour * 3600) / 60);

		if (hour > 0)
		{
			if (space) out += " ";
			space = true;
			out += QString::number(hour);
			if (hour > 1) out += " hours";
			else out += " hour";
		}
		if (minute > 0)
		{
			if (space) out += " ";
			space = true;
			out += QString::number(minute);
			if (minute > 1) out += " mins";
			else out += " min";
		}
		//add seconds when total secs is less than 60
		if (secsTo < 60)
		{
			if (space) out += " ";
			space = true;
			out += QString::number(secsTo);
			if (secsTo > 1) out += " secs";
			else out += " sec";
		}
	}

	return out;
}
