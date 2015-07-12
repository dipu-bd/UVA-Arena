#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QStandardPaths>

using namespace uva;

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

void MainWindow::initialize()
{
    // Initialize all UVAArenaWidgets and connect them

    mUVAArenaWidgets.push_back(ui->problemsWidget); 
    mUVAArenaWidgets.push_back(ui->codesWidget);
    mUVAArenaWidgets.push_back(ui->judgeStatusWidget);
    mUVAArenaWidgets.push_back(ui->profilesWidget);

    for (UVAArenaWidget* widget : mUVAArenaWidgets) {

        widget->setNetworkManager(mNetworkManager);
        widget->setUhuntApi(mUhuntApi);

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
