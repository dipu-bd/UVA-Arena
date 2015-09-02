#pragma once

#include "uvalib_global.h"
#include "uvaarenawidget.h"
#include <QWidget>
#include "models\submissionstablemodel.h"
#include "QSortFilterProxyModel"

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

        bool userSubmissionsDirectoryExists(int userID);

        virtual void onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent, QVariant) override;

        void setProblemMap(std::shared_ptr<Uhunt::ProblemMap> problemMap);

    public slots:

        void onUserSubmissionsDownloaded(const QByteArray& data, int userID, int lastSubmissionID);

        QString userSubmissionsFileName(int userID);
        void loadUserSubmissionsFromFile(QString fileName, qint32 userId = -1);

    private:

        QSortFilterProxyModel mSubmissionsProxyModel;
        SubmissionsTableModel mSubmissionsTableModel;

        std::unique_ptr<Ui::ProfilesWidget> mUi;

    };

}
