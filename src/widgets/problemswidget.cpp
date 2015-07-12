#include "problemswidget.h"
#include "ui_problemswidget.h"

using namespace uva;

ProblemsWidget::ProblemsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::ProblemsWidget)
{
    ui->setupUi(this);
}

ProblemsWidget::~ProblemsWidget()
{
    delete ui;
}

void ProblemsWidget::initialize()
{
}

void ProblemsWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}
