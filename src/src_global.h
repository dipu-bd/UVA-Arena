#pragma once

#include <QtCore/qglobal.h>

#ifdef UVA_STATIC

    #define UVA_EXPORT

#else

    #ifdef WIN32
    
        #ifdef UVA_SHARED
        #define UVA_EXPORT Q_DECL_EXPORT
        #else
        #define UVA_EXPORT Q_DECL_IMPORT
        #endif

    #else // not windows
        #define UVA_EXPORT
    #endif

#endif
