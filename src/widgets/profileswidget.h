#ifndef PROFILESWIDGET_H
#define PROFILESWIDGET_H

#include "src_global.h"
#include <QWidget>

namespace Ui {
class ProfilesWidget;
}

class SRCSHARED_EXPORT ProfilesWidget : public QWidget
{
    Q_OBJECT

public:
    explicit ProfilesWidget(QWidget *parent = 0);
    ~ProfilesWidget();

private:
    Ui::ProfilesWidget *ui;
};

#endif // PROFILESWIDGET_H
