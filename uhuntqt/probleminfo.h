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
    int getPid() const { return _pid; }
    void setPid(int v) { _pid = v; }
    // 1. Problem Number
    int getPnum() const { return _pnum; }
    void setPnum(int v) { _pnum = v; }
    // 2. Problem Title
    QString getPtitle() const { return _ptitle; }
    void setPtitle(QString v) { _ptitle = v; }
    // 3. Number of Distinct Accepted User (DACU)
    int getDacu() const { return _dacu; }
    void setDacu(int v) { _dacu = v; }
    // 4. Best Runtime of an Accepted Submission
    int getRun() const { return _run; }
    void setRun(int v);
    // 5. Best Memory used of an Accepted Submission
    int getMem() const { return _mem; }
    void setMem(int v);
    // 6. Number of No Verdict Given (can be ignored)
    int getNver() const { return _nver; }
    void setNver(int v) { _nver = v; }
    // 7. Number of Submission Error
    int getSube() const { return _sube; }
    void setSube(int v) { _sube = v; }
    // 8. Number of Can't be Judged
    int getCbj() const { return _cbj; }
    void setCbj(int v) { _cbj = v; }
    // 9. Number of In Queue
    int getInq() const { return _inq; }
    void setInq(int v) { _inq = v; }
    // 10. Number of Compilation Error
    int getCe() const { return _ce; }
    void setCe(int v) { _ce = v; }
    // 11. Number of Restricted Function
    int getResf() const { return _resf; }
    void setResf(int v) { _resf = v; }
    // 12. Number of Runtime Error
    int getRe() const { return _re; }
    void setRe(int v) { _re = v; }
    // 13. Number of Output Limit Exceeded
    int getOle() const { return _ole; }
    void setOle(int v) { _ole = v; }
    // 14. Number of Time Limit Exceeded
    int getTle() const { return _tle; }
    void setTle(int v) { _tle = v; }
    // 15. Number of Memory Limit Exceeded
    int getMle() const { return _mle; }
    void setMle(int v) { _mle = v; }
    // 16. Number of Wrong Answer
    int getWa() const { return _wa; }
    void setWa(int v) { _wa = v; }
    // 17. Number of Presentation Error
    int getPe() const { return _pe; }
    void setPe(int v) { _pe = v; }
    // 18. Number of Accepted
    int getAc() const { return _ac; }
    void setAc(int v) { _ac = v; }
    // 19. Time Limit (milliseconds)
    int getRtl() const { return _rtl; }
    void setRtl(int v) { _rtl = v; }
    // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
    ProblemStatus getStat() const { return _stat; }
    void setStat(ProblemStatus v) { _stat = v; }
    void setStat(int v);

    // get volume number of the problem
    int getVolume() const { return _pnum / 100; }
    // get the total submissions
    int getTotal() const;
    // True if default user solved this problem
    bool isSolved() const { return solved; }
    void setSolved(bool v) { solved = v; }
    // get the hardness level of the problem
    double getLevel() const { return level; }
    void setLevel(double v) { level = v; }
    // True if problem is marked as favorite
    bool getMarked() const { return marked; }
    void setMarked(bool v) { marked = v; }

private:
    // 0. Problem ID
    int _pid;
    // 1. Problem Number
    int _pnum;
    // 2. Problem Title
    QString _ptitle;
    // 3. Number of Distinct Accepted User (DACU)
    int _dacu;
    // 4. Best Runtime of an Accepted Submission
    int _run;
    // 5. Best Memory used of an Accepted Submission
    int _mem;
    // 6. Number of No Verdict Given (can be ignored)
    int _nver;
    // 7. Number of Submission Error
    int _sube;
    // 8. Number of Can't be Judged
    int _cbj;
    // 9. Number of In Queue
    int _inq;
    // 10. Number of Compilation Error
    int _ce;
    // 11. Number of Restricted Function
    int _resf;
    // 12. Number of Runtime Error
    int _re;
    // 13. Number of Output Limit Exceeded
    int _ole;
    // 14. Number of Time Limit Exceeded
    int _tle;
    // 15. Number of Memory Limit Exceeded
    int _mle;
    // 16. Number of Wrong Answer
    int _wa;
    // 17. Number of Presentation Error
    int _pe;
    // 18. Number of Accepted
    int _ac;
    // 19. Time Limit (milliseconds)
    int _rtl;
    // 20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
    ProblemStatus _stat;

    //others
    bool solved;
    bool marked;
    double level;

};

#endif // PROBLEMINFO_H
