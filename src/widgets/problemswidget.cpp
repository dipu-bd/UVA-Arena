#include "problemswidget.h"
#include "ui_problemswidget.h"

#include <QFile>
#include <QFileInfo>
#include <QMessageBox>
#include <QDataStream>
#include <QDateTime>
#include <QDir>
#include <QStandardPaths>

using namespace uva;

const QString DefaultProblemListFileName = "problemlist.json";

ProblemsWidget::ProblemsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    mMaxDaysUntilProblemListRedownload(1),
    ui(new Ui::ProblemsWidget)
{
    ui->setupUi(this);
}

ProblemsWidget::~ProblemsWidget()
{
    delete ui;
}

void ProblemsWidget::initialize()
{
    QObject::connect(mUhuntApi.get(), &Uhunt::problemListByteArrayDownloaded,
        this, &ProblemsWidget::onProblemListByteArrayDownloaded);

    // check if problem list is already downloaded
    QString result = QStandardPaths::locate(QStandardPaths::AppDataLocation,
        DefaultProblemListFileName);

    if (result.isEmpty()) { // file not found

        // download the problem list and save it
        mUhuntApi->getProblemListAsByteArray();

    } else {

        // file found, load the data
        loadProblemListFromFile(result);
    }
}

//get the problem map
Uhunt::ProblemMap ProblemsWidget::getProblemMap()
{
    return mProblems;
}

//set the problem map
void ProblemsWidget::setProblemMap(Uhunt::ProblemMap pMap)
{
    mProblems = pMap;
    mIdToNumber = QMap<int, int>();
    Uhunt::ProblemMap::const_iterator it = pMap.begin();
    for( ; it != pMap.end(); ++it)
    {
        mIdToNumber[it.value().getID()] = it.value().getNumber();
    }
}

//get problem number by problem id
int ProblemsWidget::getProblemNumber(int problemId)
{
    if(mIdToNumber.contains(problemId))
        return mIdToNumber[problemId];
    return 0;
}

//get problem id from problem number
int ProblemsWidget::getProblemId(int problemNumber)
{
    if(mProblems.contains(problemNumber))
        return mProblems[problemNumber].getID();
    return 0;
}

//get problem title by problem number
QString ProblemsWidget::getProbelmTitle(int problemNumber)
{
    if(mProblems.contains(problemNumber))
        return mProblems[problemNumber].getTitle();
    return "-";
}

//get problem by id
Problem ProblemsWidget::getProblemById(int problemId)
{
    return getProblemByNumber(getProblemNumber(problemId));
}

//get problem by problem number
Problem ProblemsWidget::getProblemByNumber(int problemNumber)
{
    if(mProblems.contains(problemNumber))
        return mProblems[problemNumber];
    return Problem();
}

void ProblemsWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}

void ProblemsWidget::loadProblemListFromFile(QString fileName)
{

    QFile file(fileName);

    if (!file.open(QIODevice::ReadOnly)) {

        // TODO: couldn't open the file
        QMessageBox::critical(this, "Read failure",
            "Could not read the default problem list file.");

        return;
    }

    // make sure the default probelm list file is not too old
    QFileInfo fileInfo(file);
    QDateTime lastModified = fileInfo.lastModified();

    if (lastModified.daysTo(QDateTime::currentDateTime())
                                        > mMaxDaysUntilProblemListRedownload) {

        // the problem list file is too old, redownload it

        mUhuntApi->getProblemListAsByteArray();

        return;
    }

    QDataStream dataStream(&file);
    QByteArray data;

    dataStream >> data;

    setProblemMap(Uhunt::problemMapFromData(data));
    emit newUVAArenaEvent(UVAArenaEvent::UPDATE_STATUS, "Problem list loaded from file.");
}

void ProblemsWidget::onProblemListByteArrayDownloaded(QByteArray data)
{
    // set the file to save to
    QString saveDirectory =
        QStandardPaths::writableLocation(QStandardPaths::AppDataLocation);

    if (!QFile::exists(saveDirectory)) {
        QDir dirToMake;
        dirToMake.mkpath(saveDirectory);
    }

    QFile file(saveDirectory + "/" + DefaultProblemListFileName);

    if (!file.open(QIODevice::WriteOnly)) {

        // couldn't open the file
        QMessageBox::critical(this, "Write failure",
            "Could not write to the default problem list file:\n"
            + saveDirectory + "/" + DefaultProblemListFileName);

        return;
    }

    // write the problem list data
    QDataStream dataStream(&file);
    dataStream << data;

    setProblemMap(Uhunt::problemMapFromData(data));
    emit newUVAArenaEvent(UVAArenaEvent::UPDATE_STATUS, "Problem list downloaded.");
}

