#include "arenatablemodel.h"

using namespace uva;

ArenaTableModel::ArenaTableModel()
    : mModelStyle(nullptr),
      mDisplayedCount(0),
      mMaxRowsToLoad(50)
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
    return mDisplayedCount;
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

    if (role != Qt::DisplayRole) {
        if (mModelStyle)
            return mModelStyle->Style(index, role);
        else
            return QVariant();
    }

    return getDataAtIndex(index);
}

void ArenaTableModel::setModelStyle(std::unique_ptr<ModelStyle> style)
{
    mModelStyle = std::move(style);
}

void ArenaTableModel::setMaxRowsToLoad(int maxRowsToLoad)
{
    mMaxRowsToLoad = maxRowsToLoad;
}

bool ArenaTableModel::canFetchMore(const QModelIndex &parent) const
{
    if (mDisplayedCount < getDataCount())
        return true;
    else
        return false;
}

void ArenaTableModel::fetchMore(const QModelIndex &parent)
{
    int remainingData = getDataCount() - mDisplayedCount;
    int itemsToFetch = qMin(mMaxRowsToLoad, remainingData);

    beginInsertRows(QModelIndex(), mDisplayedCount, mDisplayedCount + itemsToFetch - 1);
    mDisplayedCount += itemsToFetch;
    endInsertRows();
}
