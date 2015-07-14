#include "profileswidget.h"
#include "ui_profileswidget.h"

using namespace uva;

ProfilesWidget::ProfilesWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::ProfilesWidget)
{
    ui->setupUi(this);
}

ProfilesWidget::~ProfilesWidget()
{
    delete ui;
}

void ProfilesWidget::initialize()
{   
}

void ProfilesWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}
