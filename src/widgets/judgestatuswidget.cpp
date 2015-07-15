#include "judgestatuswidget.h"
#include "ui_judgestatuswidget.h"
#include "mainwindow.h"

using namespace uva;


class StatusModelStyle : public ModelStyle
{
public:
    virtual QVariant Style(const QModelIndex &index, int role) override
    {
        switch (role)
        {
        case Qt::ForegroundRole:
            switch (index.column())
            {
            case 0: //submission id
            case 1: //username
            case 2: //full name
            case 3: //number
            case 4: //title
            case 5: //language
            case 6: //verdict
            case 7: //runtime
            case 8: //rank
            case 9: //submission time
            default:
                return QBrush(Qt::blue);
            }

        default:
            return ModelStyle::Style(index, role);
        }
    }
};

JudgeStatusWidget::JudgeStatusWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::JudgeStatusWidget)
{
    ui->setupUi(this);
    mStatusTableModel.setModelStyle(std::make_unique<StatusModelStyle>());
    ui->statusTableView->setModel(&mStatusTableModel);
}

JudgeStatusWidget::~JudgeStatusWidget()
{
    delete ui;
}

void JudgeStatusWidget::initialize()
{
}

void JudgeStatusWidget::setStatusData(std::shared_ptr<QList<JudgeStatus> > statusData,
                                      std::shared_ptr<Uhunt::ProblemMap> problemMap)
{
    mStatusTableModel.setStatusData(statusData, problemMap);
}

void JudgeStatusWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}
