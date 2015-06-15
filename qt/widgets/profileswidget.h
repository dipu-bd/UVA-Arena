#ifndef PROFILESWIDGET_H
#define PROFILESWIDGET_H

#include <QWidget>

namespace Ui {
class ProfilesWidget;
}

class ProfilesWidget : public QWidget
{
    Q_OBJECT

public:
    explicit ProfilesWidget(QWidget *parent = 0);
    ~ProfilesWidget();

private:
    Ui::ProfilesWidget *ui;
};

#endif // PROFILESWIDGET_H
