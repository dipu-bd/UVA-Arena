#ifndef USERSUBMISSION_H
#define USERSUBMISSION_H

#include <QtCore/QString>
#include <QtCore/QJsonArray>

#include "enums.h"

class UserSubmission
{    
public:
    UserSubmission();
    UserSubmission(const QJsonArray& data);

    bool isInQueue() const;
    bool isAccepted() const;
    void loadData(const QJsonArray& data);

    //Submission ID
    int getSid() const { return _sid; }
    void setSid(int v) { _sid = v; }
    //Problem ID
    int getPid() const { return _pid; }
    void setPid(int v) { _pid = v; }
    //Verdict ID
    Verdict getVer() const { return _ver; }
    void setVer(Verdict v) { _ver = v; }
    void setVer(int v);
    //Runtime
    int getRun() const { return _run; }
    void setRun(int v);
    //Submission Time (UNIX time stamp)
    int getSbt() const { return _sbt; }
    void setSbt(int v) { _sbt = v; }
    //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
    Language getLan() const { return _lan; }
    void setLan(Language v) { _lan = v; }
    void setLan(int v);
    //Submission Rank
    int getRank() const { return _rank; }
    void setRank(int v) { _rank = v; }

    //problem number
    int getPnum() const { return _pnum; }
    void setPnum(int v) { _pnum = v; }
    //user name
    QString getUname() const { return _uname; }
    void setUname(QString v) { _uname = v; }
    //full username
    QString Name() const { return _name; }
    void setName(QString v) { _name = v; }
    //problem title
    QString getPtitle() const { return _ptitle; }
    void setPtitle(QString v) { _ptitle = v; }

private:
    //Submission ID
    int _sid;
    //Problem ID
    int _pid;
    //Verdict ID
    Verdict _ver;
    //Runtime
    int _run;
    //Submission Time (UNIX time stamp)
    int _sbt;
    //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
    Language _lan;
    //Submission Rank
    int _rank;

    //problem number
    int _pnum;
    //user name
    QString _uname;
    //full username
    QString _name;
    //problem title
    QString _ptitle;
};

#endif // USERSUBMISSION_H
