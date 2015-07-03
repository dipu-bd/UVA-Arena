#include <QApplication>
#include <QTableView>
#include <QSortFilterProxyModel>
#include <QLineEdit>
#include <QFrame>
#include <QVBoxLayout>

#include <iostream>

#include "models/arenatablemodel.h"
using namespace std;

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

	QSortFilterProxyModel proxy;
	proxy.setSourceModel(&submissions);
	// filter by "Full name" column
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
