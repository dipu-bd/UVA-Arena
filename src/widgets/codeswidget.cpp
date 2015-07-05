#include "codeswidget.h"
#include "ui_codeswidget.h"
#include <Qsci/qscilexercpp.h>

using namespace uva;

CodesWidget::CodesWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::CodesWidget)
{
    ui->setupUi(this);

    QsciLexer* curLexer = new QsciLexerCPP(ui->editor);
    ui->editor->setLexer(curLexer);
}

CodesWidget::~CodesWidget()
{
    delete ui;
}
