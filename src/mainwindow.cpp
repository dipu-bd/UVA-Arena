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

const QString UVAProblemHTMLUrl = "https://uva.onlinejudge.org/external/%1/%2.html"; // 1 = container, 2 = problem number
const QString UVAProblemPDFUrl = "https://uva.onlinejudge.org/external/%1/%2.pdf"; // 1 = container, 2 = problem number

const QString DefaultWebViewPageHTML =
"<html>"
"<head>"
"<style>"
"h1 { color: #FFF; text-align: center; font-family: Lora, Times New Roman, serif; }"
"</style>"
"</head>"
"<body>"
"<h1>Double click a problem from the problem list to view it here.<h1>"
"</body>"
"</html>";

MainWindow::MainWindow(std::shared_ptr<QNetworkAccessManager> networkManager, QWidget *parent) :
    QMainWindow(parent),
    mNetworkManager(networkManager),
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

void MainWindow::onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent arenaEvent, QVariant metaData)
{
    typedef UVAArenaWidget::UVAArenaEvent UVAArenaEvent;

    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        statusBar()->showMessage(metaData.toString(), 2000);
        break;

    case UVAArenaEvent::SHOW_PROBLEM:
        showProblem(metaData.toInt());
        break;

    case UVAArenaEvent::SHOW_PROBLEM_DESCRIPTION:
        break;

    case UVAArenaEvent::SHOW_CODE:
        break;

    case UVAArenaEvent::SHOW_STATUS:
        break;

    case UVAArenaEvent::SHOW_PROFILE:
        break;

    default:
        break;
    }
}

void MainWindow::onProblemListByteArrayDownloaded(QByteArray data)
{
    // set the file to save to
    QDir saveDirectory(
        QStandardPaths::writableLocation(QStandardPaths::AppDataLocation)
    );

    if (!saveDirectory.exists())
        QDir().mkpath(".");

    QString fileName = saveDirectory.filePath(DefaultProblemListFileName);
    QFile file(fileName);

    if (!file.open(QIODevice::WriteOnly)) {

        // couldn't open the file
        QMessageBox::critical(this, "Write failure",
            "Could not write to the default problem list file:\n"
            + fileName);

        return;
    }

    // write the problem list data to the default problems list file
    QDataStream dataStream(&file);
    dataStream << data;

    setProblemMap(Uhunt::problemMapFromJson(data));
}

void MainWindow::setProblemMap(Uhunt::ProblemMap problemMap)
{
    mProblems.reset(new Uhunt::ProblemMap);
    *mProblems = std::move(problemMap);

    Uhunt::ProblemMap::const_iterator it = mProblems->begin();

    ui->problemsWidget->setProblemMap(getProblemMap());
    ui->liveEventsWidget->setProblemMap(getProblemMap());
    statusBar()->showMessage("Problem list loaded", 2000);
}

void MainWindow::initialize()
{
    initializeData();
    initializeWidgets();
}

void MainWindow::initializeData()
{
    //connect problem list byte downloaded array signal
    QObject::connect(mUhuntApi.get(), &Uhunt::allProblemsDownloaded,
        this, &MainWindow::onProblemListByteArrayDownloaded);

    // check if problem list is already downloaded
    QString result = QStandardPaths::locate(QStandardPaths::AppDataLocation,
        DefaultProblemListFileName);

    if (result.isEmpty()) {
        // download the problem list and save it
        mUhuntApi->allOnlineJudgeProblems();
    } else {
        // file found, load the data
        loadProblemListFromFile(result);
    }
}

void MainWindow::initializeWidgets()
{
    ui->pdfViewer->setSaveOnDownload(mSettings.savePDFDocumentsOnDownload());

    // Initialize all UVAArenaWidgets and connect them
    mUVAArenaWidgets.push_back(ui->problemsWidget);
    mUVAArenaWidgets.push_back(ui->codesWidget);
    mUVAArenaWidgets.push_back(ui->liveEventsWidget);
    mUVAArenaWidgets.push_back(ui->profilesWidget);

    for (UVAArenaWidget* widget : mUVAArenaWidgets) {

        widget->setNetworkManager(mNetworkManager);
        widget->setUhuntApi(mUhuntApi);

        QObject::connect(widget, &UVAArenaWidget::newUVAArenaEvent,
            this, &MainWindow::onUVAArenaEvent);

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
        mUhuntApi->allOnlineJudgeProblems();

        return;
    }

    QDataStream dataStream(&file);
    QByteArray data;

    dataStream >> data;

    setProblemMap(Uhunt::problemMapFromJson(data));
}

void MainWindow::loadPDFByProblemNumber(int problemNumber)
{
    QDir saveDirectory(
        QStandardPaths::writableLocation(QStandardPaths::AppDataLocation)
        );

    if (!saveDirectory.exists())
        saveDirectory.mkpath(".");

    if (!saveDirectory.cd("problems")) {
        saveDirectory.mkdir("problems");
        saveDirectory.cd("problems");
    }

    QString pdfFileName =
        saveDirectory.filePath(tr("%1.pdf").arg(problemNumber));

    if (QFile::exists(pdfFileName)) {

        ui->tabWidget->setCurrentWidget(ui->codesTab);
        ui->pdfViewer->loadDocument(pdfFileName);

    } else {
        ui->pdfViewer->downloadPDF(UVAProblemPDFUrl.arg(problemNumber / 100)
                                                   .arg(problemNumber)
                                                   , pdfFileName);
    }
}

void MainWindow::showProblem(int problemNumber)
{
    typedef UVAArenaSettings::ProblemFormat ProblemFormat;

    if (mSettings.problemFormatPreference() == ProblemFormat::HTML) {

    } else { // PDF

        ui->tabWidget->setCurrentWidget(ui->codesWidget);
        loadPDFByProblemNumber(problemNumber);
    }
}
