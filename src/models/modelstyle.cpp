#include "modelstyle.h"

using namespace uva;

ModelStyle::ModelStyle(ArenaTableModel *owner)
    : mOwner(owner)
{
}

QVariant ModelStyle::Style(const QModelIndex &index, int role)
{
    return QVariant();
}
