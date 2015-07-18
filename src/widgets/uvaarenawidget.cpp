#include "uvaarenawidget.h"

using namespace uva;

UVAArenaWidget::UVAArenaWidget(QWidget* parent)
    : QWidget(parent)
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
