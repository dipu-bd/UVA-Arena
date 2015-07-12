#include <stdio.h>
#include <iostream>
#include <memory>
#include <QList>
#include <QNetworkAccessManager>

#include <QApplication>
#include <QFrame>
#include <QPushButton>
#include <QVBoxLayout>

#include <QFile>

#include "mainwindow.h"
#include "uhunt/uhunt.h"
#include "uhunt/categorynode.h"

using namespace std;
using namespace uva;


int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    /*
      Category data file
      Download link: https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/CP%20Book3.cat
     */
    QString file = "CP Book3.cat";
    if(QFile::exists(file))
        cout << "File exist." << endl;
    else
        cout << "File not exist." << endl;

    //Get category node
    QFile f(file);
    if (!f.open(QFile::ReadOnly | QFile::Text))
        qDebug() << "Error while reading the file";
    QByteArray json = f.readAll();
    const QJsonDocument& jdoc = QJsonDocument::fromJson(json);
    CategoryNode node = CategoryNode::fromJsonObject(jdoc.object());

    cout << node.getName().toStdString() << endl;
    cout << node.getNote().toStdString() << endl;
    cout << node.getProblems().count() << endl;
    cout << node.getCategorNodes().count() << endl;

    return app.exec();
}
