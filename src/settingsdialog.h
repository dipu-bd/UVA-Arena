#pragma once

#include "uvalib_global.h"

#include <memory>
#include "QDialog"
#include "uvaarenasettings.h"

namespace uva
{
    namespace Ui
    {
        class SettingsDialog;
    }

    class UVA_EXPORT SettingsDialog : public QDialog
    {
    public:

        SettingsDialog();

        ~SettingsDialog();

        /*! Opens this object. */
        virtual int exec() override;

        /*! Accepts this object. */
        virtual void accept() override;

    private:
        UVAArenaSettings mSettings;
        std::unique_ptr<Ui::SettingsDialog> mUi;


    };
}