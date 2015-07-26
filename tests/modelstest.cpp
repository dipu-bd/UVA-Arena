#include <QApplication>
#include <QTableView>
#include <QSortFilterProxyModel>
#include <QLineEdit>
#include <QFrame>
#include <QVBoxLayout>

#include <iostream>

#include "models/problemstablemodel.h"

using namespace std;
using namespace uva;

int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    QFrame frame;
    QVBoxLayout verticalLayout;
    QLineEdit filterText;

    ProblemsTableModel submissions;

    // add data here

    QSortFilterProxyModel proxy;
    proxy.setSortCaseSensitivity(Qt::CaseInsensitive);
    proxy.setFilterCaseSensitivity(Qt::CaseInsensitive);
    proxy.setSourceModel(&submissions);
    // sort and filter by "Full name" column
    proxy.setFilterKeyColumn(2);

    QTableView tableView;
    tableView.setModel(&proxy);
    tableView.setSortingEnabled(true);

    verticalLayout.addWidget(&filterText);
    verticalLayout.addWidget(&tableView);
    frame.setLayout(&verticalLayout);

    QObject::connect(&filterText, &QLineEdit::textChanged, &proxy, &QSortFilterProxyModel::setFilterFixedString);

    frame.show();

    return app.exec();
}
