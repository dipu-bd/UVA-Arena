#ifndef UHUNTQT_H
#define UHUNTQT_H

#include "probleminfo.h"
#include "uhuntqt_global.h"

class UHUNTQTSHARED_EXPORT Uhuntqt
{

public:
    Uhuntqt();

    /**
     * @brief getProblemList Get the list of problem info.
     * @param data Downloaded JSON data.
     * @param size Size of the JSON data.
     * @return List of problem info objects.
     */
    static QList<ProblemInfo> Uhuntqt::getProblemList(const char* data, int size);

};

#endif // UHUNTQT_H
