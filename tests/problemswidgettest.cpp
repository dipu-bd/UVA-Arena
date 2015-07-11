#include <QApplication>
#include <iostream>

#include "widgets/problemswidget.h"

using namespace std;
using namespace uva;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    ProblemsWidget widget;

    widget.show();

    return app.exec();
}
