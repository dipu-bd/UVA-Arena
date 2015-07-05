#pragma once

#include "uvalib_global.h"
#include <QWidget>

namespace uva
{
	
	/**
		\brief base class for all UVA-Arena widgets. This class facilitates
			   communication between widgets through signals and slots, and by providing
			   methods to access common functionality.
	*/
	class UVA_EXPORT UVAArenaWidget : public QWidget
	{
        Q_OBJECT
	public:
        explicit UVAArenaWidget(QWidget *parent = 0);

    signals:

    public slots:

	private:

	};

}
