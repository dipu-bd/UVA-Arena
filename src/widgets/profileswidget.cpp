#include "profileswidget.h"
#include "ui_profileswidget.h"
#include "QFile"
#include "QDir"
#include "QStandardPaths"
#include "QMessageBox"

using namespace uva;

const QString DefaultUserSubmissionsFileName = "submissions.json";

ProfilesWidget::ProfilesWidget(QWidget *parent) :
    UVAArenaWidget(parent),
    mUi(new Ui::ProfilesWidget)
{
    mUi->setupUi(this);
    mSubmissionsProxyModel.setSourceModel(&mSubmissionsTableModel);
    mUi->tableView->setModel(&mSubmissionsProxyModel);
}

ProfilesWidget::~ProfilesWidget()
{
}

void ProfilesWidget::initialize()
{

    QObject::connect(mUhuntApi.get(), &Uhunt::userSubmissionsJsonDownloaded,
        this, &ProfilesWidget::onUserSubmissionsDownloaded);

    qint32 userId = mSettings.userId();

    if (userId != -1) {

        mUi->userNameLabel->setText(mSettings.userName());
        mUi->rankLabel->setText("");


        if (userSubmissionsDirectoryExists(userId)) {
            QString submissionsFileName = userSubmissionsFileName(userId);

            if (QFile::exists(submissionsFileName))
                loadUserSubmissionsFromFile(submissionsFileName);
            else
                mUhuntApi->userSubmissionsByUserID(userId);

        } else {

            mUhuntApi->userSubmissionsByUserID(userId);
        }
    }

}

bool uva::ProfilesWidget::userSubmissionsDirectoryExists(int userID)
{
    QDir dir(QStandardPaths::writableLocation(QStandardPaths::AppDataLocation));

    if (!dir.cd("users"))
        return false;

    QString userDirectoryName = QString("%1").arg(userID);

    if (!dir.cd(userDirectoryName))
        return false;

    return true;
}

void uva::ProfilesWidget::onUVAArenaEvent(UVAArenaWidget::UVAArenaEvent arenaEvent, QVariant)
{
    switch (arenaEvent)
    {
    case uva::UVAArenaWidget::UVAArenaEvent::UPDATE_SETTINGS:
        mUhuntApi->userSubmissionsByUserID(mSettings.userId());
        break;

    default:
        break;
    }
}

QString uva::ProfilesWidget::userSubmissionsFileName(int userID)
{
    QDir dir(QStandardPaths::writableLocation(QStandardPaths::AppDataLocation));

    if (!dir.cd("users")) {
        dir.mkdir("users");
        dir.cd("users");
    }

    QString userDirectoryName = QString("%1").arg(userID);

    if (!dir.cd(userDirectoryName)) {
        dir.mkdir(userDirectoryName);
        dir.cd(userDirectoryName);
    }

    return dir.filePath(DefaultUserSubmissionsFileName);
}

void uva::ProfilesWidget::loadUserSubmissionsFromFile(QString fileName, qint32 userId /*= -1*/)
{
    // #TODO: check if it's too old and redownload
    if (userId == -1)
        userId = mSettings.userId();

    QFile file(fileName);

    if (!file.open(QIODevice::ReadOnly)) {
        QMessageBox::critical(this, "Couldn't save file", "Could not save user submission file");
        return;
    }

    mSubmissionsTableModel.setSubmissionsList(Uhunt::userSubmissionsFromJson(file.readAll(), userId));

    file.close();

    mUi->tableView->sortByColumn(2, Qt::DescendingOrder);
}

void uva::ProfilesWidget::onUserSubmissionsDownloaded(const QByteArray& data, int userID, int lastSubmissionID)
{
    mSubmissionsTableModel.setSubmissionsList(Uhunt::userSubmissionsFromJson(data, userID));
    mUi->tableView->sortByColumn(2, Qt::DescendingOrder);

    // save the data
    
    //create/overwrite file
    
    QFile userSubmissionsFile(userSubmissionsFileName(userID));
    
    if (!userSubmissionsFile.open(QIODevice::WriteOnly)) {
        // couldn't write file
        QMessageBox::critical(this, "Couldn't save file", "Could not save user submission file");
        return;
    }

    userSubmissionsFile.write(data);

    userSubmissionsFile.flush();
    userSubmissionsFile.close();
}
