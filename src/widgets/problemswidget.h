#ifndef PROBLEMSWIDGET_H
#define PROBLEMSWIDGET_H

#include <QWidget>

namespace Ui {
class ProblemsWidget;
}

class ProblemsWidget : public QWidget
{
    Q_OBJECT

public:
    explicit ProblemsWidget(QWidget *parent = 0);
    ~ProblemsWidget();

private:
    Ui::ProblemsWidget *ui;
};

#endif // PROBLEMSWIDGET_H
