#include <stdio.h>
#include <iostream>
#include <memory>
#include <QApplication>
#include <QPushButton>
#include <QNetworkAccessManager>
#include <QList>

#include "uhunt/uhuntqt.h"

using namespace std;

const QByteArray problemData = "[[40,104,\"this \\\"is\\\" [a] test\",3711,10,1000000000,0,306,0,0,2238,0,2205,5,3538,427,12052,578,6228,3000,2,0],[36,100,\"The 3n + 1 problem\",69204,7,1000000000,0,6669,0,0,102913,0,57933,146,53082,5209,242535,4512,174208,3000,1,0],[37,101,\"The Blocks Problem\",11806,0,1000000000,0,911,0,0,12366,0,19363,6,8576,200,20990,5738,17518,3000,1,0],[38,102,\"Ecological Bin Packing\",21625,0,1000000000,0,1948,0,0,11862,0,4418,6,2810,72,32599,640,33663,3000,1,0],[39,103,\"Stacking Boxes\",6369,0,1000000000,0,36,0,0,9101,0,4407,0,2869,0,11452,1351,9536,3000,2,0],[40,104,\"Arbitrage\",3711,10,1000000000,0,306,0,0,2238,0,2205,5,3538,427,12052,578,6228,3000,2,0]]";
const QByteArray judgeData = "[{\"id\":1433402181069, \"type\":\"lastsubs\", \"msg\":{\"sid\":15735294,\"uid\":159399,\"pid\":481,\"ver\":0,\"lan\":2,\"run\":0,\"mem\":0,\"rank\":-1,\"sbt\":1436337218,\"name\":\"Jim\",\"uname\":\"vjudge10\"}},{\"id\":1433402181070, \"type\":\"lastsubs\", \"msg\":{\"sid\":15735292,\"uid\":159395,\"pid\":1876,\"ver\":80,\"lan\":5,\"run\":0,\"mem\":0,\"rank\":-1,\"sbt\":1436337213,\"name\":\"Tom\",\"uname\":\"vjudge6\"}},{\"id\":1433402181071, \"type\":\"lastsubs\", \"msg\":{\"sid\":15735293,\"uid\":705089,\"pid\":318,\"ver\":70,\"lan\":1,\"run\":0,\"mem\":0,\"rank\":-1,\"sbt\":1436337215,\"name\":\"Safial Islam Ayon\",\"uname\":\"safialislam302\"}}]";
const QList<QString> userNames = {"baodog", "dipu_sust", "felix_halim", "invalid-user", "", "-", "jgcoded"};

void showProblemList(QList<uva::ProblemInfo> data)
{
    cout << "Problem List data: "
         << data.count() << " items" << endl;
    for (uva::ProblemInfo info : data)
    {
        cout << "level=" << info.getLevel()
             << "\t num=" << info.getProblemNumber()
             << "\t dacu=" << info.getDACU()
             << "\t ac=" << info.getAcceptedCount()
             << "\t total=" << info.getTotalSubmission()
             << "\t title=" << info.getProblemTitle().toStdString()
             << endl;
    }
    cout << endl;
}
void showJudgeStatus(QList<uva::JudgeStatus> data)
{
    cout << "Judge status data: "
         << data.count() << " items" << endl;
    for(uva::JudgeStatus stat : data)
    {
        cout << "sid=" << stat.getSubmissionID()
             << "\t pid=" << stat.getProblemID()
             << "\t ver="  << stat.getVerdict()
             << "\t lan=" << stat.getLanguage()
             << "\t sbt=" << stat.getSubmissionTime()
             << "\t user=" << stat.getFullName().toStdString()
             << endl;
    }
    cout << endl;
}

void showUserID(QString uname, int id)
{
    cout << uname.toStdString() << " = " << id << endl;
}

void TestSampleData(uva::Uhuntqt& api)
{
    //problem list test
    showProblemList(api.problemListFromData(problemData));

    //judge status test
    showJudgeStatus(api.judgeStatusFromData(judgeData));

    //testing username to userid
    cout << "Userids: " << endl;
    for(QString x : userNames)
    {
        api.getUserID(x);
    }
}

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    shared_ptr<QNetworkAccessManager> manager = make_shared<QNetworkAccessManager>();
    uva::Uhuntqt api(manager);

    QObject::connect(&api, &uva::Uhuntqt::problemListDownloaded, &showProblemList);
    QObject::connect(&api, &uva::Uhuntqt::judgeStatusDownloaded, &showJudgeStatus);
    QObject::connect(&api, &uva::Uhuntqt::userIdDownloaded, &showUserID);

    //sample test
    TestSampleData(api);

/*
    cout << "Downloading..." << endl;
    freopen("test.txt", "w", stdout);
    //download judge status
    api.getJudgeStatus();
    //download problem list
    api.getProblemList();
*/

/*
    //problem list button
    QPushButton getProblemListButton;
    getProblemListButton.setText("Click me to get the problem list");
    QObject::connect(&getProblemListButton, &QPushButton::clicked,
                     &api, &uva::Uhuntqt::getProblemList);
    getProblemListButton.show();
*/

    //judge statis button
    QPushButton getJudgeStatusButton;
    getJudgeStatusButton.setText("Click me to get the judge status");
    QObject::connect(&getJudgeStatusButton, &QPushButton::clicked,
                     &api, &uva::Uhuntqt::getJudgeStatus);
    getJudgeStatusButton.show();


    return app.exec();
}
