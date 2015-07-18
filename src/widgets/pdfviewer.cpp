#include <QPainter>
#include <QDebug>
#include <QDateTime>
#include <QWheelEvent>

#include "widgets/pdfviewer.h"

using namespace uva;

PDFViewer::PDFViewer(QWidget *parent)
    : QWidget(parent),
      mPDFDocument(nullptr),
      mCurrentPageIndex(0),
      mScale(1.0f)
{
}

int PDFViewer::numPages()
{
    if (mPDFDocument)
        return mPDFDocument->numPages();

    return 0;
}

void PDFViewer::loadDocument(QByteArray data)
{
    clear();

    mData = std::move(data);
    mPDFDocument.reset(MuPDF::loadDocument(mData));
    setupPages();
}

void PDFViewer::loadDocument(const QString &filePath)
{
    clear();

    mPDFDocument.reset(MuPDF::loadDocument(filePath));
    setupPages();
}

void PDFViewer::setPage(int pageNum)
{
    if (!mPDFDocument)
        return;

    int index = pageNum - 1;

    if (index < 0 || index >= mPDFDocument->numPages())
        return;

    mCurrentPageIndex = index;
    resize(mPages[index]->size().toSize());
}

void PDFViewer::clear()
{
    mPages.clear();
    mCurrentPageIndex = 0;
}

void PDFViewer::zoomIn()
{
    mScale += 0.1f;
    update();
    resize((mPages[mCurrentPageIndex]->size()*mScale).toSize());
}

void PDFViewer::zoomOut()
{
    mScale -= 0.1f;
    update();
    resize((mPages[mCurrentPageIndex]->size()*mScale).toSize());
}

void PDFViewer::paintEvent(QPaintEvent *event)
{
    QPainter painter(this);
    painter.setRenderHint(QPainter::RenderHint::TextAntialiasing);

    if (mPDFDocument) {
        painter.setRenderHint(QPainter::RenderHint::HighQualityAntialiasing);

        if (mPages[mCurrentPageIndex]) {
            painter.drawImage(QPoint(),
                mPages[mCurrentPageIndex]->renderImage(mScale, mScale));
        }

    } else {
        painter.setPen(Qt::white);
        painter.setFont(QFont("consolas", 32));
        painter.drawText(rect(), Qt::AlignCenter, "No PDF document loaded");
    }
}

void PDFViewer::setupPages()
{
    for (int i = 0; i < mPDFDocument->numPages(); ++i)
        mPages.push_back(std::unique_ptr<MuPDF::Page>(mPDFDocument->page(i)));

    setPage(1);
}
