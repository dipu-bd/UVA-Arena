#ifndef CATEGORYNODE_H
#define CATEGORYNODE_H

#include <QObject>

#include "uhuntqt_global.h"

class UHUNTQT_EXPORT CategoryNode : public QObject
{
    Q_OBJECT
public:
    explicit CategoryNode(QObject *parent = 0);

signals:

public slots:
};

#endif // CATEGORYNODE_H
