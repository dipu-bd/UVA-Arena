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
      mMaxWidth(0),
      mTotalHeight(0),
      mScale(1.0f),
      mRenderAllPages(false)
{
}

int PDFViewer::numPages()
{
    if (mPDFDocument)
        return mPDFDocument->numPages();

    return 0;
}

void PDFViewer::resizeToDocument()
{
    update();

    if (mRenderAllPages)
        QWidget::resize(mMaxWidth*mScale, mTotalHeight*mScale);
    else
        QWidget::resize(mWidth*mScale, mHeight*mScale);
}

void PDFViewer::loadDocument(QByteArray data)
{
    clear();

    mData = std::move(data);
    mPDFDocument.reset(MuPDF::loadDocument(mData));
    setupPages();
    setPage(1);
}

void PDFViewer::loadDocument(const QString &filePath)
{
    clear();

    mPDFDocument.reset(MuPDF::loadDocument(filePath));
    setupPages();
    setPage(1);
}

void PDFViewer::setPage(int pageNum)
{
    if (!mPDFDocument)
        return;

    if (mRenderAllPages)
        return;

    int index = pageNum - 1;

    if (index < 0 || index >= mPDFDocument->numPages())
        return;

    mCurrentPageIndex = index;
    mWidth = mPages[index]->size().width();
    mHeight = mPages[index]->size().height();
    resizeToDocument();
}

void PDFViewer::clear()
{
    mPages.clear();
    mPDFDocument.reset(nullptr);
    mCurrentPageIndex = 0;
}

void PDFViewer::zoomIn()
{
    if (!mPDFDocument)
        return;

    mScale += 0.1f;
    resizeToDocument();
}

void PDFViewer::zoomOut()
{
    if (!mPDFDocument)
        return;

    mScale -= 0.1f;
    resizeToDocument();
}

void PDFViewer::setZoom(double amount)
{
    mScale = amount;

    if (!mPDFDocument)
        return;

    resizeToDocument();
}

void PDFViewer::setRenderAllPages(bool renderAll)
{
    mRenderAllPages = renderAll;

    if (!mPDFDocument)
        return;

    resizeToDocument();
}

void PDFViewer::paintEvent(QPaintEvent *event)
{
    QPainter painter(this);

    if (mPDFDocument) {
        painter.setRenderHint(QPainter::RenderHint::HighQualityAntialiasing);

        if (mRenderAllPages) {
            qreal curHeight = 0;
            for (size_t i = 0; i < mPages.size(); ++i) {
                if (mPages[i]) {
                    painter.drawImage(QPoint(0, curHeight),
                        mPages[i]->renderImage(mScale, mScale));

                    curHeight += mPages[i]->size().height() * mScale;
                }
            }

        } else {

            if (mPages[mCurrentPageIndex]) {
                painter.drawImage(QPoint(),
                    mPages[mCurrentPageIndex]->renderImage(mScale, mScale));
            }
        }

    } else {
        painter.setRenderHint(QPainter::RenderHint::TextAntialiasing);
        painter.setPen(Qt::white);
        painter.setFont(QFont("consolas", 32));
        painter.drawText(rect(), Qt::AlignCenter, "No PDF document loaded");
    }
}

void PDFViewer::setupPages()
{
    mMaxWidth = 0;
    mTotalHeight = 0;
    for (int i = 0; i < mPDFDocument->numPages(); ++i) {
        mPages.push_back(std::unique_ptr<MuPDF::Page>(mPDFDocument->page(i)));
        mMaxWidth = qMax(mPages[i]->size().width(), mMaxWidth);
        mTotalHeight += mPages[i]->size().height();
    }
}
