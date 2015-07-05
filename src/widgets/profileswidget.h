#pragma once

#include "src_global.h"
#include <QWidget>

namespace uva
{
    namespace Ui {
        class ProfilesWidget;
    }

    class UVA_EXPORT ProfilesWidget : public QWidget
    {
        Q_OBJECT

    public:
        explicit ProfilesWidget(QWidget *parent = 0);
        ~ProfilesWidget();

    private:
        Ui::ProfilesWidget *ui;
    };

}
