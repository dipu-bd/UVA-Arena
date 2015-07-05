#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include <QWidget>
#include <mupdf-qt.h>

namespace uva
{

    namespace Ui {
        class ProblemsWidget;
    }

    class UVA_EXPORT ProblemsWidget : public UVAArenaWidget
    {
        Q_OBJECT

    public:
        explicit ProblemsWidget(QWidget *parent = 0);
        ~ProblemsWidget();

    private:
        Ui::ProblemsWidget *ui;
        MuPDF::Document* pdfDocument;
    };

}
