#pragma once

#include "QAbstractItemModel"
#include "QVariant"

#include "uhunt/category.h"

namespace uva
{

    class CategoryTreeModel :public QAbstractItemModel
    {
    public:

        CategoryTreeModel();
        ~CategoryTreeModel();

        virtual QModelIndex index(int row, int column, const QModelIndex &parent = QModelIndex()) const override;

        virtual QModelIndex parent(const QModelIndex &child) const override;

        virtual int rowCount(const QModelIndex &parent = QModelIndex()) const override;

        virtual int columnCount(const QModelIndex &parent = QModelIndex()) const override;

        virtual QVariant data(const QModelIndex &index, int role = Qt::DisplayRole) const override;

        void addCategory(std::shared_ptr<Category> category);

        virtual QVariant headerData(int section, Qt::Orientation orientation, int role = Qt::DisplayRole) const override;

        std::shared_ptr<Category> categoryRoot() const;

    protected:

    private:

        std::shared_ptr<Category> mRoot;

    };

}
