#include "categorytreemodel.h"

using namespace uva;

CategoryTreeModel::CategoryTreeModel()
    : mRoot(new Category)
{
    mRoot->Name = "All Problem Categories";
}

CategoryTreeModel::~CategoryTreeModel()
{

}

QModelIndex CategoryTreeModel::index(int row, int column, const QModelIndex &parent /*= QModelIndex()*/) const
{
    if (!hasIndex(row, column, parent))
        return QModelIndex();

    Category *parentCategory;

    if (parent.isValid())
        parentCategory = static_cast<Category*>(parent.internalPointer());
    else
        parentCategory = mRoot.get();

    Category *childCategory = parentCategory->Branches.values().at(row).get();

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
    Category *parent = childCategory->Parent.lock().get();

    if (parent == mRoot.get())
        return QModelIndex();

    return createIndex(parent->Branches.keys().indexOf(childCategory->Name), 0, parent);
}

int CategoryTreeModel::rowCount(const QModelIndex &parent /*= QModelIndex()*/) const
{
    Category *parentCategory;
    if (parent.column() > 0)
        return 0;

    if (parent.isValid())
        parentCategory = static_cast<Category*>(parent.internalPointer());
    else
        parentCategory = mRoot.get();

    return parentCategory->Branches.count();
}

int CategoryTreeModel::columnCount(const QModelIndex &parent /*= QModelIndex()*/) const
{
    return 1;
}

QVariant CategoryTreeModel::data(const QModelIndex &index, int role /*= Qt::DisplayRole*/) const
{
    if (!index.isValid())
        return QVariant();


    Category *category = static_cast<Category*>(index.internalPointer());

    if (role == Qt::ToolTipRole)
        return tr("%1 problems (including subcategories)").arg(category->Problems.count());

    if (role == Qt::StatusTipRole)
        return category->Note;

    if (role == Qt::DisplayRole) 
        return category->Name;

    return QVariant();
}

void uva::CategoryTreeModel::addCategory(std::shared_ptr<Category> category)
{
    category->Parent = mRoot;
    beginInsertRows(QModelIndex(), mRoot->Branches.count(), mRoot->Branches.count());
    mRoot->Branches.insert(category->Name, category);

    for (auto problem : category->Problems)
        if (!mRoot->Problems.contains(problem->Number))
            mRoot->Problems.insert(problem->Number, problem);

    endInsertRows();
}

std::shared_ptr<Category> CategoryTreeModel::categoryRoot() const
{
    return mRoot;
}

QVariant CategoryTreeModel::headerData(int section, Qt::Orientation orientation, int role /*= Qt::DisplayRole*/) const
{
    if (orientation == Qt::Horizontal && role == Qt::DisplayRole)
        return "Category";

    return QVariant();
}
