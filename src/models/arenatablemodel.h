#ifndef ARENATABLEMODEL_H
#define ARENATABLEMODEL_H

#include "src_global.h"
#include <QAbstractTableModel>
#include <QStringList>
#include <QMap>
#include <QList>
#include <QVariant>

class SRCSHARED_EXPORT ArenaTableModel : public QAbstractTableModel
{
public:
    typedef QMap<QVariant, QList<QVariant> > ModelMap;
    ArenaTableModel();

	virtual bool insertRow(QVariant key, QList<QVariant> data);
	virtual bool removeRow(QVariant key);
	virtual bool insertColumns(QStringList columnNames);

    virtual int columnCount(const QModelIndex & parent = QModelIndex()) const override;
    virtual int rowCount(const QModelIndex &parent) const override;

    QVariant headerData(int section, Qt::Orientation orientation, int role) const override;
    QVariant data(const QModelIndex &index, int role) const override;

private:

    ModelMap mData;
    QStringList mColumnNames;
};

#endif // ARENATABLEMODEL_H
