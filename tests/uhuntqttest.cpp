#include <iostream>
#include <memory>
#include <QApplication>
#include <QNetworkAccessManager>
#include <QPushButton>
#include "uhunt/uhuntqt.h"

using namespace std;

const QByteArray sampleData = "[[36,100,\"The 3n + 1 problem\",69204,7,1000000000,0,6669,0,0,102913,0,57933,146,53082,5209,242535,4512,174208,3000,1,0],[37,101,\"The Blocks Problem\",11806,0,1000000000,0,911,0,0,12366,0,19363,6,8576,200,20990,5738,17518,3000,1,0],[38,102,\"Ecological Bin Packing\",21625,0,1000000000,0,1948,0,0,11862,0,4418,6,2810,72,32599,640,33663,3000,1,0],[39,103,\"Stacking Boxes\",6369,0,1000000000,0,36,0,0,9101,0,4407,0,2869,0,11452,1351,9536,3000,2,0],[40,104,\"Arbitrage\",3711,10,1000000000,0,306,0,0,2238,0,2205,5,3538,427,12052,578,6228,3000,2,0]]";

void TestSampleData(uva::Uhuntqt& api)
{
    cout << "Sample data: " << endl;
    for (uva::ProblemInfo info : api.problemListFromData(sampleData))
    {
        cout << info.getProblemTitle().toStdString() << endl;
    }
    cout << endl;
}

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    shared_ptr<QNetworkAccessManager> manager = make_shared<QNetworkAccessManager>();
    uva::Uhuntqt api(manager);

    QObject::connect(&api, &uva::Uhuntqt::problemListDownloaded, 
        [] (QList<uva::ProblemInfo> data)
        {
            for (uva::ProblemInfo info : data)
            {
                cout << info.getProblemTitle().toStdString() << endl;
            }

        });

    QPushButton getProblemListButton;
    getProblemListButton.setText("Click me to get the problem list");

    QObject::connect(&getProblemListButton, &QPushButton::clicked,
                     &api, &uva::Uhuntqt::getProblemList);

    getProblemListButton.show();
    TestSampleData(api);

    return app.exec();
}