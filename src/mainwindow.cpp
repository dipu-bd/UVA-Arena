#include "mainwindow.h"
#include "ui_mainwindow.h"

using namespace uva;

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    this->setWindowState(Qt::WindowState::WindowMaximized);
    statusBar()->showMessage("Welcome to UVA-Arena.");
}

MainWindow::~MainWindow()
{
    delete ui;
}
