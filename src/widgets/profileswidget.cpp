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
