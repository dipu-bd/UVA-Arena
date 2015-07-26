#include <QNetworkRequest>
#include <QNetworkReply>
#include <QStandardPaths>
#include <QDir>
#include <QFile>
#include <QMessageBox>

#include "problemswidget.h"
#include "ui_problemswidget.h"
#include "mainwindow.h"

using namespace uva;

ProblemsWidget::ProblemsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    mUi(new Ui::ProblemsWidget)
{
    mUi->setupUi(this);
    mProblemsTableModel.setMaxRowsToFetch(mSettings.maxProblemsTableRowsToFetch());
    mProblemsFilterProxyModel.setSortCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setFilterCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setSourceModel(&mProblemsTableModel);
    mProblemsFilterProxyModel.setFilterKeyColumn(1); // Problem title column
    QObject::connect(mUi->searchProblemsLineEdit, &QLineEdit::textChanged,
        &mProblemsFilterProxyModel, &QSortFilterProxyModel::setFilterFixedString);

    mUi->problemsTableView->setModel(&mProblemsFilterProxyModel);
}

ProblemsWidget::~ProblemsWidget()
{
}

void ProblemsWidget::initialize()
{
}

void ProblemsWidget::setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap)
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

void ProblemsWidget::problemsTableDoubleClicked(QModelIndex index)
{
    index = mProblemsFilterProxyModel.mapToSource(index);
    int selectedProblemNumber = index.sibling(index.row(), 0).data().toInt();
    emit newUVAArenaEvent(UVAArenaEvent::SHOW_PROBLEM, selectedProblemNumber);
}
