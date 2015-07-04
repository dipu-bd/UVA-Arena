#pragma once

#include <QObject>
#include "uhuntqt_global.h"

namespace uhuntqt
{

    class UHUNTQT_EXPORT ContextBook : public QObject
    {
        Q_OBJECT
    public:
        explicit ContextBook(QObject *parent = 0);

    signals:

        public slots :
    };

}
