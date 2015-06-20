#ifndef JUDGESTATUSWIDGET_H
#define JUDGESTATUSWIDGET_H

#include "src_global.h"
#include <QWidget>

namespace Ui {
class JudgeStatusWidget;
}

class SRCSHARED_EXPORT JudgeStatusWidget : public QWidget
{
    Q_OBJECT

public:
    explicit JudgeStatusWidget(QWidget *parent = 0);
    ~JudgeStatusWidget();

private:
    Ui::JudgeStatusWidget *ui;
};

#endif // JUDGESTATUSWIDGET_H
