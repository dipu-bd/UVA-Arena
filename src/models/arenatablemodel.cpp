#include "arenatablemodel.h"

using namespace uva;

ArenaTableModel::ArenaTableModel()
    : mModelStyle(nullptr)
{
}

bool ArenaTableModel::insertRow(QVariant key, QList<QVariant> data)
{
	beginInsertRows(QModelIndex(), mData.size(), mData.size());
	mData[key] = data;
	endInsertRows();

	return true;
}

bool ArenaTableModel::removeRow(QVariant key)
{
	ModelMap::const_iterator it = mData.find(key);

	if (it == mData.end())
		return false;
	
	int index = mData.values().indexOf(*it);
	beginRemoveRows(QModelIndex(), index, index);
	mData.remove(key);
	endRemoveRows();

	return true;
}

bool ArenaTableModel::insertColumns(QStringList columnNames)
{
	beginInsertColumns(QModelIndex(), 0, columnNames.size());
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
    return mData.count();
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

    if (index.row() >= mData.count())
        return QVariant();

    if (index.column() >= mColumnNames.count())
        return QVariant();

    if (role != Qt::DisplayRole)
    {
        if (mModelStyle)
            return mModelStyle->Style(index, role);
        else
            return QVariant();
    }
	QVariant key = mData.keys().at(index.row());

	if (index.column() == 0)
		return key;
	else
		return mData[key].at(index.column());
}

void ArenaTableModel::SetModelStyle(std::shared_ptr<ModelStyle> style)
{
    mModelStyle = style;
}
