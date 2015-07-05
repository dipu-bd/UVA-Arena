#pragma once

#include "uvalib_global.h"
#include <QObject>

namespace uva
{
	
	/**
		\brief base class for all UVA-Arena widgets. This class facilitates
			   communication between widgets through signals and slots, and by providing
			   methods to access common functionality.
	*/
	class UVA_EXPORT UVAArenaObject : public QObject
	{
        Q_OBJECT
	public:
        UVAArenaObject();

    signals:

    public slots:

	private:

	};

}
