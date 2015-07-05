#include "problemswidget.h"
#include "ui_problemswidget.h"

using namespace uva;

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
