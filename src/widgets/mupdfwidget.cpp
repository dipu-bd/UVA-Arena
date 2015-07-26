#include <QPainter>
#include <QDebug>
#include <QDateTime>
#include <QWheelEvent>

#include "widgets/mupdfwidget.h"

using namespace uva;

MuPDFWidget::MuPDFWidget(QWidget *parent)
    : QWidget(parent),
      mPDFDocument(nullptr),
      mCurrentPageIndex(0),
      mMaxWidth(0),
      mTotalHeight(0),
      mScale(1.0f),
      mRenderAllPages(false)
{
}

int MuPDFWidget::numPages()
{
    if (mPDFDocument)
        return mPDFDocument->numPages();

    return 0;
}

void MuPDFWidget::resizeToDocument()
{
    update();

    if (mRenderAllPages)
        QWidget::resize(mMaxWidth*mScale, mTotalHeight*mScale);
    else
        QWidget::resize(mWidth*mScale, mHeight*mScale);
}

void MuPDFWidget::loadDocument(QByteArray data)
{
    clear();

    mData = std::move(data);
    mPDFDocument.reset(MuPDF::loadDocument(mData));
    setupPages();

    if (mRenderAllPages)
        resizeToDocument();
    else
        setPage(1);
}

void MuPDFWidget::loadDocument(const QString &filePath)
{
    clear();

    mPDFDocument.reset(MuPDF::loadDocument(filePath));
    setupPages();

    if (mRenderAllPages)
        resizeToDocument();
    else
        setPage(1);
}

void MuPDFWidget::setPage(int pageNum)
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

void MuPDFWidget::clear()
{
    mPages.clear();
    mPDFDocument.reset(nullptr);
    mCurrentPageIndex = 0;
}

void MuPDFWidget::zoomIn()
{
    if (!mPDFDocument)
        return;

    mScale += 0.1f;
    resizeToDocument();
}

void MuPDFWidget::zoomOut()
{
    if (!mPDFDocument)
        return;

    mScale -= 0.1f;
    resizeToDocument();
}

void MuPDFWidget::setZoom(double amount)
{
    mScale = amount;

    if (!mPDFDocument)
        return;

    resizeToDocument();
}

void MuPDFWidget::setRenderAllPages(bool renderAll)
{
    mRenderAllPages = renderAll;

    if (!mPDFDocument)
        return;

    resizeToDocument();
}

void MuPDFWidget::paintEvent(QPaintEvent *event)
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

void MuPDFWidget::setupPages()
{
    mMaxWidth = 0;
    mTotalHeight = 0;
    for (int i = 0; i < mPDFDocument->numPages(); ++i) {
        mPages.push_back(std::unique_ptr<MuPDF::Page>(mPDFDocument->page(i)));
        mMaxWidth = qMax(mPages[i]->size().width(), mMaxWidth);
        mTotalHeight += mPages[i]->size().height();
    }
}
