#include "liveeventswidget.h"
#include "ui_liveeventswidget.h"
#include "mainwindow.h"
#include "commons/colorizer.h"

using namespace uva;

LiveEventsWidget::LiveEventsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    mUi(new Ui::LiveEventsWidget)
{
    mUi->setupUi(this);
    mLiveEventsTableModel.setFetchAllRows(false);
    mUi->statusTableView->setModel(&mLiveEventsTableModel);

    QObject::connect(&mTimer, &QTimer::timeout, this, &LiveEventsWidget::refreshLiveEvents);
}

LiveEventsWidget::~LiveEventsWidget()
{
}

void LiveEventsWidget::initialize()
{
    //connect judge status downloaded signal
    QObject::connect(mUhuntApi.get(), 
        &Uhunt::liveEventsDownloaded, 
        this, 
        &LiveEventsWidget::setLiveEventMap);

    if (mSettings.liveEventsAutoStart())
        mTimer.start(mSettings.liveEventsUpdateInterval());
}

void uva::LiveEventsWidget::setAutomaticallyUpdate(bool autoupdate)
{
    mSettings.setLiveEventsAutoStart(autoupdate);

    if (autoupdate)
        mTimer.start(mSettings.liveEventsUpdateInterval());
    else
        mTimer.stop();
}

void LiveEventsWidget::refreshLiveEvents()
{
    mUhuntApi->liveEvents(mLiveEventsTableModel.getLastSubmissionId());
}

void LiveEventsWidget::setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    mLiveEventsTableModel.setProblemMap(problemMap);
}

void LiveEventsWidget::setLiveEventMap(Uhunt::LiveEventMap statusData)
{
    mLiveEventsTableModel.setLiveEventMap(statusData);
    mUi->statusTableView->resizeColumnsToContents();
    emit newUVAArenaEvent(UVAArenaEvent::UPDATE_STATUS, "Judge Status updated.");
}
