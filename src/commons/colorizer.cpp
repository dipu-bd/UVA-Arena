#include "colorizer.h"

using namespace uva;

//colors
QColor Colorizer::aliceBlue = QColor(240,248,255);
QColor Colorizer::antiqueWhite = QColor(250,235,215);
QColor Colorizer::aquaMarine = QColor(127,255,212);
QColor Colorizer::azure = QColor(240,255,255);
QColor Colorizer::beige = QColor(245,245,220);
QColor Colorizer::bisque = QColor(255,228,196);
QColor Colorizer::blachedAlmond = QColor(255,235,205);
QColor Colorizer::burlyWood = QColor(222,184,135);
QColor Colorizer::cornFlowerBlue = QColor(100,149,237);
QColor Colorizer::cornsilk = QColor(255,248,220);
QColor Colorizer::cyan = QColor(0,255,255);
QColor Colorizer::darkKhaki = QColor(189,183,107);
QColor Colorizer::darkSalmon = QColor(233,150,122);
QColor Colorizer::darkSeaGreen = QColor(143,188,139);
QColor Colorizer::darkTurquoise = QColor(0,206,209);
QColor Colorizer::floralWhite = QColor(255,250,240);
QColor Colorizer::gainsBoro = QColor(220,220,220);
QColor Colorizer::ghostWhite = QColor(248,248,255);
QColor Colorizer::gold = QColor(255,215,0);
QColor Colorizer::goldenRod = QColor(218,165,32);
QColor Colorizer::greenYellow = QColor(173,255,47);
QColor Colorizer::honeyDew = QColor(240,255,240);
QColor Colorizer::ivory = QColor(255,255,240);
QColor Colorizer::khaki = QColor(240,230,140);
QColor Colorizer::lavender = QColor(230,230,250);
QColor Colorizer::lavenderBlush = QColor(255,240,245);
QColor Colorizer::lawnGreen = QColor(124,252,0);
QColor Colorizer::lemonChiffon = QColor(255,250,205);
QColor Colorizer::lightBlue = QColor(173,216,230);
QColor Colorizer::lightCoral = QColor(240,128,128);
QColor Colorizer::lightCyan = QColor(224,255,255);
QColor Colorizer::lightGoldenRodYellow = QColor(250,250,210);
QColor Colorizer::lightGreen = QColor(144,238,144);
QColor Colorizer::lightGrey = QColor(211,211,211);
QColor Colorizer::lightPink = QColor(255,182,193);
QColor Colorizer::lightSalmon = QColor(255,160,122);
QColor Colorizer::lightSeaGreen = QColor(32,178,170);
QColor Colorizer::lightSkyBlue = QColor(135,206,250);
QColor Colorizer::lightSteelBlue = QColor(176,196,222);
QColor Colorizer::lightYellow = QColor(255,255,224);
QColor Colorizer::linen = QColor(250,240,230);
QColor Colorizer::mediumSpringGreen = QColor(0,250,154);
QColor Colorizer::mintCream = QColor(245,255,250);
QColor Colorizer::mistyRose = QColor(255,228,225);
QColor Colorizer::moccasin = QColor(255,228,181);
QColor Colorizer::navajoWhite = QColor(255,222,173);
QColor Colorizer::oldLace = QColor(253,245,230);
QColor Colorizer::orchid = QColor(218,112,214);
QColor Colorizer::paleGoldenRod = QColor(238,232,170);
QColor Colorizer::paleGreen = QColor(152,251,152);
QColor Colorizer::paleTurquoise = QColor(175,238,238);
QColor Colorizer::papayaWhip = QColor(255,239,213);
QColor Colorizer::peachPuff = QColor(255,218,185);
QColor Colorizer::pink = QColor(255,192,203);
QColor Colorizer::plum = QColor(221,160,221);
QColor Colorizer::powderBlue = QColor(176,224,230);
QColor Colorizer::rosyBrown = QColor(188,143,143);
QColor Colorizer::salmon = QColor(250,128,114);
QColor Colorizer::sandyBrown = QColor(244,164,96);
QColor Colorizer::seaShell = QColor(255,245,238);
QColor Colorizer::skyBlue = QColor(135,206,235);
QColor Colorizer::snow = QColor(255,250,250);
QColor Colorizer::tan = QColor(210,180,140);
QColor Colorizer::thistle = QColor(216,191,216);
QColor Colorizer::turquoise = QColor(64,224,208);
QColor Colorizer::violet = QColor(238,130,238);
QColor Colorizer::wheat = QColor(245,222,179);
QColor Colorizer::white = QColor(255,255,255);
QColor Colorizer::whiteSmoke = QColor(245,245,245);
QColor Colorizer::yellowGreen = QColor(154,205,50);

QColor Colorizer::getVerdictColor(Submission::Verdict verdict)
{
typedef Submission::Verdict Verdict;
    switch(verdict)
    {
    case Verdict::SUBMISSION_ERROR:
        return Colorizer::darkSeaGreen;

    case Verdict::CANNOT_BE_JUDGED:
        return Colorizer::bisque;

    case Verdict::COMPILE_ERROR:
        return Colorizer::thistle;

    case Verdict::RESTRICTED_FUNCTION:
    case Verdict::RUNTIME_ERROR:
        return Colorizer::skyBlue;

    case Verdict::OUTPUT_LIMIT:
        return Colorizer::lightCoral;

    case Verdict::TIME_LIMIT:
        return Colorizer::khaki;

    case Verdict::MEMORY_LIMIT:
        return Colorizer::pink;

    case Verdict::WRONG_ANSWER:
        return Colorizer::lawnGreen;

    case Verdict::PRESENTATION_ERROR:
        return Colorizer::lemonChiffon;

    case Verdict::ACCEPTED:
        return Colorizer::floralWhite;

    default:
        return Qt::white;
    }
}
