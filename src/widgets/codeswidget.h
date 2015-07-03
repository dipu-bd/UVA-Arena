#pragma once

#include "src_global.h"
#include <QWidget>

namespace Ui {
class CodesWidget;
}

class UVA_EXPORT CodesWidget : public QWidget
{
    Q_OBJECT

public:
    explicit CodesWidget(QWidget *parent = 0);
    ~CodesWidget();

private:
    Ui::CodesWidget *ui;
};
