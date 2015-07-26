#include "liveeventswidget.h"
#include "ui_liveeventswidget.h"
#include "mainwindow.h"
#include "commons/colorizer.h"

using namespace uva;

LiveEventsWidget::LiveEventsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::LiveEventsWidget) // #TODO make all generated ui file code unique_ptrs
{
    ui->setupUi(this);
    ui->statusTableView->setModel(&mStatusTableModel);

    mTimer = new QTimer(this);
    QObject::connect(mTimer, &QTimer::timeout, this, &LiveEventsWidget::refreshJudgeStatus);
}

LiveEventsWidget::~LiveEventsWidget()
{
    delete ui;
}

void LiveEventsWidget::initialize()
{
    //connect judge status downloaded signal
    QObject::connect(mUhuntApi.get(), 
        &Uhunt::liveEventsDownloaded, 
        this, 
        &LiveEventsWidget::setStatusData);

    mTimer->start(mSettings.getJudgeStatusUpdateInterval());
}

void LiveEventsWidget::refreshJudgeStatus()
{
    mUhuntApi->liveEvents(mStatusTableModel.getLastSubmissionId());
}

void LiveEventsWidget::setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    mStatusTableModel.setProblemMap(problemMap);
}

void LiveEventsWidget::setStatusData(Uhunt::LiveEventMap statusData)
{
    mStatusTableModel.setStatusData(statusData);
    emit newUVAArenaEvent(UVAArenaEvent::UPDATE_STATUS, "Judge Status updated.");
}

void LiveEventsWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}
