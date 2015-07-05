#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include <QWidget>

namespace uva
{

    namespace Ui {
        class JudgeStatusWidget;
    }

    class UVA_EXPORT JudgeStatusWidget : public UVAArenaWidget
    {
        Q_OBJECT

    public:
        explicit JudgeStatusWidget(QWidget *parent = 0);
        ~JudgeStatusWidget();

    private:
        Ui::JudgeStatusWidget *ui;
    };

}