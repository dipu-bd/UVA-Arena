#pragma once

#include <QObject>
#include "uhuntqt_global.h"

namespace uhuntqt
{

    class UHUNTQT_EXPORT CategoryNode : public QObject
    {
        Q_OBJECT
    public:
        explicit CategoryNode(QObject *parent = 0);

    signals:

        public slots :
    };

}
