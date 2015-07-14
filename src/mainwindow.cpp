#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QFile>
#include <QFileInfo>
#include <QMessageBox>
#include <QDataStream>
#include <QDateTime>
#include <QDir>
#include <QStandardPaths>

using namespace uva;

const QString DefaultProblemListFileName = "problemlist.json";

MainWindow::MainWindow(std::shared_ptr<QNetworkAccessManager> networkManager, QWidget *parent) :
    QMainWindow(parent),
    mNetworkManager(networkManager),
    mProblems(new Uhunt::ProblemMap),
    mProblemIdToNumber(new QMap<int, int>),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    this->setWindowState(Qt::WindowState::WindowMaximized);
    statusBar()->showMessage("Welcome to UVA-Arena.");

    mUhuntApi = std::make_shared<Uhunt>(mNetworkManager);

#ifdef _DEBUG
    // if we're testing, save to test directories
    QStandardPaths::setTestModeEnabled(true);
#endif

    initialize();
}

MainWindow::~MainWindow()
{
    delete ui;
}

std::shared_ptr<Uhunt::ProblemMap> MainWindow::getProblemMap()
{
    return mProblems;
}

int MainWindow::getProblemNumberFromId(int problemId)
{
    if (mProblemIdToNumber) {
        if (mProblemIdToNumber->contains(problemId))
            return mProblemIdToNumber->value(problemId);
    }

    return 0;
}

int MainWindow::getProblemIdFromNumber(int problemNumber)
{
    if (mProblems) {
        if (mProblems->contains(problemNumber))
            return mProblems->value(problemNumber).getID();
    }

    return 0;
}

QString MainWindow::getProblemTitle(int problemNumber)
{
    if (mProblems) {
        if (mProblems->contains(problemNumber))
            return mProblems->value(problemNumber).getTitle();
    }

    return "-";
}

Problem MainWindow::getProblemById(int problemId)
{
    return getProblemByNumber(getProblemNumberFromId(problemId));
}

Problem MainWindow::getProblemByNumber(int problemNumber)
{
    if (problemNumber) {
        if (mProblems->contains(problemNumber))
            return mProblems->value(problemNumber);
    }

    return Problem();
}

void MainWindow::onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent arenaEvent, QVariant metaData)
{
    typedef UVAArenaWidget::UVAArenaEvent UVAArenaEvent;

    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        statusBar()->showMessage(metaData.toString());
        break;

    default:
        break;
    }
}

void MainWindow::onProblemListByteArrayDownloaded(QByteArray data)
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

    // write the problem list data to the default problems list file
    QDataStream dataStream(&file);
    dataStream << data;

    setProblemMap(Uhunt::problemMapFromData(data));
}

void MainWindow::setProblemMap(Uhunt::ProblemMap problemMap)
{
    *mProblems = std::move(problemMap);
    mProblemIdToNumber->clear();

    Uhunt::ProblemMap::const_iterator it = mProblems->begin();

    while (it != mProblems->end()) {
        mProblemIdToNumber->insert(it->getID(), it->getNumber());
        ++it;
    }

    ui->problemsWidget->setProblemsMap(getProblemMap());
    statusBar()->showMessage("Problem list loaded", 2000);
}


void MainWindow::initialize()
{
    initializeData();
    initializeWidgets();
}

void MainWindow::initializeData()
{
    QObject::connect(mUhuntApi.get(), &Uhunt::problemListByteArrayDownloaded,
        this, &MainWindow::onProblemListByteArrayDownloaded);

    // check if problem list is already downloaded
    QString result = QStandardPaths::locate(QStandardPaths::AppDataLocation,
        DefaultProblemListFileName);

    if (result.isEmpty()) {
        // download the problem list and save it
        mUhuntApi->getProblemListAsByteArray();
    }
    else {
        // file found, load the data
        loadProblemListFromFile(result);
    }
}

void MainWindow::initializeWidgets()
{
    // Initialize all UVAArenaWidgets and connect them
    mUVAArenaWidgets.push_back(ui->problemsWidget);
    mUVAArenaWidgets.push_back(ui->codesWidget);
    mUVAArenaWidgets.push_back(ui->judgeStatusWidget);
    mUVAArenaWidgets.push_back(ui->profilesWidget);

    for (UVAArenaWidget* widget : mUVAArenaWidgets) {

        widget->setNetworkManager(mNetworkManager);
        widget->setUhuntApi(mUhuntApi);

        widget->setMainWindow(this);
        widget->setProblemsWidget(ui->problemsWidget);
        widget->setCodesWidget(ui->codesWidget);
        widget->setJudgeStatusWidget(ui->judgeStatusWidget);
        widget->setProfilesWidget(ui->profilesWidget);

        QObject::connect(widget, &UVAArenaWidget::newUVAArenaEvent,
            this, &MainWindow::onUVAArenaEvent);

        // connect this widget's events to all other widgets
        // don't allow the widget to connect to itself
        for (UVAArenaWidget* other : mUVAArenaWidgets) {
            if (widget == other)
                continue;

            QObject::connect(widget, &UVAArenaWidget::newUVAArenaEvent,
                other, &UVAArenaWidget::onUVAArenaEvent);
        }

        widget->initialize();
    }
}

void MainWindow::loadProblemListFromFile(QString fileName)
{

    QFile file(fileName);

    if (!file.open(QIODevice::ReadOnly)) {

        // TODO: couldn't open the file
        QMessageBox::critical(this, "Read failure",
            "Could not read the default problem list file.");

        return;
    }

    // make sure the default problem list file is not too old
    QFileInfo fileInfo(file);
    QDateTime lastModified = fileInfo.lastModified();

    if (lastModified.daysTo(QDateTime::currentDateTime())
                            > mSettings.maxDaysUntilProblemListRedownload()) {

        // the problem list file is too old, redownload it
        mUhuntApi->getProblemListAsByteArray();

        return;
    }

    QDataStream dataStream(&file);
    QByteArray data;

    dataStream >> data;

    setProblemMap(Uhunt::problemMapFromData(data));
}
