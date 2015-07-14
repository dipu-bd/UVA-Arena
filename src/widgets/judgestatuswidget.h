#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include "models/judgestatustablemodel.h"

#include <QWidget>

namespace uva
{

    namespace Ui {
        class JudgeStatusWidget;
    }

    class UVA_EXPORT JudgeStatusWidget : public UVAArenaWidget
    {
        Q_OBJECT

    public:
        explicit JudgeStatusWidget(QWidget *parent = 0);
        ~JudgeStatusWidget();

        virtual void initialize() override;

        void setStatusData(std::shared_ptr<QList<JudgeStatus> > statusData,
                           std::shared_ptr<Uhunt::ProblemMap> problemMap);

    public slots:

        void onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData) override;

    private:
        Ui::JudgeStatusWidget *ui;
        JudgeStatusTableModel mStatusTableModel;
    };

}
