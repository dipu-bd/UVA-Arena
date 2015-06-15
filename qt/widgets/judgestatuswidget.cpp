#include "judgestatuswidget.h"
#include "ui_judgestatuswidget.h"

JudgeStatusWidget::JudgeStatusWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::JudgeStatusWidget)
{
    ui->setupUi(this);
}

JudgeStatusWidget::~JudgeStatusWidget()
{
    delete ui;
}
