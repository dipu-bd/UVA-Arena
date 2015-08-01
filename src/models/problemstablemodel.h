#pragma once

#include "arenatablemodel.h"
#include "uhunt/uhunt.h"
#include "uhunt/category.h"

namespace uva
{

    class UVA_EXPORT ProblemsTableModel : public ArenaTableModel
    {
    public:

        ProblemsTableModel();

        void setUhuntProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap);

        void setCategoryRoot(std::shared_ptr<Category> root);

    protected:

        virtual QVariant getDataAtIndex(const QModelIndex &index) const override;

        virtual QVariant style(const QModelIndex &index, int role) const override;

        virtual int getDataCount() const override;

    private:
        int mMaxDacu;
        QList<int> mRowToId;
        std::shared_ptr<Uhunt::ProblemMap> mProblemMap;
        std::shared_ptr<Category> mCategoryRoot;
    };
}
