#include "settingsdialog.h"
#include "ui_settingsdialog.h"

uva::SettingsDialog::SettingsDialog()
    : mUi(new Ui::SettingsDialog)
{
    mUi->setupUi(this);
}

uva::SettingsDialog::~SettingsDialog()
{

}

int uva::SettingsDialog::exec()
{
    // read in all settings from UVAArenaSettings
    UVAArenaSettings settings;

    mUi->userNameLineEdit->setText(settings.userName());

    mUi->problemFormatComboBox->setCurrentIndex((int)settings.problemFormatPreference());

    mUi->savePDFCheckBox->setChecked(settings.savePDFDocumentsOnDownload());

    mUi->problemsUpdateIntervalSpinBox->setValue(settings.problemsUpdateInterval());

    mUi->fetchAllProblemsCheckBox->setChecked(settings.fetchAllProblemsTableRows());

    if (settings.fetchAllProblemsTableRows())
        mUi->maxRowsToFetchSpinBox->setEnabled(false);

    mUi->maxRowsToFetchSpinBox->setValue(settings.maxProblemsTableRowsToFetch());

    mUi->categoryUpdateIntervalSpinBox->setValue(settings.categoriesUpdateInterval());

    mUi->autoStartLiveEventsCheckBox->setChecked(settings.liveEventsAutoStart());

    int updateInterval = settings.liveEventsUpdateInterval();

    if (updateInterval >= 1e3 * 60) // greater than 60 seconds
    {
        mUi->refreshLiveEventsComboBox->setCurrentIndex(1);
        updateInterval /= 1e3 * 60;
        mUi->refreshLiveEventsSpinBox->setValue(updateInterval);
    }
    else if (updateInterval >= 1e3 * 60 * 60) // greater than 60 minutes
    {
        mUi->refreshLiveEventsComboBox->setCurrentIndex(2);
        updateInterval /= 1e3 * 60 * 60;
        mUi->refreshLiveEventsSpinBox->setValue(updateInterval);
    }
    else
    {
        mUi->refreshLiveEventsComboBox->setCurrentIndex(0);
        updateInterval /= 1e3;
        mUi->refreshLiveEventsSpinBox->setValue(updateInterval);
    }

    return QDialog::exec();
}

void uva::SettingsDialog::accept()
{
    UVAArenaSettings settings;

    mSettings.setUserName(mUi->userNameLineEdit->text());
    mSettings.setProblemFormatPreference((UVAArenaSettings::ProblemFormat)mUi->problemFormatComboBox->currentIndex());
    mSettings.setSavePDFDocumentsOnDownload(mUi->savePDFCheckBox->isChecked());
    mSettings.setProblemsUpdateInterval(mUi->problemsUpdateIntervalSpinBox->value());
    mSettings.setFetchAllProblemsTableRows(mUi->fetchAllProblemsCheckBox->isChecked());
    mSettings.setMaxProblemsTableRowsToFetch(mUi->maxRowsToFetchSpinBox->value());
    mSettings.setCategoryUpdateInterval(mUi->categoryUpdateIntervalSpinBox->value());
    mSettings.setLiveEventsAutoStart(mUi->autoStartLiveEventsCheckBox->isChecked());

    int updateInterval = mUi->refreshLiveEventsSpinBox->value() * 1e3;
    if (mUi->refreshLiveEventsComboBox->currentIndex() > 0)
        updateInterval *= 60;
    if (mUi->refreshLiveEventsComboBox->currentIndex() > 1)
        updateInterval *= 60;

    mSettings.setLiveEventsUpdateInterval(updateInterval);

    QDialog::accept();
}
