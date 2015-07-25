#include "liveeventswidget.h"
#include "ui_liveeventswidget.h"
#include "mainwindow.h"
#include "commons/colorizer.h"

using namespace uva;

class StatusModelStyle : public ModelStyle
{
public:
    StatusModelStyle(ArenaTableModel *owner = nullptr)
        : ModelStyle(owner)
    {
    }

    virtual QVariant Style(const QModelIndex &index, int role) override
    {
        switch (role)
        {
        case Qt::ForegroundRole:
            switch (index.column())
            {
            case 0: //submission id
                return QBrush(Colorizer::tan);
            case 1: //username
                return QBrush(Colorizer::goldenRod);
            case 2: //full name
                return QBrush(Colorizer::lightCoral);
            case 3: //number
                return QBrush(Colorizer::antiqueWhite);
            case 4: //title
                return QBrush(Colorizer::cyan);
            case 5: //language
                return QBrush(Colorizer::burlyWood);
            case 6: //verdict
                if (mOwner)
                    return QBrush(Colorizer::getVerdictColor((Submission::Verdict)mOwner->getModelDataAtIndex(index).toInt()));
            case 7: //runtime
                return QBrush(Colorizer::cornsilk);
            case 8: //rank                
                return QBrush(Colorizer::gold);
            case 9: //submission time                
                return QBrush(Colorizer::snow);
            default:
                return QBrush(Colorizer::white);
            }

        default:
            return ModelStyle::Style(index, role);
        }
    }
};

LiveEventsWidget::LiveEventsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::LiveEventsWidget)
{
    ui->setupUi(this);
    mStatusTableModel.setModelStyle(std::make_unique<StatusModelStyle>(&mStatusTableModel));
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
