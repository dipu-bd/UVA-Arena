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
        return "C++ 11";

    default:
        return "Other";
    }
}

QString Conversion::getRuntime(int runtime)
{
    double time = static_cast<double>(runtime) / 1000.0;
    return QString::number(time, 'f', 3) + "sec";
}

QString Conversion::getMemory(int memory)
{
    int index = 0;
    double mem = static_cast<double>(memory);
    while(mem > 1024.0)
    {
        memory /= 1024.0;
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

QString Conversion::getSubmissionTime(int unixTime)
{
    return QString::number(unixTime);
}

