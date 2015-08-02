#include "editorwidget.h"
#include "ui_editorwidget.h"
#include "commons/colorizer.h"
#include <Qsci/qscilexercpp.h>
#include <Qsci/qscilexerjava.h>
#include <Qsci/qscilexerpascal.h>

using namespace uva;

EditorWidget::EditorWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    mUi(new Ui::EditorWidget)
{
    mUi->setupUi(this);

    setCFamilyLexer();
    styleEditor();

    mUi->editor->setText("#include <iostream>\n\nint main()\n{\n\t\n\treturn 0;\n}\n");
}

EditorWidget::~EditorWidget()
{
}

void EditorWidget::initialize()
{
}

void EditorWidget::onSelectedLanguageChanged(QString language)
{
    bool currentIsCFamilyLexer = mCurrentLanguage == Submission::Language::C
        || mCurrentLanguage == Submission::Language::CPP
        || mCurrentLanguage == Submission::Language::CPP11;

    if (language == "C" && !currentIsCFamilyLexer) {
        setCFamilyLexer();
        styleEditor();
        mCurrentLanguage = Submission::Language::C;
    } else if (language == "C++" && !currentIsCFamilyLexer) {
        setCFamilyLexer();
        styleEditor();
        mCurrentLanguage = Submission::Language::CPP;
    } else if (language == "C++11" && !currentIsCFamilyLexer) {
        setCFamilyLexer();
        styleEditor();
        mCurrentLanguage = Submission::Language::CPP11;
    } else if (language == "Java") {
        setJavaLexer();
        styleEditor();
        mCurrentLanguage = Submission::Language::JAVA;
    } else if (language == "Pascal") {
        setPascalLexer();
        styleEditor();
        mCurrentLanguage = Submission::Language::PASCAL;
    }
}

void uva::EditorWidget::styleEditor()
{
    QFont marginsFont("consolas");
    mUi->editor->setMarginsFont(marginsFont);
    QFontMetrics defaultFontMetrics(marginsFont);
    mUi->editor->setMarginWidth(0, defaultFontMetrics.width("0") + 6);
    mUi->editor->setMarginLineNumbers(0, true);
    mUi->editor->setMarginsForegroundColor(QColor(43, 145, 175));
    mUi->editor->setMarginsBackgroundColor(QColor(30, 30, 30));

    mUi->editor->setIndentationGuides(true);
    mUi->editor->setAutoIndent(true);

    mUi->editor->setBraceMatching(QsciScintilla::BraceMatch::SloppyBraceMatch);
    mUi->editor->setMatchedBraceForegroundColor(QColor(220, 220, 220));
    mUi->editor->setMatchedBraceBackgroundColor(QColor(14, 69, 131));
    mUi->editor->setUnmatchedBraceBackgroundColor(QColor(220, 220, 220));
    mUi->editor->setUnmatchedBraceForegroundColor(Qt::red);

    mUi->editor->setCaretLineVisible(true);
    mUi->editor->setCaretLineBackgroundColor(Qt::black);

    mUi->editor->setCaretForegroundColor(QColor(220, 220, 220));
    mUi->editor->setTabWidth(4);
    mUi->editor->setSelectionBackgroundColor(QColor(51, 153, 255));
}

void uva::EditorWidget::setCFamilyLexer()
{
    QsciLexer* curLexer = new QsciLexerCPP(mUi->editor);
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

    mUi->editor->setLexer(curLexer);
}

void uva::EditorWidget::setPascalLexer()
{
    QsciLexer* curLexer = new QsciLexerPascal(mUi->editor);
    QFont defaultFont("consolas");
    defaultFont.setFixedPitch(true);
    defaultFont.setPointSize(10);

    curLexer->setFont(defaultFont);
    curLexer->setDefaultColor(QColor(220, 220, 220));
    curLexer->setColor(QColor(220, 220, 220), QsciLexerPascal::Default);
    curLexer->setColor(QColor(86, 156, 214), QsciLexerPascal::Keyword);
    curLexer->setColor(QColor(220, 220, 220), QsciLexerPascal::Operator);
    curLexer->setColor(QColor(220, 220, 220), QsciLexerPascal::Identifier);
    curLexer->setColor(QColor(184, 215, 163), QsciLexerPascal::Number);
    curLexer->setColor(QColor(155, 155, 155), QsciLexerPascal::PreProcessor);
    curLexer->setColor(QColor(81, 166, 74), QsciLexerPascal::CommentLine);
    curLexer->setPaper(QColor(30, 30, 30));
    curLexer->setDefaultPaper(QColor(30, 30, 30));

    mUi->editor->setLexer(curLexer);
}

void EditorWidget::setJavaLexer()
{
    QsciLexer* curLexer = new QsciLexerJava(mUi->editor);
    QFont defaultFont("consolas");
    defaultFont.setFixedPitch(true);
    defaultFont.setPointSize(10);

    curLexer->setFont(defaultFont);
    curLexer->setDefaultColor(QColor(220, 220, 220));
    curLexer->setColor(QColor(220, 220, 220), QsciLexerJava::Default);
    curLexer->setColor(QColor(86, 156, 214), QsciLexerJava::Keyword);
    curLexer->setColor(QColor(220, 220, 220), QsciLexerJava::Operator);
    curLexer->setColor(QColor(220, 220, 220), QsciLexerJava::Identifier);
    curLexer->setColor(QColor(78, 201, 176), QsciLexerJava::UserLiteral);
    curLexer->setColor(QColor(184, 215, 163), QsciLexerJava::Number);
    curLexer->setColor(QColor(155, 155, 155), QsciLexerJava::PreProcessor);
    curLexer->setColor(QColor(81, 166, 74), QsciLexerJava::CommentLine);
    curLexer->setPaper(QColor(30, 30, 30));
    curLexer->setDefaultPaper(QColor(30, 30, 30));

    mUi->editor->setLexer(curLexer);
}
