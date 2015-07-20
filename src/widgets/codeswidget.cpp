#include "codeswidget.h"
#include "ui_codeswidget.h"
#include "commons/colorizer.h"
#include <Qsci/qscilexercpp.h>

using namespace uva;

CodesWidget::CodesWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::CodesWidget)
{
    ui->setupUi(this);

    QsciLexer* curLexer = new QsciLexerCPP(ui->editor);
    QFont defaultFont("consolas");
    defaultFont.setFixedPitch(true);
    defaultFont.setPointSize(10);
    
    curLexer->setFont(defaultFont);
    curLexer->setDefaultColor(QColor(220, 220, 220));
    curLexer->setColor(QColor(220, 220, 220), QsciLexerCPP::Default);
    curLexer->setColor(QColor(86, 156, 214), QsciLexerCPP::Keyword);
    curLexer->setColor(QColor(220, 220, 220), QsciLexerCPP::Operator);
    curLexer->setColor(QColor(220, 220, 220), QsciLexerCPP::Identifier);
    curLexer->setColor(QColor(78, 201, 176), QsciLexerCPP::UserLiteral);
    curLexer->setColor(QColor(184, 215, 163), QsciLexerCPP::Number);
    curLexer->setColor(QColor(155, 155, 155), QsciLexerCPP::PreProcessor);
    curLexer->setColor(QColor(81, 166, 74), QsciLexerCPP::CommentLine);
    curLexer->setPaper(QColor(30, 30, 30));
    curLexer->setDefaultPaper(QColor(30, 30, 30));

    ui->editor->setMarginsFont(defaultFont);
    QFontMetrics defaultFontMetrics(defaultFont);
    ui->editor->setMarginWidth(0, defaultFontMetrics.width("0") + 6);
    ui->editor->setMarginLineNumbers(0, true);
    ui->editor->setMarginsForegroundColor(QColor(43, 145, 175));
    ui->editor->setMarginsBackgroundColor(QColor(30, 30, 30));

    ui->editor->setIndentationGuides(true);
    ui->editor->setAutoIndent(true);

    ui->editor->setBraceMatching(QsciScintilla::BraceMatch::SloppyBraceMatch);
    ui->editor->setMatchedBraceForegroundColor(QColor(220, 220, 220));
    ui->editor->setMatchedBraceBackgroundColor(QColor(14, 69, 131));
    ui->editor->setUnmatchedBraceBackgroundColor(QColor(220, 220, 220));
    ui->editor->setUnmatchedBraceForegroundColor(Qt::red);
    
    ui->editor->setCaretLineVisible(true);
    ui->editor->setCaretLineBackgroundColor(Qt::black);

    ui->editor->setCaretForegroundColor(QColor(220, 220, 220));
    ui->editor->setTabWidth(4);
    ui->editor->setSelectionBackgroundColor(QColor(51, 153, 255));

    ui->editor->setLexer(curLexer);

    ui->editor->setText("#include <iostream>\n\nint main()\n{\n\t\n\treturn 0;\n}\n");

}

CodesWidget::~CodesWidget()
{
    delete ui;
}

void CodesWidget::initialize()
{
    
}

void CodesWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}
