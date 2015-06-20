#pragma once

#include <QtCore/qglobal.h>

#if defined(SRC_LIBRARY)
#  define SRCSHARED_EXPORT Q_DECL_EXPORT
#else
#  define SRCSHARED_EXPORT Q_DECL_IMPORT
#endif
