#include "profileswidget.h"
#include "ui_profileswidget.h"

using namespace uva;

ProfilesWidget::ProfilesWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    mUi(new Ui::ProfilesWidget)
{
    mUi->setupUi(this);
}

ProfilesWidget::~ProfilesWidget()
{
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
