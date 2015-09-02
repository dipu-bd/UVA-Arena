#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "settingsdialog.h"

#include <QFile>
#include <QFileInfo>
#include <QMessageBox>
#include <QDataStream>
#include <QDateTime>
#include <QDir>
#include <QStandardPaths>
#include <QUrl>
#include <QPushButton>

using namespace uva;

const QString DefaultProblemListFileName = "problemlist.json";

const QString UVAProblemHTMLUrl = "https://uva.onlinejudge.org/external/%1/%2.html"; // 1 = container, 2 = problem number
const QString UVAProblemPDFUrl = "https://uva.onlinejudge.org/external/%1/%2.pdf"; // 1 = container, 2 = problem number

MainWindow::MainWindow(std::shared_ptr<QNetworkAccessManager> networkManager, QWidget *parent) :
    QMainWindow(parent),
    mNetworkManager(networkManager),
    mUi(new Ui::MainWindow)
{
    mUi->setupUi(this);

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

    default:
        break;
    }
}

void MainWindow::onAllProblemsDownloaded(QByteArray data)
{
    // set the file to save to
    QDir saveDirectory(
        QStandardPaths::writableLocation(QStandardPaths::AppDataLocation)
    );

    if (!saveDirectory.exists())
        saveDirectory.mkpath(saveDirectory.path());

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

    mUi->problemsWidget->setProblemMap(mProblems);
    mUi->liveEventsWidget->setProblemMap(mProblems);
    mUi->profilesWidget->setProblemMap(mProblems);
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
        this, &MainWindow::onAllProblemsDownloaded);

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
    if (mSettings.userId() != -1)
        setWindowTitle("UVA Arena - " + mSettings.userName());

    QPushButton *settingsButton = new QPushButton(mUi->tabWidget);
    settingsButton->setText("Settings");
    QObject::connect(settingsButton, &QPushButton::clicked, this, &MainWindow::openSettings);
    mUi->tabWidget->setCornerWidget(settingsButton);

    mUi->pdfViewer->setSaveOnDownload(mSettings.savePDFDocumentsOnDownload());
    mUi->pdfViewer->setNetworkManager(mNetworkManager);
    // Initialize all UVAArenaWidgets and connect them
    mUVAArenaWidgets.push_back(mUi->problemsWidget);
    mUVAArenaWidgets.push_back(mUi->editorWidget);
    mUVAArenaWidgets.push_back(mUi->liveEventsWidget);
    mUVAArenaWidgets.push_back(mUi->profilesWidget);

    for (UVAArenaWidget* widget : mUVAArenaWidgets) {

        widget->setNetworkManager(mNetworkManager);
        widget->setUhuntApi(mUhuntApi);

        // Allow each widget to communicate with the MainWindow
        QObject::connect(widget, &UVAArenaWidget::newUVAArenaEvent,
            this, &MainWindow::onUVAArenaEvent);

        // Allow MainWindow to communicate with each widget
        QObject::connect(this, &MainWindow::newUVAArenaEvent,
            widget, &UVAArenaWidget::onUVAArenaEvent);

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
                            > mSettings.problemsUpdateInterval()) {

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

        mUi->tabWidget->setCurrentWidget(mUi->solveTab);
        mUi->pdfViewer->loadDocument(pdfFileName);

    } else {
        mUi->pdfViewer->downloadPDF(UVAProblemPDFUrl.arg(problemNumber / 100)
                                                   .arg(problemNumber)
                                                   , pdfFileName);
    }
}
#include <QDesktopServices>
void MainWindow::showProblem(int problemNumber)
{
    typedef UVAArenaSettings::ProblemFormat ProblemFormat;

    if (mSettings.problemFormatPreference() == ProblemFormat::HTML) {

        QDesktopServices::openUrl(QUrl(UVAProblemHTMLUrl.arg(problemNumber / 100).arg(problemNumber)));

    } else { // PDF

        mUi->tabWidget->setCurrentWidget(mUi->solveTab);
        loadPDFByProblemNumber(problemNumber);
    }
}

void MainWindow::openSettings()
{
    SettingsDialog dialog;
    QString oldUserName = mSettings.userName();
    if (dialog.exec() == QDialog::Accepted) {
        // emit new uva arena event
        mUi->pdfViewer->setSaveOnDownload(mSettings.savePDFDocumentsOnDownload());
        QString newUserName = mSettings.userName();
        if (oldUserName != newUserName) {// new user, get the id

            QObject::connect(mUhuntApi.get(), &Uhunt::userIDDownloaded,
                [this, newUserName](QString userName, int userId) {
                if (userName == newUserName) { // Make sure we're talking about the same person
                    this->mSettings.setUserId(userId);
                    setWindowTitle("UVA Arena - " + mSettings.userName());
                }

                emit newUVAArenaEvent(UVAArenaWidget::UVAArenaEvent::UPDATE_SETTINGS, QVariant());
            });

            mUhuntApi->userIDFromUserName(mSettings.userName());
        }
    }
}
