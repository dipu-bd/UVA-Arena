#include <QPainter>
#include <QDebug>
#include <QDateTime>

#include "widgets/pdfviewer.h"

using namespace uva;

PDFViewer::PDFViewer(QWidget *parent)
    : QWidget(parent),
      mPdfDocument(nullptr),
      mCurrentPageIndex(0)
{
}

int PDFViewer::numPages()
{
    if (mPdfDocument)
        return mPdfDocument->numPages();

    return 0;
}

const QByteArray& PDFViewer::getPDFData()
{
    return mData;
}

void PDFViewer::loadDocument(QByteArray data)
{
    mData = std::move(data);
    mPdfDocument.reset(MuPDF::loadDocument(mData));
}

void PDFViewer::loadDocument(const QString &filePath)
{
    mPdfDocument.reset(MuPDF::loadDocument(filePath));
}

void PDFViewer::setPage(int index)
{
    if (!mPdfDocument)
        return;

    if (index < 0 || index >= mPdfDocument->numPages())
        return;

    mCurrentPageIndex = index;
}

void PDFViewer::paintEvent(QPaintEvent *event)
{
    QPainter painter(this);
    painter.setRenderHint(QPainter::RenderHint::TextAntialiasing);

    if (mPdfDocument) {

        std::unique_ptr<MuPDF::Page> page(mPdfDocument->page(mCurrentPageIndex));
        
        if (page) {
            painter.drawImage(QPoint(), page->renderImage());
            page->renderImage();
        }

    } else {
        painter.setPen(Qt::white);
        painter.setFont(QFont("consolas", 32));
        painter.drawText(rect(), Qt::AlignCenter, "No PDF document loaded");
    }
}
