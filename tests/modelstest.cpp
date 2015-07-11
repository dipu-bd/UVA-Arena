#include <QApplication>
#include <QTableView>
#include <QSortFilterProxyModel>
#include <QLineEdit>
#include <QFrame>
#include <QVBoxLayout>
#include <QStandardItemModel>
#include <iostream>

using namespace std;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    QFrame frame;
    QVBoxLayout verticalLayout;
    QLineEdit filterText;

    QStringList headerLabels = { "SID", "User", "Full name", "Language", "Result", "Runtime" };

    QStandardItemModel model;
    model.setHorizontalHeaderLabels(headerLabels);

    QList<QList<QVariant> > sampleUserData = {
        { 7, "jgcoded", "Julio Gutierrez", "java", "accepted", 3.0001f },
        { 5, "dipu", "Sudipto Chandra", "c++", "accepted", 0.320f },
        { 2, "otherguy12", "John Smith", "c", "error", 0.3f },
        { 2532234, "otherguy32", "Unknown Entity", "pascal", "time limit exceeded", 3.00001f },
        { 1, "bob", "Bob Smith", "c++", "accepted", 0.320f },
        { 6542365, "human4", "Vanity Bug", "c++11", "wrong answer", 0.01f },
    };

    for (int i = 0; i < sampleUserData.count(); ++i) {
        for (int j = 0; j < sampleUserData[i].count(); ++j) {
            
            QStandardItem *item = new QStandardItem;
            item->setData(sampleUserData[i][j], Qt::DisplayRole);
            item->setForeground(QBrush(Qt::magenta));
            model.setItem(i, j, item);
        }
    }

    QSortFilterProxyModel filterProxyModel;
    filterProxyModel.setSourceModel(&model);
    filterProxyModel.setSortCaseSensitivity(Qt::CaseSensitive);
    filterProxyModel.setFilterCaseSensitivity(Qt::CaseInsensitive);
    // sort and filter by full name column
    filterProxyModel.setFilterKeyColumn(2);

    QTableView tableView;
    tableView.setModel(&filterProxyModel);
    tableView.setSortingEnabled(true);
    // don't allow users to edit the items
    tableView.setEditTriggers(QTableView::NoEditTriggers);

    verticalLayout.addWidget(&filterText);
    verticalLayout.addWidget(&tableView);
    frame.setLayout(&verticalLayout);

    QObject::connect(&filterText, &QLineEdit::textChanged, &filterProxyModel, &QSortFilterProxyModel::setFilterFixedString);

    frame.show();

    return app.exec();
}
