#pragma once

#include "src_global.h"
#include <QWidget>

namespace Ui {
class JudgeStatusWidget;
}

class UVA_EXPORT JudgeStatusWidget : public QWidget
{
    Q_OBJECT

public:
    explicit JudgeStatusWidget(QWidget *parent = 0);
    ~JudgeStatusWidget();

private:
    Ui::JudgeStatusWidget *ui;
};
