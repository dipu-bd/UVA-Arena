#pragma once

#include "arenatablemodel.h"
#include "mainwindow.h"
#include "Uhunt/uhunt.h"

namespace uva
{

    class UVA_EXPORT JudgeStatusTableModel : public ArenaTableModel
    {
    public:
        JudgeStatusTableModel();

        void setStatusData(std::shared_ptr< QList<JudgeStatus> > statusData,
            std::shared_ptr<Uhunt::ProblemMap> problemMap);

    protected:

        virtual int getDataCount() const;

        virtual QVariant getDataAtIndex(const QModelIndex &index) const;

    private:

        std::shared_ptr< QList<JudgeStatus> > mStatusData;

    };
}
