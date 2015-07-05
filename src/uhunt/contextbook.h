#pragma once

#include <QObject>
#include "uvalib_global.h"

namespace uva
{

    class UVA_EXPORT ContextBook : public QObject
    {
        Q_OBJECT
    public:
        explicit ContextBook(QObject *parent = 0);

    signals:

        public slots :
    };

}
