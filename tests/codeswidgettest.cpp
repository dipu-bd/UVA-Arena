#include <QApplication>
#include <iostream>

#include "widgets/codeswidget.h"

using namespace std;
using namespace uva;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    CodesWidget widget;

    widget.show();

    return app.exec();
}
