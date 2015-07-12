#include "mainwindow.h"
#include <QApplication>
#include <QFile>
#include <QTextStream>
#include <QNetworkAccessManager>

using namespace uva;

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    QFile f(":qdarkstyle/style.qss");
    if (!f.exists())
    {
        printf("Unable to set stylesheet, file not found\n");
    }
    else
    {
        f.open(QFile::ReadOnly | QFile::Text);
        QTextStream ts(&f);
        a.setStyleSheet(ts.readAll());
    }

    QCoreApplication::setOrganizationName("UVA Arena");
    QCoreApplication::setApplicationName("UVA Arena");
    QCoreApplication::setApplicationVersion("0.0.0");

    MainWindow w(std::make_shared<QNetworkAccessManager>());
    w.show();

    return a.exec();
}
