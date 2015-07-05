#pragma once

#include "src_global.h"
#include <QAbstractTableModel>
#include <QStringList>
#include <QMap>
#include <QList>
#include <QVariant>
#include <memory>

#include "modelstyle.h"

namespace uva
{

    class UVA_EXPORT ArenaTableModel : public QAbstractTableModel
    {
    public:
        typedef QMap<QVariant, QList<QVariant> > ModelMap;
        ArenaTableModel();

        virtual bool insertRow(QVariant key, QList<QVariant> data);
        virtual bool removeRow(QVariant key);
        virtual bool insertColumns(QStringList columnNames);

        virtual int columnCount(const QModelIndex & parent = QModelIndex()) const override;
        virtual int rowCount(const QModelIndex &parent) const override;

        virtual QVariant headerData(int section, Qt::Orientation orientation, int role) const override;
        virtual QVariant data(const QModelIndex &index, int role) const override;

        virtual void SetModelStyle(std::shared_ptr<ModelStyle> style);

    private:
        std::shared_ptr<ModelStyle> mModelStyle;
        ModelMap mData;
        QStringList mColumnNames;
    };

}
