#pragma once

#include "uvalib_global.h"
#include <QAbstractTableModel>
#include <QStringList>
#include <QMap>
#include <QList>
#include <QVariant>
#include <memory>

#include "uhunt/uhunt.h"

#include "modelstyle.h"

namespace uva
{

    class UVA_EXPORT ArenaTableModel : public QAbstractTableModel
    {
    public:

        ArenaTableModel();

        virtual int columnCount(const QModelIndex & parent = QModelIndex()) const override;
        virtual int rowCount(const QModelIndex &parent) const override;

        virtual QVariant headerData(int section, Qt::Orientation orientation, int role) const override;
        virtual QVariant data(const QModelIndex &index, int role) const override;

        virtual void setModelStyle(std::unique_ptr<ModelStyle> style);

        virtual void setMaxRowsToLoad(int maxRowsToLoad);

    protected:

        virtual bool insertColumns(QStringList columnNames);

        virtual bool canFetchMore(const QModelIndex &parent) const override;
        virtual void fetchMore(const QModelIndex &parent) override;

        virtual int getDataCount() const = 0;
        virtual QVariant getDataAtIndex(const QModelIndex &index) const = 0;

    private:

        int mMaxRowsToLoad;
        int mDisplayedCount;
        std::unique_ptr<ModelStyle> mModelStyle;
        QStringList mColumnNames;
    };

}
