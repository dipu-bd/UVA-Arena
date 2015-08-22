#include <QApplication>
#include <iostream>
#include <memory>
#include "widgets/profileswidget.h"
#include <QNetworkAccessManager>

#include "uhunt/uhunt.h"

using namespace std;
using namespace uva;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    QCoreApplication::setOrganizationName("UVA Arena");
    QCoreApplication::setApplicationName("UVA Arena");
    QCoreApplication::setApplicationVersion("0.0.0");

    UVAArenaSettings settings;
    settings.setUserId(750840);

    cout << settings.userId() << endl;

    shared_ptr<QNetworkAccessManager> networkManager = make_shared<QNetworkAccessManager>();
    shared_ptr<Uhunt> uhuntApi = make_shared<Uhunt>(networkManager);

    ProfilesWidget widget;

    widget.setNetworkManager(networkManager);
    widget.setUhuntApi(uhuntApi);

    widget.initialize();

    widget.show();

    settings.setUserId(-1);

    return app.exec();
}
