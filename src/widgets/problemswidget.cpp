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

Uhunt::ProblemMap ProblemsWidget::getProblemMap()
{
    return mProblems;
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

    mProblems = Uhunt::problemMapFromData(data);
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

    mProblems = Uhunt::problemMapFromData(data);
    emit newUVAArenaEvent(UVAArenaEvent::UPDATE_STATUS, "Problem list downloaded.");
}

