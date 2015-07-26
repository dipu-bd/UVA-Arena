#include <QApplication>
#include <iostream>

#include "widgets/editorwidget.h"

using namespace std;
using namespace uva;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    EditorWidget widget;

    widget.show();

    return app.exec();
}
