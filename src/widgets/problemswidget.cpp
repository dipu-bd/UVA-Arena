#include "problemswidget.h"
#include "ui_problemswidget.h"

using namespace uva;

#include "mainwindow.h"

class ProblemModelStyle : public ModelStyle
{
public:
    virtual QVariant Style(const QModelIndex &index, int role) override
    {
        switch (role)
        {
        case Qt::ForegroundRole:
            switch (index.column())
            {
            case 0:
            case 1:
            case 2:
                return QBrush(Qt::red);

            case 3:
                return QBrush(Qt::cyan);

            default:
                return QBrush(Qt::magenta);
            }

        default:
            return ModelStyle::Style(index, role);
        }
    }
};

ProblemsWidget::ProblemsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    ui(new Ui::ProblemsWidget)
{
    ui->setupUi(this);
}

ProblemsWidget::~ProblemsWidget()
{
    delete ui;
}

void ProblemsWidget::initialize()
{
    mProblemsTableModel.setUhuntProblemMap(mainWindow()->getProblemMap());
    mProblemsTableModel.setModelStyle(std::make_unique<ProblemModelStyle>());
    mProblemsTableModel.setMaxRowsToLoad(mSettings.maxProblemsTableRowsToLoad());
    ui->problemsTableView->setModel(&mProblemsTableModel);
}

void ProblemsWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent)
    {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}
