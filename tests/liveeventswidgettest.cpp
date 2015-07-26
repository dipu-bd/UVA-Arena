#include <QApplication>
#include <iostream>
#include <QNetworkAccessManager>

#include "widgets/liveeventswidget.h"
#include "uhunt/uhunt.h"

using namespace std;
using namespace uva;


int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    LiveEventsWidget widget;
    widget.show();

    return app.exec();
}
