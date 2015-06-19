#ifndef UHUNTQT_GLOBAL_H
#define UHUNTQT_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(UHUNTQT_LIBRARY)
#  define UHUNTQTSHARED_EXPORT Q_DECL_EXPORT
#else
#  define UHUNTQTSHARED_EXPORT Q_DECL_IMPORT
#endif

#endif // UHUNTQT_GLOBAL_H
