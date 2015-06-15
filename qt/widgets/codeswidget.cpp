#include "codeswidget.h"
#include "ui_codeswidget.h"

CodesWidget::CodesWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::CodesWidget)
{
    ui->setupUi(this);
}

CodesWidget::~CodesWidget()
{
    delete ui;
}
