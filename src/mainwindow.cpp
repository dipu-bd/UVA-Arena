#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QFile>
#include <QFileInfo>
#include <QMessageBox>
#include <QDataStream>
#include <QDateTime>
#include <QDir>

using namespace uva;

const QString DefaultProblemListFileName = "problemlist.json";

MainWindow::MainWindow(std::shared_ptr<QNetworkAccessManager> networkManager, QWidget *parent) :
    QMainWindow(parent),
    mNetworkManager(networkManager),
    mMaxDaysUntilProblemListRedownload(1),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    this->setWindowState(Qt::WindowState::WindowMaximized);
    statusBar()->showMessage("Welcome to UVA-Arena.");

    mUhuntApi = std::make_shared<Uhunt>(mNetworkManager);

    QObject::connect(mUhuntApi.get(), &Uhunt::problemListByteArrayDownloaded,
        this, &MainWindow::onProblemListByteArrayDownloaded);

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

void MainWindow::initialize()
{
    // Initialize problem list

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

void MainWindow::loadProblemListFromFile(QString fileName)
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

    // TODO: do something with the problem list
    UhuntDatabase::setProblemList(Uhunt::problemListFromData(data));
    statusBar()->showMessage("Problem list loaded.");
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
            + saveDirectory + DefaultProblemListFileName);

        return;
    }

    // write the problem list data
    QDataStream dataStream(&file);
    dataStream << data;

    // TODO: do something with the problem list
    UhuntDatabase::setProblemList(Uhunt::problemListFromData(data));
    statusBar()->showMessage("Problem list loaded.");
}
