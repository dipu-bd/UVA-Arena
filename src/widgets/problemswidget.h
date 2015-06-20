#ifndef PROBLEMSWIDGET_H
#define PROBLEMSWIDGET_H

#include "src_global.h"
#include <QWidget>

namespace Ui {
class ProblemsWidget;
}

class SRCSHARED_EXPORT ProblemsWidget : public QWidget
{
    Q_OBJECT

public:
    explicit ProblemsWidget(QWidget *parent = 0);
    ~ProblemsWidget();

private:
    Ui::ProblemsWidget *ui;
};

#endif // PROBLEMSWIDGET_H
