#pragma once

#include "uvalib_global.h"
#include <QVariant>

namespace uva
{
    class ArenaTableModel;

    /**
        \brief Parent class for custom styles used with ArenaTableModel.
        */
    class UVA_EXPORT ModelStyle
    {
    public:

        ModelStyle(ArenaTableModel *owner = nullptr);
        /**
            \brief Controls how an item in a view should be styled.

            This function should be overridden in subclasses and have a switch
            statement on the role parameter.

            The role parameter takes on a value from the Qt::ItemDataRole enum
            located in qnamespace.h.

            For convenience, these are some of the various roles:
            DisplayRole = 0,
            DecorationRole = 1,
            EditRole = 2,
            ToolTipRole = 3,
            StatusTipRole = 4,
            WhatsThisRole = 5,
            FontRole = 6,
            TextAlignmentRole = 7,
            BackgroundColorRole = 8,
            BackgroundRole = 8,
            TextColorRole = 9,
            ForegroundRole = 9,
            CheckStateRole = 10

            Documentation on these roles can be found here:
            http://doc.qt.io/qt-5/qt.html#ItemDataRole-enum

            \param index contains the row and column of the current item.
            \param role the Qt::ItemDataRole to consider.
            \return The appropriate Qt object that will be used to style the
            view item.
            */
        virtual QVariant Style(const QModelIndex &index, int role);

    protected:
        ArenaTableModel* mOwner;
    };

}
