#include <QApplication>
#include <iostream>

#include "widgets/judgestatuswidget.h"

using namespace std;
using namespace uva;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    JudgeStatusWidget widget;

    widget.show();

    return app.exec();
}
