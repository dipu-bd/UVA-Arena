#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include "models/problemstablemodel.h"
#include "models/categorytreemodel.h"
#include "models/problemsproxymodel.h"

#include <QWidget>

#include <QWidget>
#include <QSortFilterProxyModel>

namespace uva
{

    namespace Ui {
        class ProblemsWidget;
    }

    class UVA_EXPORT ProblemsWidget : public UVAArenaWidget
    {
        Q_OBJECT

    public:
        explicit ProblemsWidget(QWidget *parent = 0);
        ~ProblemsWidget();

        /** Initializes this object. */
        virtual void initialize() override;

        /**
            Sets problem map.
        
            \param  problemsMap The problems map.
        */
        void setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemsMap);

    public slots:

    private slots:

        /**
            Invoked when the Problems table is double clicked.
        
            \param  index   which row in the model the user clicked
        */
        void problemsTableDoubleClicked(QModelIndex index);

        void categoryTreeViewClicked(QModelIndex index);

    private:

        /**
            Queries if the category index file exists.
        
            \return true if it succeeds, false if it fails.
        */
        bool categoryIndexFileExists();

        /** Downloads the category index. */
        void downloadCategoryIndex();
        /** Loads category index from file. */
        void loadCategoryIndexFromFile();

        /**
            Executes the category index downloaded action.
        
            \param  data    The downloaded data.
        */
        void onCategoryIndexDownloaded(QByteArray data);

        /**
            Downloads the categories described by categories.
        
            \param  categories  The categories.
        */
        void downloadCategories(QList<QPair<QString, int> > categories);

        /**
            Category index JSON to list.
        
            \param  data    The data.
        
            \return A list of.
        */
        QList<QPair<QString, int> > categoryIndexJsonToList(QByteArray data);

        /**
            Should be invoked when a new category is loaded.
        
            \param [in] category    If non-null, the category.
        */
        void newCategoryLoaded(std::shared_ptr<Category> category);

        /**
            Saves a category file.
        
            \param  categoryJson    The category JSON.
            \param  fileName        Filename of the file.
        */
        void saveCategoryFile(const QByteArray &categoryJson, QString fileName);

        ProblemsProxyModel mProblemsFilterProxyModel;
        QSortFilterProxyModel mCategoryFilterProxyModel;
        ProblemsTableModel mProblemsTableModel;
        CategoryTreeModel mCategoryTreeModel;
        std::unique_ptr<Ui::ProblemsWidget> mUi;
    };

}
