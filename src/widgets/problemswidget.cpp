#include <QNetworkRequest>
#include <QNetworkReply>
#include <QStandardPaths>
#include <QDir>
#include <QFile>
#include <QFileInfo>
#include <QMessageBox>
#include <QDateTime>

#include "problemswidget.h"
#include "ui_problemswidget.h"
#include "mainwindow.h"

using namespace uva;

const QString CategoryIndexUrl =
"https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/INDEX";

const QString CategoryFileUrl =
"https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/%1";

const QString DefaultCategoryIndexFileName = "INDEX";


ProblemsWidget::ProblemsWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    mUi(new Ui::ProblemsWidget)
{
    mUi->setupUi(this);
    mProblemsTableModel.setCategoryRoot(mCategoryTreeModel.categoryRoot());
    mProblemsTableModel.setMaxRowsToFetch(mSettings.maxProblemsTableRowsToFetch());
    mProblemsFilterProxyModel.setSortCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setFilterCaseSensitivity(Qt::CaseInsensitive);
    mProblemsFilterProxyModel.setSourceModel(&mProblemsTableModel);
    QObject::connect(mUi->searchProblemsLineEdit, &QLineEdit::textChanged,
        &mProblemsFilterProxyModel, &QSortFilterProxyModel::setFilterFixedString);

    mUi->problemsTableView->setModel(&mProblemsFilterProxyModel);

    mCategoryFilterProxyModel.setSourceModel(&mCategoryTreeModel);
    mUi->categoryTreeView->setModel(&mCategoryFilterProxyModel);
}

ProblemsWidget::~ProblemsWidget()
{
}

void ProblemsWidget::initialize()
{
    // Get category data
    if(categoryIndexFileExists())
        loadCategoryIndexFromFile();
    else
        downloadCategoryIndex();
}

void ProblemsWidget::setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap)
{
    mProblemsTableModel.setUhuntProblemMap(problemsMap);
    mUi->problemsTableView->sortByColumn(0, Qt::AscendingOrder);
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

void ProblemsWidget::problemsTableDoubleClicked(QModelIndex index)
{
    index = mProblemsFilterProxyModel.mapToSource(index);
    int selectedProblemNumber = index.sibling(index.row(), 0).data().toInt();
    emit newUVAArenaEvent(UVAArenaEvent::SHOW_PROBLEM, selectedProblemNumber);
}

void ProblemsWidget::downloadCategoryIndex()
{
    if (!mNetworkManager)
        return;

    QNetworkRequest request;
    request.setUrl(CategoryIndexUrl);
    QNetworkReply *reply = mNetworkManager->get(request);

    QObject::connect(reply, &QNetworkReply::finished, [reply, this](){

        if (reply->error() == QNetworkReply::NoError)
            onCategoryIndexDownloaded(reply->readAll());

    });
}

void ProblemsWidget::categoryTreeViewClicked(QModelIndex index)
{
    Category *category = static_cast<Category*>(
        mCategoryFilterProxyModel.mapToSource(index).internalPointer());

    mProblemsFilterProxyModel.setProblemFilter(category->Problems.keys());
}

void uva::ProblemsWidget::loadCategoryIndexFromFile()
{
    QFile categoryIndexFile(QStandardPaths::locate(QStandardPaths::AppDataLocation,
        DefaultCategoryIndexFileName));

    QFileInfo fileInfo(categoryIndexFile);
    if (fileInfo.lastModified().daysTo(QDateTime::currentDateTime())
                        > mSettings.maxDaysUntilCategoryIndexRedownload()) {
        // the category index file is too old, redownload it
        downloadCategoryIndex();

        return;
    }

    if (!categoryIndexFile.open(QIODevice::ReadOnly)) {
        QMessageBox::critical(this, "Read failure",
            "Could not open the category index file for reading.");

        return;
    }

    QByteArray categoryIndexFileData = categoryIndexFile.readAll();

    for (auto category : categoryIndexJsonToList(categoryIndexFileData)) {
        // load the file by name category.first,
        // and then call newCategoryLoaded(Category::fromJson(categoryJson));

        // assume the file exists at this point
        QDir categoryFileDirectory(
            QStandardPaths::writableLocation(QStandardPaths::AppDataLocation)
            );

        QFile categoryFile(categoryFileDirectory.filePath(category.first));

        if (!categoryFile.open(QIODevice::ReadOnly))
            continue;

        newCategoryLoaded(Category::fromJson(categoryFile.readAll()));
        categoryFile.close();

    }

}

bool ProblemsWidget::categoryIndexFileExists()
{
    QString result = QStandardPaths::locate(QStandardPaths::AppDataLocation,
        DefaultCategoryIndexFileName);

    return !result.isEmpty();
}

void uva::ProblemsWidget::onCategoryIndexDownloaded(QByteArray data)
{
    // when a new category index is downloaded, two things should happen:
    // see if the file already exists. If it does:
    // compare the category files and download changes
    // If there is no difference in category files, do nothing
    // save to file and parse

    QDir dir(QStandardPaths::writableLocation(QStandardPaths::AppDataLocation));
    QFile categoryIndexFile(dir.filePath(DefaultCategoryIndexFileName));

    if (categoryIndexFileExists()) { // compare the old with the new

        if (!categoryIndexFile.open(QIODevice::ReadWrite)) {
            QMessageBox::critical(this, "Read failure",
                "Could not open the category index file for reading.");

            return;
        }

        // get a copy of the old data
        QByteArray categoryIndexJson = categoryIndexFile.readAll();

        categoryIndexFile.resize(0); // clear contents
        categoryIndexFile.write(data); // replace with downloaded data
        categoryIndexFile.close();

        QList<QPair<QString, int> > oldIndex = 
            categoryIndexJsonToList(categoryIndexJson);

        // save the new data

        QList<QPair<QString, int> > newIndex = categoryIndexJsonToList(data);

        // compare the old with the new
        while (!oldIndex.empty()) {
            QPair<QString, int> oldCategory = oldIndex.takeFirst();

            int i = newIndex.indexOf(oldCategory);
            if (i != -1) { // compare versions
                QPair<QString, int> newCategory = newIndex.at(i);

                // Remove from newIndex if we have the latest category
                if (newCategory.second <= oldCategory.second)
                    newIndex.removeAt(i);
            }
        }

        // newIndex now contains all new/updated category files to download
        downloadCategories(newIndex);

    } else { // first download
    
        if (!categoryIndexFile.open(QIODevice::WriteOnly)) {
            QMessageBox::critical(this, "Write failure",
                "Could not open the category index file for writing.");

            return;
        }
    
        // save it
        categoryIndexFile.write(data);
        categoryIndexFile.close();

        downloadCategories(categoryIndexJsonToList(data));

    }
}

QList<QPair<QString, int> > uva::ProblemsWidget::categoryIndexJsonToList(QByteArray data)
{
    QJsonDocument document = QJsonDocument::fromJson(data);
    QList<QPair<QString, int> > categories;

    if (document.isArray()) {
        QJsonArray jsonArray = document.array();

        QJsonArray::const_iterator it = jsonArray.begin();
        while (it != jsonArray.end()) {
            if (it->isObject()) {
                QJsonObject categoryJson = it->toObject();
                categories.push_back(
                    { categoryJson["file"].toString()
                    , categoryJson["ver"].toInt() });
            }
            it++;
        }
    }

    return categories;
}

void uva::ProblemsWidget::newCategoryLoaded(std::shared_ptr<Category> category)
{
    if (!category)
        return;

    mCategoryTreeModel.addCategory(category);
    // resize the first column
    mUi->categoryTreeView->resizeColumnToContents(0);
    mCategoryFilterProxyModel.sort(0, Qt::AscendingOrder);
}

void uva::ProblemsWidget::saveCategoryFile(const QByteArray &categoryJson, QString fileName)
{
    QDir categoryDirectory(QStandardPaths::writableLocation(QStandardPaths::AppDataLocation));
    QFile categoryFile(categoryDirectory.filePath(fileName));

    if (!categoryFile.open(QIODevice::WriteOnly | QIODevice::Truncate))
        return;

    categoryFile.write(categoryJson);
    categoryFile.flush();
    categoryFile.close();
}

void uva::ProblemsWidget::downloadCategories(QList<QPair<QString, int> > categories)
{
    if (!mNetworkManager)
        return;

    for (auto category : categories) {

        QNetworkRequest request;
        request.setUrl(CategoryFileUrl.arg(category.first));
        QNetworkReply *reply = mNetworkManager->get(request);

        QObject::connect(reply, &QNetworkReply::finished, [reply, category, this](){

            if (reply->error() == QNetworkReply::NoError) {
                // save it too
                QByteArray categoryJson = reply->readAll();
                newCategoryLoaded(Category::fromJson(categoryJson));
                saveCategoryFile(categoryJson, category.first);
            }


        });
    }
}
