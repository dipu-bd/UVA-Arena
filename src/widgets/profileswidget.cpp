#include "profileswidget.h"
#include "ui_profileswidget.h"

using namespace uva;

ProfilesWidget::ProfilesWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::ProfilesWidget)
{
    ui->setupUi(this);
}

ProfilesWidget::~ProfilesWidget()
{
    delete ui;
}
