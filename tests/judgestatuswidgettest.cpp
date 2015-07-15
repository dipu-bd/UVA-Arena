#include <QApplication>
#include <iostream>
#include <QNetworkAccessManager>

#include "widgets/judgestatuswidget.h"
#include "uhunt/uhunt.h"

using namespace std;
using namespace uva;


int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    JudgeStatusWidget widget;

    Uhunt::ProblemMap problemMap;
    QList<JudgeStatus> judgeStatus;
    Uhunt api(std::make_shared<QNetworkAccessManager>());

    QObject::connect(&api, &Uhunt::problemListDownloaded, [&](Uhunt::ProblemMap pmap) {
        problemMap =  pmap;
        api.getJudgeStatus();
        cout << "Downloading judge status... " << endl;
    });

    QObject::connect(&api, &Uhunt::judgeStatusDownloaded, [&](QList<JudgeStatus> status) {
        judgeStatus = status;
        widget.setStatusData(std::make_shared<QList<JudgeStatus>>(judgeStatus),
                             std::make_shared<Uhunt::ProblemMap>(problemMap));
        cout << "All data downloaded." << endl;
    });

    widget.show();

    api.getProblemList();
    cout << "Downloading problem list... " << endl;

    return app.exec();
}
