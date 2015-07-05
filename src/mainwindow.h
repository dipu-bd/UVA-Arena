#pragma once

#include "src_global.h"
#include <QMainWindow>

namespace uva
{

    namespace Ui {
        class MainWindow;
    }

    class UVA_EXPORT MainWindow : public QMainWindow
    {
        Q_OBJECT

    public:
        explicit MainWindow(QWidget *parent = 0);
        ~MainWindow();

    private:
        Ui::MainWindow *ui;
    };

}
