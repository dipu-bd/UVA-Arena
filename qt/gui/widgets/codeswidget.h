#ifndef CODESWIDGET_H
#define CODESWIDGET_H

#include <QWidget>

namespace Ui {
class CodesWidget;
}

class CodesWidget : public QWidget
{
    Q_OBJECT

public:
    explicit CodesWidget(QWidget *parent = 0);
    ~CodesWidget();

private:
    Ui::CodesWidget *ui;
};

#endif // CODESWIDGET_H
