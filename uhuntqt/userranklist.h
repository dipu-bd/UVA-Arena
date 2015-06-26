#ifndef USERRANKLIST_H
#define USERRANKLIST_H

#include <QtCore/QString>
#include <QtCore/QJsonArray>
#include <QtCore/QJsonObject>

#include "enums.h"

class UserRanklist
{
public:
    UserRanklist();
    UserRanklist(const QJsonObject& data);

    //load data from json object
    void loadData(const QJsonObject& data);

    //rank : The rank of the user
    int getRank() { return _rank; }
    void setRank(int v) { _rank = v; }
    //old : Non zero if the user is an old UVa user that hasn't migrate
    bool getOld() { return _old; }
    void setOld(bool v) { _old = v; }
    //name : The name of the user
    QString getName() { return _name; }
    void setName(QString v) { _name = v; }
    //username : The username of the user
    QString getUsername() { return _username; }
    void setUsername(QString v) { _username = v; }
    //ac : The number of accepted problems
    int getAc() { return _ac; }
    void setAc(int v) { _ac = v; }
    //nos : The number of submissions of the user
    int getNos() { return _nos; }
    void setNos(int v) { _nos = v; }

    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    void setActivity(const QJsonArray& data);
    //past 2 days activity
    int get2day() { return _2day; }
    void set2day(int v) { _2day = v; }
    //past 7 days activity
    int get7day() { return _7day; }
    void set7day(int v) { _7day = v; }
    //past 31 days activity
    int get31day() { return _31day; }
    void set31day(int v) { _31day = v; }
    //past 3 months ctivity
    int get3month() { return _3month; }
    void set3month(int v) { _3month = v; }
    //past 1 years activity
    int get1year() { return _1year; }
    void set1year(int v) { _1year = v; }

private:
    //rank : The rank of the user
    int _rank;
    //old : Non zero if the user is an old UVa user that hasn't migrate
    bool _old;
    //name : The name of the user
    QString _name;
    //username : The username of the user
    QString _username;
    //ac : The number of accepted problems
    int _ac;
    //nos : The number of submissions of the user
    int _nos;

    //activity : The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year.
    //past 2 days activity
    int _2day;
    //past 7 days activity
    int _7day;
    //past 31 days activity
    int _31day;
    //past 3 months ctivity
    int _3month;
    //past 1 years activity
    int _1year;
};

#endif // USERRANKLIST_H
