#include <QApplication>
#include <QTableView>
#include <QSortFilterProxyModel>
#include <QLineEdit>
#include <QFrame>
#include <QVBoxLayout>

#include <iostream>

#include "models/problemstablemodel.h"
#include "models/modelstyle.h"

using namespace std;
using namespace uva;

class MyCustomModelStyle : public ModelStyle
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


int main(int argc, char* argv[])
{
    QApplication app(argc, argv);

    QFrame frame;
    QVBoxLayout verticalLayout;
    QLineEdit filterText;

    ProblemsTableModel submissions;

    // add data here

    submissions.setModelStyle(std::make_unique<MyCustomModelStyle>());

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
