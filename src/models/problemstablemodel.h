#pragma once

#include "arenatablemodel.h"
#include "Uhunt/uhunt.h"

namespace uva
{

    class UVA_EXPORT ProblemsTableModel : public ArenaTableModel
    {
    public:

        ProblemsTableModel();

        void setUhuntProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap);

    protected:

        virtual QVariant getDataAtIndex(const QModelIndex &index) const;

        virtual int getDataCount() const;

    private:

        QList<int> mRowToId;
        std::shared_ptr<Uhunt::ProblemMap> mProblemMap;

    };
}
