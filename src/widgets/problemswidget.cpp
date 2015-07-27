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

const QString categoryIndexUrl =
"https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/INDEX";

const QString DefaultCategoryIndexFileName = "INDEX";


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
    // Get category data

    QDir categoriesDirectory(
        QStandardPaths::writableLocation(QStandardPaths::AppDataLocation)
    );

    QString result = QStandardPaths::locate(QStandardPaths::AppDataLocation,
        DefaultCategoryIndexFileName);

    if (result.isEmpty()) {
        // #TODO downloadCategoryIndex();
    } else {
        // #TODO loadCategoryIndexFromFile(result);
    }
}

void ProblemsWidget::setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap)
{
    mProblemsTableModel.setUhuntProblemMap(problemsMap);
    mUi->problemsTableView->resizeColumnsToContents();
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

void ProblemsWidget::downloadCategoryIndex()
{
    throw std::logic_error("The method or operation is not implemented.");
}

void ProblemsWidget::loadCategoryIndexFromFile(QString result)
{
    throw std::logic_error("The method or operation is not implemented.");
}
