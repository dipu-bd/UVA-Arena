#include "judgestatuswidget.h"
#include "ui_judgestatuswidget.h"

using namespace uva;

JudgeStatusWidget::JudgeStatusWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::JudgeStatusWidget)
{
    ui->setupUi(this);
}

JudgeStatusWidget::~JudgeStatusWidget()
{
    delete ui;
}
