#include "arenatablemodel.h"

using namespace uva;

ArenaTableModel::ArenaTableModel()
    : mRowsFetchedCount(0),
      mMaxRowsToFetch(50),
      mFetchAllRows(true)
{
}

bool ArenaTableModel::insertColumns(QStringList columnNames)
{
    beginInsertColumns(QModelIndex(), 0, columnNames.count());
    mColumnNames = columnNames;
    endInsertColumns();

    return true;
}

int ArenaTableModel::columnCount(const QModelIndex & /* parent */) const
{
    return mColumnNames.count();
}

int ArenaTableModel::rowCount(const QModelIndex & /* parent */) const
{
    if (mFetchAllRows)
        return getDataCount();

    return mRowsFetchedCount;
}

QVariant ArenaTableModel::headerData(int section, Qt::Orientation orientation, int role) const
{
    if(role != Qt::DisplayRole)
        return QVariant();

    if(orientation == Qt::Vertical)
        return QString("%1").arg(section + 1);

    if(orientation == Qt::Horizontal)
        return mColumnNames[section];

    return QVariant();
}

QVariant ArenaTableModel::data(const QModelIndex &index, int role) const
{
    if (!index.isValid())
        return QVariant();

    if (index.row() >= getDataCount() || index.row() < 0)
        return QVariant();

    if (index.column() >= mColumnNames.size() || index.column() < 0)
        return QVariant();

    if (role != Qt::DisplayRole)
        return style(index, role);

    return getDataAtIndex(index);
}

void ArenaTableModel::setMaxRowsToFetch(int maxRowsToFetch)
{
    mMaxRowsToFetch = maxRowsToFetch;
}

void ArenaTableModel::setFetchAllRows(bool shouldLoadAll)
{
    mFetchAllRows = shouldLoadAll;
}

bool ArenaTableModel::canFetchMore(const QModelIndex &parent) const
{
    if (mFetchAllRows)
        return QAbstractTableModel::canFetchMore(parent);

    if (mRowsFetchedCount < getDataCount())
        return true;
    else
        return false;
}

void ArenaTableModel::fetchMore(const QModelIndex &parent)
{
    int remainingData = getDataCount() - mRowsFetchedCount;
    int itemsToFetch = qMin(mMaxRowsToFetch, remainingData);

    beginInsertRows(QModelIndex(), mRowsFetchedCount, mRowsFetchedCount + itemsToFetch - 1);
    mRowsFetchedCount += itemsToFetch;
    endInsertRows();
}
