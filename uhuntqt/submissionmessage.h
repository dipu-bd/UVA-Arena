#ifndef SUBMISSIONMESSAGE_H
#define SUBMISSIONMESSAGE_H

#include <QtCore/QString>
#include <QtCore/QJsonObject>

#include "enums.h"

class SubmissionMessage
{    
public:
    SubmissionMessage();
    SubmissionMessage(const QJsonObject& data);

    void loadData(const QJsonObject& data);

    //sid: Submission ID
    int getSid() const { return _sid; }
    void setSid(int v) { _sid = v; }
    //uid: user id
    int getUid() const { return _uid; }
    void setUid(int v) { _uid = v; }
    //pid: Problem ID
    int getPid() const { return _pid; }
    void setPid(int v) { _pid = v; }
    //ver: Verdict ID
    Verdict getVer() const { return _ver; }
    void setVer(Verdict v) { _ver = v; }
    void setVer(int v);
    //lan: Language ID
    Language getLan() const { return _lan; }
    void setLan(Language v) { _lan = v; }
    void setLan(int v);
    //run : Runtime
    int getRun() const { return _run; }
    void setRun(int v);
    //mem: Memory taken
    int getMem() const { return _mem; }
    void setMem(int v);
    //rank: Submission Rank
    int getRank() const { return _rank; }
    void setRank(int v) { _rank = v; }
    //sbt: Submission Time (UNIX time stamp)
    int getSbt() const { return _sbt; }
    void setSbt(int v) { _sbt = v; }
    //name: full username
    QString Name() const { return _name; }
    void setName(QString v) { _name = v; }
    //uname: user name
    QString getUname() const { return _uname; }
    void setUname(QString v) { _uname = v; }

    //problem number
    int getPnum() const { return _pnum; }
    void setPnum(int v) { _pnum = v; }
    //problem title
    QString getPtitle() const { return _ptitle; }
    void setPtitle(QString v) { _ptitle = v; }

private:
    //sid: Submission ID
    int _sid;
    //uid: user id
    int _uid;
    //pid: Problem ID
    int _pid;
    //ver: Verdict ID
    Verdict _ver;
    //lan: Language ID
    Language _lan;
    //run : Runtime
    int _run;
    //mem: Memory taken
    int _mem;
    //rank: Submission Rank
    int _rank;
    //sbt: Submission Time (UNIX time stamp)
    int _sbt;
    //name: full username
    QString _name;
    //uname: user name
    QString _uname;

    //problem number
    int _pnum;
    //problem title
    QString _ptitle;
};

#endif // SUBMISSIONMESSAGE_H
