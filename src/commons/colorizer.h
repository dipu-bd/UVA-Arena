#pragma once

#include "uhunt/enums.h"
#include "uvalib_global.h"
#include <QVariant>
#include <QColor>

namespace uva
{

    /**
        \brief Class for converting one type of data into another
        */
    class UVA_EXPORT Colorizer
    {
    public:

        //list of matching colors with black background
        static QColor aliceBlue;
        static QColor antiqueWhite;
        static QColor aquaMarine;
        static QColor azure;
        static QColor beige;
        static QColor bisque;
        static QColor blachedAlmond;
        static QColor burlyWood;
        static QColor cornFlowerBlue;
        static QColor cornsilk;
        static QColor cyan;
        static QColor darkKhaki;
        static QColor darkSalmon;
        static QColor darkSeaGreen;
        static QColor darkTurquoise;
        static QColor floralWhite;
        static QColor gainsBoro;
        static QColor ghostWhite;
        static QColor gold;
        static QColor goldenRod;
        static QColor greenYellow;
        static QColor honeyDew;
        static QColor ivory;
        static QColor khaki;
        static QColor lavender;
        static QColor lavenderBlush;
        static QColor lawnGreen;
        static QColor lemonChiffon;
        static QColor lightBlue;
        static QColor lightCoral;
        static QColor lightCyan;
        static QColor lightGoldenRodYellow;
        static QColor lightGreen;
        static QColor lightGrey;
        static QColor lightPink;
        static QColor lightSalmon;
        static QColor lightSeaGreen;
        static QColor lightSkyBlue;
        static QColor lightSteelBlue;
        static QColor lightYellow;
        static QColor linen;
        static QColor mediumSpringGreen;
        static QColor mintCream;
        static QColor mistyRose;
        static QColor moccasin;
        static QColor navajoWhite;
        static QColor oldLace;
        static QColor orchid;
        static QColor paleGoldenRod;
        static QColor paleGreen;
        static QColor paleTurquoise;
        static QColor papayaWhip;
        static QColor peachPuff;
        static QColor pink;
        static QColor plum;
        static QColor powderBlue;
        static QColor rosyBrown;
        static QColor salmon;
        static QColor sandyBrown;
        static QColor seaShell;
        static QColor skyBlue;
        static QColor snow;
        static QColor tan;
        static QColor thistle;
        static QColor turquoise;
        static QColor violet;
        static QColor wheat;
        static QColor white;
        static QColor whiteSmoke;
        static QColor yellowGreen;


        /*!
          \brief Gets different colors for differect type of verdict messages.
          \param[in] verdict Verdict to get color of.
          \return QColor representing the color of the verdict.
         */
        static QColor getVerdictColor(Verdict verdict);

    };
}
