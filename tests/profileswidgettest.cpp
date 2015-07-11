#include <QApplication>
#include <iostream>

#include "widgets/profileswidget.h"

using namespace std;
using namespace uva;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    ProfilesWidget widget;

    widget.show();

    return app.exec();
}
