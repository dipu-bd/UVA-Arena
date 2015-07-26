#include "pdfviewer.h"
#include "ui_pdfviewer.h"
#include "QMessageBox"
#include "QFile"
#include "QNetworkReply"

using namespace uva;

PDFViewer::PDFViewer(QWidget *parent) :
    QWidget(parent),
    mNetworkManager(nullptr),
    mSaveOnDownload(true),
    mUi(new Ui::PDFViewer)
{
    mUi->setupUi(this);
    mUi->mupdfWidget->setZoom(mUi->zoomDoubleSpinBox->value());
    mUi->mupdfWidget->setRenderAllPages(mUi->renderAllPagesCheckBox->isChecked());
}

void PDFViewer::downloadPDF(const QString &url, const QString &saveFileName /*= QString()*/)
{
    if (!mNetworkManager)
        return;

    QNetworkRequest request;
    request.setUrl(QUrl(url));

    QNetworkReply *reply = mNetworkManager->get(request);

    if (reply == nullptr)
        return;

    QObject::connect(reply, &QNetworkReply::finished,
        [this, reply, saveFileName]() {

        if (reply && reply->error() == QNetworkReply::NoError) {

            QByteArray pdfData = reply->readAll();
            mUi->mupdfWidget->loadDocument(pdfData);
            mUi->pageNumSpinBox->setSuffix(tr("/%1").arg(mUi->mupdfWidget->numPages()));
            mUi->pageNumSpinBox->setMaximum(mUi->mupdfWidget->numPages());

            if (mSaveOnDownload && !saveFileName.isEmpty())
                savePDF(saveFileName, pdfData);
        }

        reply->deleteLater();
    });
}

void PDFViewer::savePDF(const QString &fileName, const QByteArray &pdfData)
{
    QFile file(fileName);

    if (!file.open(QIODevice::WriteOnly)) {

        // couldn't open the file
        QMessageBox::critical(this, "Write failure",
            "Could not save pdf file:\n"
            + fileName);

        return;
    }

    QDataStream dataStream(&file);
    dataStream << pdfData;
}

void PDFViewer::loadDocument(const QString &fileName)
{
    mUi->mupdfWidget->loadDocument(fileName);
    mUi->pageNumSpinBox->setSuffix(tr("/%1").arg(mUi->mupdfWidget->numPages()));
    mUi->pageNumSpinBox->setMaximum(mUi->mupdfWidget->numPages());
}

uva::PDFViewer::~PDFViewer()
{

}

bool PDFViewer::saveOnDownload() const
{
    return mSaveOnDownload;
}

void PDFViewer::setSaveOnDownload(bool val)
{
    mSaveOnDownload = val;
}

void PDFViewer::setNetworkManager(std::shared_ptr<QNetworkAccessManager> val)
{
    mNetworkManager = val;
}

