#include <QApplication>
#include <QFile>
#include <QDebug>
#include <QScrollArea>
#include <QScrollBar>
#include <QPushButton>
#include <QVBoxLayout>
#include <QFrame>

#include "widgets/pdfviewer.h"

using namespace uva;

const QString testPDFFile = "100.pdf";

int main(int argc, char* argv[])
{
    if (!QFile::exists(testPDFFile)) {
        qDebug() << "Could not load pdf file.";
        return 0;
    }

    QApplication app(argc, argv);

    PDFViewer viewer;
    viewer.loadDocument(testPDFFile);

    viewer.show();

    return app.exec();
}