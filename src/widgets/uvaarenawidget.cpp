#include "uvaarenawidget.h"

using namespace uva;

UVAArenaWidget::UVAArenaWidget(QWidget* parent)
    : QWidget(parent),
      mProblemsWidget(nullptr),
      mCodesWidget(nullptr),
      mJudgeStatusWidget(nullptr),
      mProfilesWidget(nullptr),
      mMainWindow(nullptr)
{
}

UVAArenaWidget::~UVAArenaWidget()
{
}

void UVAArenaWidget::setNetworkManager(
    std::shared_ptr<QNetworkAccessManager> networkManager)
{
    mNetworkManager = networkManager;
}

void UVAArenaWidget::setUhuntApi(std::shared_ptr<Uhunt> uhuntApi)
{
    mUhuntApi = uhuntApi;
}

ProblemsWidget* UVAArenaWidget::problemsWidget()
{
    return mProblemsWidget;
}

void UVAArenaWidget::setProblemsWidget(ProblemsWidget* problemsWidget)
{
    mProblemsWidget = problemsWidget;
}

CodesWidget* UVAArenaWidget::codesWidget()
{
    return mCodesWidget;
}

void UVAArenaWidget::setCodesWidget(CodesWidget* codesWidget)
{
    mCodesWidget = codesWidget;
}

JudgeStatusWidget* UVAArenaWidget::judgeStatusWidget()
{
    return mJudgeStatusWidget;
}

void UVAArenaWidget::setJudgeStatusWidget(JudgeStatusWidget* judgeStatusWidget)
{
    mJudgeStatusWidget = judgeStatusWidget;
}

ProfilesWidget* UVAArenaWidget::profilesWidget()
{
    return mProfilesWidget;
}

void UVAArenaWidget::setProfilesWidget(ProfilesWidget* profilesWidget)
{
    mProfilesWidget = profilesWidget;
}

MainWindow* UVAArenaWidget::mainWindow()
{
    return mMainWindow;
}

void UVAArenaWidget::setMainWindow(MainWindow* mainWindow)
{
    mMainWindow = mainWindow;
}
