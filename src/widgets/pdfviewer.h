#pragma once

#include "uvalib_global.h"

#include <QWidget>
#include <mupdf-qt.h>

#include <memory>

namespace uva
{

    class UVA_EXPORT PDFViewer : public QWidget
    {
        Q_OBJECT

    public:
        PDFViewer(QWidget *parent = 0);

        int numPages();

    public slots:

        void loadDocument(const QByteArray &data);

        void loadDocument(const QString  &filePath);

        void setPage(int index);

    protected:

        virtual void paintEvent(QPaintEvent *event);

    private:
        int mCurrentPageIndex;
        std::unique_ptr<MuPDF::Document> mPdfDocument;
    };
}
