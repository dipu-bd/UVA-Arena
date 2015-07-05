#pragma once

#include <QObject>
#include "uvalib_global.h"

namespace uva
{

    class UVA_EXPORT CategoryNode : public QObject
    {
        Q_OBJECT
    public:
        explicit CategoryNode(QObject *parent = 0);

    signals:

        public slots :
    };

}
