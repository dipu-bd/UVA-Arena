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

    QFrame frame;
    QScrollArea scrollArea;
    PDFViewer viewer;

    viewer.loadDocument(testPDFFile);

    scrollArea.setAlignment(Qt::AlignHCenter);
    scrollArea.setWidget(&viewer);

    QPushButton zoomInButton;
    zoomInButton.setText("Zoom in");
    QObject::connect(&zoomInButton, &QPushButton::clicked, &viewer, &PDFViewer::zoomIn);

    QPushButton zoomOutButton;
    zoomOutButton.setText("Zoom out");
    QObject::connect(&zoomOutButton, &QPushButton::clicked, &viewer, &PDFViewer::zoomOut);

    QVBoxLayout verticalLayout;
    verticalLayout.addWidget(&zoomInButton);
    verticalLayout.addWidget(&zoomOutButton);
    verticalLayout.addWidget(&scrollArea);
 
    frame.setLayout(&verticalLayout);

    frame.show();

    return app.exec();
}