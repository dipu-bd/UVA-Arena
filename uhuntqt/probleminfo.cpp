#include "probleminfo.h"

ProblemInfo::ProblemInfo(const QJsonArray& data)
{
    loadData(data);
}

void ProblemInfo::loadData(const QJsonArray& data)
{
    // 0. Problem ID
    setPid(data[0].toInt());
    // 1. Problem Number
    setPnum(data[1].toInt());
    // 2. Problem Title
    setPtitle(data[2].toString());
    // 3. Number of Distinct Accepted User (DACU)
    setDacu(data[3].toInt());
    // 4. Best Runtime of an Accepted Submission
    setRun(data[4].toInt());
    // 5. Best Memory used of an Accepted Submission
    setMem(data[5].toInt());
    // 6. Number of No Verdict Given (can be ignored)
    setNver(data[6].toInt());
    // 7. Number of Submission Error
    setSube(data[7].toInt());
    // 8. Number of Can't be Judged
    setCbj(data[8].toInt());
    // 9. Number of In Queue
    setInq(data[9].toInt());
    // 10. Number of Compilation Error
    setCe(data[10].toInt());
    // 11. Number of Restricted Function
    setResf(data[11].toInt());
    // 12. Number of Runtime Error
    setRe(data[12].toInt());
    // 13. Number of Output Limit Exceeded
    setOle(data[13].toInt());
    // 14. Number of Time Limit Exceeded
    setTle(data[14].toInt());
    // 15. Number of Memory Limit Exceeded
    setMle(data[15].toInt());
    // 16. Number of Wrong Answer
    setWa(data[16].toInt());
    // 17. Number of Presentation Error
    setPe(data[17].toInt());
    // 18. Number of Accepted
    setAc(data[18].toInt());
    // 19. Time Limit (milliseconds)
    setRtl(data[19].toInt());
    // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
    setStat(data[20].toInt());

    marked = false;
    solved = false;
}

void ProblemInfo::setRun(int v)
{
    if (v < 0 || v >= 1000000000) v = 0;
    _run = v;
}

void ProblemInfo::setMem(int v)
{
    if (v < 0 || v >= 1000000000) v = 0;
    _mem = v;
}

void ProblemInfo::setStat(int v)
{
    switch (v)
    {
    case 0: _stat = ProblemStatus::Unavaible; break;
    case 1:_stat = ProblemStatus::Normal; break;
    case 2: _stat = ProblemStatus::Special_Judge;break;
    default: _stat = ProblemStatus::Normal;break;
    }
}

int ProblemInfo::getTotal() const
{
    return _ac + _wa + _cbj + _ce + _mle + _tle + _ole + _nver + _pe + _re + _resf + _sube;
}
