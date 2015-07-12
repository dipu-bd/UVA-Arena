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

void JudgeStatusWidget::initialize()
{

}

void JudgeStatusWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}
