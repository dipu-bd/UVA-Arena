#pragma once
#include "QWidget"
#include <memory>
#include "QNetworkAccessManager"

#include "uvalib_global.h"

namespace uva
{

    namespace Ui {
        class PDFViewer;
    }

    class UVA_EXPORT PDFViewer : public QWidget
    {
        Q_OBJECT
    public:
        explicit PDFViewer(QWidget *parent = 0);

        ~PDFViewer();

        /*!
            \brief 
        
            \param url
            \param saveFileName
        
            \return A void
        */
        void downloadPDF(const QString &url, const QString &saveFileName = QString());

        /*!
            \brief 
        
            \param fileName
        
            \return A void
        */
        void loadDocument(const QString &fileName);

        /*!
            \brief 

            \return A bool
        */
        bool saveOnDownload() const;

        /*!
            \brief 

            \param val

            \return A void
        */
        void setSaveOnDownload(bool val);

        /*!
            \brief If set, calls to downloadPDF() will downloads PDFs from the
                   internet. Otherwise, downloadPDF() will do nothing.

            \param val

            \return A void
        */
        void setNetworkManager(std::shared_ptr<QNetworkAccessManager> val);

    protected:

    private:

        std::shared_ptr<QNetworkAccessManager> mNetworkManager;
        bool mSaveOnDownload;

        void savePDF(const QString &fileName, const QByteArray &pdfData);

        std::unique_ptr<Ui::PDFViewer> mUi;
    };

}

