#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include "models/liveeventstablemodel.h"

#include <QWidget>
#include <QTimer>

namespace uva
{

    namespace Ui {
        class LiveEventsWidget;
    }

    class UVA_EXPORT LiveEventsWidget : public UVAArenaWidget
    {
        Q_OBJECT

    public:
        explicit LiveEventsWidget(QWidget *parent = 0);
        ~LiveEventsWidget();

        virtual void initialize() override;

        void setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap);

        void setStatusData(Uhunt::LiveEventMap statusData);

        void refreshJudgeStatus();

    protected:

    public slots:

        void onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData) override;

    private:

        std::unique_ptr<Ui::LiveEventsWidget> mUi;
        LiveEventsTableModel mStatusTableModel;
        QTimer mTimer;
    };

}
