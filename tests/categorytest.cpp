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
#include "uhunt/category.h"

using namespace std;
using namespace uva;


int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    /*
      Category data file
      Download link: https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/CP%20Book3.cat
     */
    QString file = "CP Book 3.cat";
    if (!QFile::exists(file)) {
        cout << "File does not exist." << endl;
        getchar();
        return 0;
    }
        
    //Get category node
    QFile f(file);
    if (!f.open(QFile::ReadOnly | QFile::Text))
        qDebug() << "Error while reading the file";

    const QJsonDocument& jdoc = QJsonDocument::fromJson(f.readAll());
    Category node = Category::fromJsonObject(jdoc.object());

    cout << node.Name.toStdString() << endl;
    cout << node.Note.toStdString() << endl;
    cout << node.Problems.count() << endl;
    cout << node.Branches.count() << endl;

    return app.exec();
}
