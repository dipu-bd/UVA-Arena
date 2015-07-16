#pragma once

#include "arenatablemodel.h"
#include "mainwindow.h"
#include "Uhunt/uhunt.h"
#include <QList>

namespace uva
{

    class UVA_EXPORT JudgeStatusTableModel : public ArenaTableModel
    {
    public:
        JudgeStatusTableModel();

        void setStatusData(Uhunt::JudgeStatusMap statusData, std::shared_ptr<Uhunt::ProblemMap> problemMap);

        qint64 getLastSubmissionId();

    protected:

        virtual int getDataCount() const;

        virtual QVariant getDataAtIndex(const QModelIndex &index) const;

    private:

        QList<qint64> mRowToId;
        Uhunt::JudgeStatusMap mStatusData;

        void updateStatusData(Uhunt::JudgeStatusMap statusData);
    };
}
