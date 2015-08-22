#pragma once

#include "arenatablemodel.h"
#include "mainwindow.h"
#include "Uhunt/uhunt.h"
#include <QList>
#include <memory>

namespace uva
{

    class UVA_EXPORT LiveEventsTableModel : public ArenaTableModel
    {
    public:
        LiveEventsTableModel();

        void setLiveEventMap(Uhunt::LiveEventMap statusData);

        void setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap);

        qint64 getLastSubmissionId();

    protected:

        virtual int dataCount() const override;

        virtual QVariant dataAtIndex(const QModelIndex &index) const override;

        virtual QVariant style(const QModelIndex &index, int role) const override;

    private:

        QList<qint64> mRowToId;
        Uhunt::LiveEventMap mStatusData;
        std::shared_ptr<Uhunt::ProblemMap> mProblemMap;
    };
}
