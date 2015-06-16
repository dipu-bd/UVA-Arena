#ifndef JUDGESTATUSWIDGET_H
#define JUDGESTATUSWIDGET_H

#include <QWidget>

namespace Ui {
class JudgeStatusWidget;
}

class JudgeStatusWidget : public QWidget
{
    Q_OBJECT

public:
    explicit JudgeStatusWidget(QWidget *parent = 0);
    ~JudgeStatusWidget();

private:
    Ui::JudgeStatusWidget *ui;
};

#endif // JUDGESTATUSWIDGET_H
