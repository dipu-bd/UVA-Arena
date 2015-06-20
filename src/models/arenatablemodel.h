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

    void SetColumnNames(QStringList columnNames);

    void AddData(QList<QVariant> data);
    void RemoveData(QVariant key);

    int columnCount(const QModelIndex & parent = QModelIndex()) const override;
    int rowCount(const QModelIndex &parent) const override;


    QVariant headerData(int section, Qt::Orientation orientation, int role) const override;
    QVariant data(const QModelIndex &index, int role) const override;

private:

    ModelMap mData;

    QStringList mColumnNames;
    int mNumRows;
    int mNumCols;

};

#endif // ARENATABLEMODEL_H
