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

        const QByteArray& getPDFData();

    public slots:

        /*!
            \brief Load a document from a copy of a PDF File raw data.

            This is a copy because mupdf does not make its own copy
            of the PDF data.
        */
        void loadDocument(QByteArray data);

        void loadDocument(const QString &filePath);

        void setPage(int index);

    protected:

        virtual void paintEvent(QPaintEvent *event);

    private:
        QByteArray mData;
        int mCurrentPageIndex;
        std::unique_ptr<MuPDF::Document> mPdfDocument;
    };
}
