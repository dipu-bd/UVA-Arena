#ifndef CONTEXTBOOK_H
#define CONTEXTBOOK_H

#include <QObject>
#include "uhuntqt_global.h"

class UHUNTQT_EXPORT ContextBook : public QObject
{
    Q_OBJECT
public:
    explicit ContextBook(QObject *parent = 0);

signals:

public slots:
};

#endif // CONTEXTBOOK_H
