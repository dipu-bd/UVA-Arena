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
    widget.show();

    return app.exec();
}
