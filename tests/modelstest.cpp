#include <QApplication>
#include <QTableView>
#include <QSortFilterProxyModel>
#include <QLineEdit>
#include <QFrame>
#include <QVBoxLayout>

#include <iostream>

#include "models/arenatablemodel.h"
#include "models/modelstyle.h"
using namespace std;

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

	ArenaTableModel submissions;
	submissions.insertColumns({ "SID", "User", "Full name", "Language", "Result", "Runtime" });
	submissions.insertRow(5, QList<QVariant>({ 5, "dipu", "Sudiptu Chandra", "c++", "accepted", 0.32f }));
	submissions.insertRow(7, QList<QVariant>({ 7, "jgcoded", "Julio Gutierrez", "java++", "accepted", 0.22f }));
	submissions.insertRow(2, QList<QVariant>({ 2, "otherguy12", "John Smith", "c", "error", 0.05f }));

    submissions.SetModelStyle(std::make_shared<MyCustomModelStyle>());

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
