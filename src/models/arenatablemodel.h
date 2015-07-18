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

        virtual void setMaxRowsToFetch(int maxRowsToFetch);

        virtual void setLoadAllData(bool shouldLoadAll);

        virtual QVariant getModelDataAtIndex(const QModelIndex &index) const;

    protected:

        virtual QVariant getDataAtIndex(const QModelIndex &index) const = 0;

        virtual bool insertColumns(QStringList columnNames);

        virtual bool canFetchMore(const QModelIndex &parent) const override;
        virtual void fetchMore(const QModelIndex &parent) override;

        virtual int getDataCount() const = 0;

    private:

        bool mLoadAllData;
        int mMaxRowsToFetch;
        int mDisplayedCount;
        std::unique_ptr<ModelStyle> mModelStyle;
        QStringList mColumnNames;
    };

}
