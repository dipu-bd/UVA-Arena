#pragma once

#include <QtCore/qglobal.h>

#ifdef UHUNTQT_STATIC

    #define UHUNTQT_EXPORT

#else

    #ifdef WIN32

        #ifdef UHUNTQT_SHARED
            #define UHUNTQT_EXPORT Q_DECL_EXPORT
        #else
            #define UHUNTQT_EXPORT Q_DECL_IMPORT
        #endif

    #else // not windows
        #define UHUNTQT_EXPORT
    #endif

#endif
