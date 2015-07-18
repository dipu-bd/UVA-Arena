#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include "models/judgestatustablemodel.h"

#include <QWidget>
#include <QTimer>

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

        void setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap);

        void setStatusData(Uhunt::JudgeStatusMap statusData);

        void refreshJudgeStatus();

    protected:

    public slots:

        void onUVAArenaEvent(UVAArenaEvent arenaEvent, QVariant metaData) override;

    private:
        std::shared_ptr<Uhunt::ProblemMap> mProblemMap;
        Ui::JudgeStatusWidget *ui;
        JudgeStatusTableModel mStatusTableModel;
        QTimer *mTimer;
    };

}
