#include "problemswidget.h"
#include "ui_problemswidget.h"

ProblemsWidget::ProblemsWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::ProblemsWidget)
{
    ui->setupUi(this);
}

ProblemsWidget::~ProblemsWidget()
{
    delete ui;
}
