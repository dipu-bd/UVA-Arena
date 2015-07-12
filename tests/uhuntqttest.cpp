#include <stdio.h>
#include <iostream>
#include <memory>
#include <QList>
#include <QNetworkAccessManager>

#include <QApplication>
#include <QFrame>
#include <QPushButton>
#include <QVBoxLayout>

#include "mainwindow.h"
#include "uhunt/uhunt.h"

using namespace std;
using namespace uva;

const QByteArray problemData = "[[40,104,\"this \\\"is\\\" [a] test\",3711,10,1000000000,0,306,0,0,2238,0,2205,5,3538,427,12052,578,6228,3000,2,0],[36,100,\"The 3n + 1 problem\",69204,7,1000000000,0,6669,0,0,102913,0,57933,146,53082,5209,242535,4512,174208,3000,1,0],[37,101,\"The Blocks Problem\",11806,0,1000000000,0,911,0,0,12366,0,19363,6,8576,200,20990,5738,17518,3000,1,0],[38,102,\"Ecological Bin Packing\",21625,0,1000000000,0,1948,0,0,11862,0,4418,6,2810,72,32599,640,33663,3000,1,0],[39,103,\"Stacking Boxes\",6369,0,1000000000,0,36,0,0,9101,0,4407,0,2869,0,11452,1351,9536,3000,2,0],[40,104,\"Arbitrage\",3711,10,1000000000,0,306,0,0,2238,0,2205,5,3538,427,12052,578,6228,3000,2,0]]";
const QByteArray judgeData = "[{\"id\":1433402181069, \"type\":\"lastsubs\", \"msg\":{\"sid\":15735294,\"uid\":159399,\"pid\":481,\"ver\":0,\"lan\":2,\"run\":0,\"mem\":0,\"rank\":-1,\"sbt\":1436337218,\"name\":\"Jim\",\"uname\":\"vjudge10\"}},{\"id\":1433402181070, \"type\":\"lastsubs\", \"msg\":{\"sid\":15735292,\"uid\":159395,\"pid\":1876,\"ver\":80,\"lan\":5,\"run\":0,\"mem\":0,\"rank\":-1,\"sbt\":1436337213,\"name\":\"Tom\",\"uname\":\"vjudge6\"}},{\"id\":1433402181071, \"type\":\"lastsubs\", \"msg\":{\"sid\":15735293,\"uid\":705089,\"pid\":318,\"ver\":70,\"lan\":1,\"run\":0,\"mem\":0,\"rank\":-1,\"sbt\":1436337215,\"name\":\"Safial Islam Ayon\",\"uname\":\"safialislam302\"}}]";
const QByteArray dummyUserInfo = "{\"name\":\"Sudipto Chandra\",\"uname\":\"dipu_sust\",\"subs\":[[11555091,96,10,0,1365074607,1,-1],[12112146,382,10,0,1374910525,3,-1],[12112174,382,10,0,1374910892,3,-1],[12113445,382,10,0,1374934476,3,-1],[12113467,382,10,0,1374934867,3,-1],[12115611,382,10,0,1374979816,3,-1],[12115648,382,10,0,1374980298,3,-1]]}";
const QList<QString> userNames = {"baodog", "dipu_sust", "felix_halim", "invalid-user", "", "-", "jgcoded"};
const QByteArray rankData = "[{\"rank\":1,\"old\":0,\"userid\":1133,\"name\":\"Josh Bao\",\"username\":\"baodog\",\"ac\":4528,\"nos\":16236,\"activity\":[0,0,24,37,198]},{\"rank\":2,\"old\":0,\"userid\":2223,\"name\":\"try\",\"username\":\"try\",\"ac\":4366,\"nos\":6338,\"activity\":[0,0,0,5,951]},{\"rank\":3,\"old\":0,\"userid\":111636,\"name\":\"Brian Fry\",\"username\":\"brianfry713\",\"ac\":4357,\"nos\":13299,\"activity\":[0,0,2,5,527]},{\"rank\":4,\"old\":0,\"userid\":19304,\"name\":\"Krzysztof Stencel\",\"username\":\"stencel\",\"ac\":4127,\"nos\":9146,\"activity\":[0,3,21,60,264]},{\"rank\":5,\"old\":0,\"userid\":6320,\"name\":\"Lee Wei\",\"username\":\"evandrix\",\"ac\":3511,\"nos\":7519,\"activity\":[0,0,6,28,200]},{\"rank\":6,\"old\":0,\"userid\":2448,\"name\":\"Neal Zane\",\"username\":\"nealzane\",\"ac\":3389,\"nos\":6707,\"activity\":[0,0,12,19,127]},{\"rank\":7,\"old\":0,\"userid\":2397,\"name\":\"dreamoon\",\"username\":\"dreamoon\",\"ac\":3309,\"nos\":10050,\"activity\":[0,0,2,4,107]},{\"rank\":8,\"old\":0,\"userid\":46705,\"name\":\"No English No AC, I'm out.\",\"username\":\"morris821028\",\"ac\":3042,\"nos\":9585,\"activity\":[1,1,16,123,837]},{\"rank\":9,\"old\":0,\"userid\":10845,\"name\":\"nehc\",\"username\":\"nehcdnr\",\"ac\":2481,\"nos\":9056,\"activity\":[2,7,31,90,317]},{\"rank\":10,\"old\":0,\"userid\":94974,\"name\":\"张翼德\",\"username\":\"vjudge2\",\"ac\":2471,\"nos\":63831,\"activity\":[15,43,222,397,970]}]";

void showProblemById(Problem problem)
{
    cout << "Downloaded problem: " << problem.getTitle().toStdString() << endl
        << "Number: " << problem.getNumber() << endl
        << "Id: " << problem.getID() << endl << endl;
}

void showProblemList(const Uhunt::ProblemMap &data)
{
    cout << "\nProblem List data: "
         << data.count() << " items" << endl;

    int cnt = 0;
    for (Problem info : data) {
        if(cnt++ > 20) break;
        cout << "level=" << info.getLevel()
             << "\t num=" << info.getNumber()
             << "\t dacu=" << info.getDACU()
             << "\t ac=" << info.getAcceptedCount()
             << "\t total=" << info.getTotalSubmission()
             << "\t title=" << info.getTitle().toStdString()
             << endl;
    }
    cout << "Problem list data shown" << endl;
}
void showJudgeStatus(const QList<JudgeStatus>& data)
{
    cout << "\nJudge status data: "
         << data.count() << " items" << endl;

    int cnt = 0;
    for(JudgeStatus stat : data) {
        if(cnt++ > 20) break;
        cout << "sid=" << stat.getSubmissionID()
             << "\t pnum=" << stat.getProblemNumber()
             << "\t ver="  << stat.getVerdict()
             << "\t lan=" << stat.getLanguage()
             << "\t user=" << stat.getFullName().toStdString()             
             << "\t ptitle=" << stat.getProblemTitle().toStdString()
             << endl;
    }
    cout << "Judge Status data shown" << endl;
}

void showUserID(QString uname, int id)
{
    cout << uname.toStdString() << " = " << id << endl;
}

void showUserInfo(const UserInfo& uinfo)
{
    cout << "\nUser Info:\n"
         << "UID = " << uinfo.getUserId()
         << "\nFull=" << uinfo.getFullName().toStdString()
         << "\nUser=" << uinfo.getUserName().toStdString()
         << "\nTotal=" << uinfo.getTotalSubmissionCount()
         << "\nAC=" << uinfo.getTotalSolvedCount()
         << "\nLast Sub Id=" << uinfo.getLastSubmissionID()<< "\n";
    cout << "Submissions: \n";

    int cnt = 0;
    for(int pnum : uinfo.getSubmissionList().keys()) {
        const UserSubmission& usub = uinfo.getSubmission(pnum);
        if(cnt++ > 20) break;
        cout << "sid=" << usub.getSubmissionID()
             << "\t pnum=" << usub.getProblemNumber()
             << "\t ver="  << usub.getVerdict()
             << "\t lan=" << usub.getLanguage()
             << "\t ptitle=" << usub.getProblemTitle().toStdString()
             << endl;
    }

    cout << "Userinfo data shown" << endl;
}

void showUserData(const QByteArray& data, int userId, int lastSub)
{
    if(lastSub > 0) {
        UserInfo ui = UserInfo::fromJson(userId, dummyUserInfo);
        ui.addUserSubmission(data);
        showUserInfo(ui);
    } else {
        UserInfo ui = UserInfo::fromJson(userId, data);
        showUserInfo(ui);
    }
}

void showRankData(const QList<RankInfo>& data)
{
    cout << "\nShowing RankInfo data: "
         << data.count() << " items" << endl;

    int cnt = 0;
    for(RankInfo info : data) {
        if(cnt++ > 20) break;
        cout << "rank=" << info.getRank()
             << "\t ac=" << info.getAcceptedCount()
             << "\t 3mon=" << info.getPast3month()
             << "\t 1year=" << info.getPast1year()
             << "\t name=" << info.getUserName().toStdString()
             << endl;
    }
    cout << "Judge Status data shown" << endl;
}

void showSubmissionData(const QList<SubmissionMessage>& data)
{
    cout << "\nSubmission Data: "
         << data.count() << " items" << endl;

    int cnt = 0;
    for(SubmissionMessage stat : data) {
        if(cnt++ > 20) break;
        cout << "sid=" << stat.getSubmissionID()
             << "\t pnum=" << stat.getProblemNumber()
             << "\t ver="  << stat.getVerdict()
             << "\t rank=" << stat.getRank()
             << "\t sbt=" << stat.getSubmissionTime()
             << "\t user=" << stat.getUserName().toStdString()
             << "\t ptitle=" << stat.getProblemTitle().toStdString()
             << endl;
    }
    cout << "Submission data shown" << endl;
}

void TestSampleData(Uhunt& api)
{
    //problem list test
    showProblemList(Uhunt::problemMapFromData(problemData));

    //judge status test
    showJudgeStatus(api.judgeStatusFromData(judgeData));

    //user info
    showUserInfo(UserInfo::fromJson(222248, dummyUserInfo));

    //rank info
    showRankData(api.rankListFromData(rankData));

    cout << "Sample data test ended\n\n";
}

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    //api data
    shared_ptr<QNetworkAccessManager> manager = make_shared<QNetworkAccessManager>();
    Uhunt api(manager);

    //connect signal and slots
    QObject::connect(&api, &Uhunt::problemByIdDownloaded, &showProblemById);
    QObject::connect(&api, &Uhunt::problemListDownloaded, &showProblemList);
    QObject::connect(&api, &Uhunt::judgeStatusDownloaded, &showJudgeStatus);
    QObject::connect(&api, &Uhunt::userIdDownloaded, &showUserID);
    QObject::connect(&api, &Uhunt::userInfoDataDownloaded, &showUserData);
    QObject::connect(&api, &Uhunt::rankByPositionDownloaded, &showRankData);
    QObject::connect(&api, &Uhunt::rankByUserDownloaded, &showRankData);
    QObject::connect(&api, &Uhunt::submissionOnProblemDownloaded,  &showSubmissionData);
    QObject::connect(&api, &Uhunt::ranklistOnProblemDownloaded,  &showSubmissionData);
    QObject::connect(&api, &Uhunt::userRankOnProblemDownloaded,  &showSubmissionData);
    QObject::connect(&api, &Uhunt::userSubmissionOnProblemDownloaded,  &showUserInfo);

    //sample test
    TestSampleData(api);

    //initialize frame
    QFrame frame;
    QVBoxLayout verticalLayout;
    QPushButton pushButton1;
    QPushButton pushButton2;
    QPushButton pushButton3;
    QPushButton pushButton4;
    QPushButton pushButton5;
    QPushButton pushButton6;
    QPushButton pushButton7;
    QPushButton pushButton8;
    QPushButton pushButton9;
    QPushButton pushButton10;
    QPushButton pushButton11;
    QPushButton pushButton12;
    verticalLayout.addWidget(&pushButton1);
    verticalLayout.addWidget(&pushButton2);
    verticalLayout.addWidget(&pushButton3);
    verticalLayout.addWidget(&pushButton4);
    verticalLayout.addWidget(&pushButton5);
    verticalLayout.addWidget(&pushButton6);
    verticalLayout.addWidget(&pushButton7);    
    verticalLayout.addWidget(&pushButton8);
    verticalLayout.addWidget(&pushButton9);
    verticalLayout.addWidget(&pushButton10);
    verticalLayout.addWidget(&pushButton11);
    verticalLayout.addWidget(&pushButton12);
    frame.setLayout(&verticalLayout);
    frame.setWindowTitle("Unit Test");

    pushButton1.setText("Click here to get userids");
    QObject::connect(&pushButton1, &QPushButton::clicked,
        [&]() {
            cout << "Userids: " << endl;
            for(QString x : userNames)
            {
                api.getUserID(x);
            }
        });

    pushButton2.setText("Click here to get judge status");
    QObject::connect(&pushButton2, &QPushButton::clicked, [&]() { api.getJudgeStatus(); });

    pushButton3.setText("Click here to get problem list");
    QObject::connect(&pushButton3, &QPushButton::clicked, [&]() { api.getProblemList(); } );

    pushButton4.setText("Click here to get user info");
    QObject::connect(&pushButton4, &QPushButton::clicked, [&]() { api.getUserInfoData(222248); });

    pushButton5.setText("Click here to update user info data");
    QObject::connect(&pushButton5, &QPushButton::clicked, [&]() { api.getUserInfoData(222248, 12115648); });

    pushButton6.setText("Click here to get ranklist of specific user");
    QObject::connect(&pushButton6, &QPushButton::clicked, [&]() { api.getRankByUser(222248); });

    pushButton7.setText("Click here to get ranklist starting from rank-1");
    QObject::connect(&pushButton7, &QPushButton::clicked, [&]() { api.getRankByPosition(1, 20); });

    pushButton8.setText("Click here to get a problem by it's id");
    QObject::connect(&pushButton8, &QPushButton::clicked, [&]() { api.getProblemById(36); });

    pushButton9.setText("Get submission on a problems");
    QObject::connect(&pushButton9, &QPushButton::clicked, [&]() { api.getSubmissionOnProblem(36, 1360753885, 1361358685); });

    pushButton10.setText("Get ranklist from a specific position on a problem");
    QObject::connect(&pushButton10, &QPushButton::clicked, [&]() { api.getRanklistOnProblem(36, 1, 25); });

    pushButton11.setText("Get ranklist of a specific user on a problem");
    QObject::connect(&pushButton11, &QPushButton::clicked, [&]() { api.getUserRankOnProblem(36, 222248); });

    pushButton12.setText("Get user submission on a problem");
    QObject::connect(&pushButton12, &QPushButton::clicked, [&]() { api.getUserSubmissionOnProblem(222248, 36); });


    frame.show();


    return app.exec();
}
