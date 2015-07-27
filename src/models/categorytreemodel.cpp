#include "categorytreemodel.h"

using namespace uva;

CategoryTreeModel::CategoryTreeModel()
    : mRoot(new Category)
{
    mRoot->Name = "All Problem Categories";
}

CategoryTreeModel::~CategoryTreeModel()
{
    delete mRoot;
}

QModelIndex CategoryTreeModel::index(int row, int column, const QModelIndex &parent /*= QModelIndex()*/) const
{
    if (!hasIndex(row, column, parent))
        return QModelIndex();

    Category *parentCategory;

    if (parent.isValid())
        parentCategory = static_cast<Category*>(parent.internalPointer());
    else
        parentCategory = mRoot;

    Category *childCategory = parentCategory->Branches.values().at(row);

    if (childCategory)
        return createIndex(row, column, childCategory);
    else
        return QModelIndex();

}

QModelIndex CategoryTreeModel::parent(const QModelIndex &child) const
{
    if (!child.isValid())
        return QModelIndex();

    Category *childCategory = static_cast<Category*>(child.internalPointer());
    Category *parent = childCategory->Parent;

    if (parent == mRoot)
        return QModelIndex();

    return createIndex(parent->Branches.values().indexOf(childCategory), 0, parent);
}

int CategoryTreeModel::rowCount(const QModelIndex &parent /*= QModelIndex()*/) const
{
    Category *parentCategory;
    if (parent.column() > 0)
        return 0;

    if (parent.isValid())
        parentCategory = static_cast<Category*>(parent.internalPointer());
    else
        parentCategory = mRoot;

    return parentCategory->Branches.count();
}

int CategoryTreeModel::columnCount(const QModelIndex &parent /*= QModelIndex()*/) const
{
    return 2;
}

QVariant CategoryTreeModel::data(const QModelIndex &index, int role /*= Qt::DisplayRole*/) const
{
    if (!index.isValid())
        return QVariant();

    if (role != Qt::DisplayRole)
        return QVariant();

    Category *category = static_cast<Category*>(index.internalPointer());

    if (index.column() == 0)
        return category->Name;
    else if (index.column() == 1)
        return category->Note;

    return QVariant();
}

void CategoryTreeModel::addCategory(Category *category)
{
    category->Parent = mRoot;
    beginInsertRows(QModelIndex(), mRoot->Branches.count(), mRoot->Branches.count());
    mRoot->Branches.insert(category->Name, category);
    endInsertRows();
}

QVariant CategoryTreeModel::headerData(int section, Qt::Orientation orientation, int role /*= Qt::DisplayRole*/) const
{
    if (orientation == Qt::Horizontal && role == Qt::DisplayRole)
    {
        if (section == 0)
            return "Category";
        else if (section == 1)
            return "Note";
    }

    return QVariant();
}
