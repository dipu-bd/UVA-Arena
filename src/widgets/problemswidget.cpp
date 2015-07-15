#include "problemswidget.h"
#include "ui_problemswidget.h"

using namespace uva;

#include "mainwindow.h"

const QString UVAProblemUrl = "https://uva.onlinejudge.org/external/%1/%2.html"; // 1 = container, 2 = problem number

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
    mProblemsTableModel.setModelStyle(std::make_unique<ProblemModelStyle>());
    mProblemsTableModel.setMaxRowsToFetch(mSettings.maxProblemsTableRowsToFetch());
    mProblemsFilterProxyModel.setSortCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setFilterCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setSourceModel(&mProblemsTableModel);
    mProblemsFilterProxyModel.setFilterKeyColumn(1); // Problem title column
    QObject::connect(ui->searchProblemsLineEdit, &QLineEdit::textChanged,
        &mProblemsFilterProxyModel, &QSortFilterProxyModel::setFilterFixedString);

    ui->problemsTableView->setModel(&mProblemsFilterProxyModel);
}

ProblemsWidget::~ProblemsWidget()
{
    delete ui;
}

void ProblemsWidget::initialize()
{
}

void ProblemsWidget::setProblemsMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap)
{
    mProblemsTableModel.setUhuntProblemMap(problemsMap);
}

void ProblemsWidget::onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData)
{
    switch (arenaEvent) {
    case UVAArenaEvent::UPDATE_STATUS:
        break;

    default:
        break;
    }
}

void ProblemsWidget::setFilterProblemsBy(QString columnName)
{
    if (columnName == "Problem Number")
        mProblemsFilterProxyModel.setFilterKeyColumn(0);
    else if (columnName == "Problem Title")
        mProblemsFilterProxyModel.setFilterKeyColumn(1);
}

void ProblemsWidget::showNewProblem(QModelIndex index)
{
    index = mProblemsFilterProxyModel.mapToSource(index);

    int selectedProblemNumber = index.sibling(index.row(), 0).data().toInt();

    ui->webView->setUrl(QUrl(UVAProblemUrl.arg(selectedProblemNumber / 100).arg(selectedProblemNumber)));
    ui->problemsWidgetToolbox->setCurrentWidget(ui->problemViewPage);
}
