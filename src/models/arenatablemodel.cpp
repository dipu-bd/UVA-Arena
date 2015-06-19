#include "arenatablemodel.h"

ArenaTableModel::ArenaTableModel()
{

}

void ArenaTableModel::SetColumnNames(QStringList columnNames)
{
    beginInsertColumns(QModelIndex(), 0, columnNames.size());
    mColumnNames = columnNames;
    endInsertColumns();
}

void ArenaTableModel::AddData(QList<QVariant> data)
{
    beginInsertRows(QModelIndex(), mData.size(), mData.size());
    QVariant key = data.takeFirst();
    mData[key] = data;
    endInsertRows();
}

void ArenaTableModel::RemoveData(QVariant key)
{
    ModelMap::const_iterator it = mData.find(key);

    if(it != mData.end())
    {
        int index = mData.values().indexOf(*it);
        beginRemoveRows(QModelIndex(), index, index);
        mData.remove(key);
        endRemoveRows();
    }
}

int ArenaTableModel::columnCount(const QModelIndex & /* parent */) const
{
    return mColumnNames.count();
}

int ArenaTableModel::rowCount(const QModelIndex & /* parent */) const
{
    return mData.count();
}

QVariant ArenaTableModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role != Qt::DisplayRole)
        return QVariant();

    if(orientation == Qt::Vertical)
        return QString("%1").arg(section);

    if(orientation == Qt::Horizontal)
        return mColumnNames[section];

    return QVariant();
}

QVariant ArenaTableModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if (index.row() >= mData.size())
        return QVariant();

    if(role != Qt::DisplayRole)
        return QVariant();

    if(index.column() == 0)
        return mData.keys().at(index.row());
    else
        return mData.values().at(index.row());
}


