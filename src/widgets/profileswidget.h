#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include <QWidget>

namespace uva
{
    namespace Ui {
        class ProfilesWidget;
    }
    // #TODO Create GUI interface to allow user to see their submissions
    // and rankings
    class UVA_EXPORT ProfilesWidget : public UVAArenaWidget
    {
        Q_OBJECT

    public:
        explicit ProfilesWidget(QWidget *parent = 0);
        ~ProfilesWidget();

        virtual void initialize() override;

        virtual void onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent, QVariant) override;

    public slots:

    private:
        std::unique_ptr<Ui::ProfilesWidget> mUi;

    };

}
