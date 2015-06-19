#-------------------------------------------------
#
# Project created by QtCreator 2015-06-15T18:35:05
#
#-------------------------------------------------

include(../defaults.pri)

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = UVA-Arena
TEMPLATE = app

SOURCES += main.cpp

win32:CONFIG(release, debug|release): LIBS += -L$$OUT_PWD/../src/release/ -lUVA-Arena
else:win32:CONFIG(debug, debug|release): LIBS += -L$$OUT_PWD/../src/debug/ -lUVA-Arena
else:unix: LIBS += -L$$OUT_PWD/../src/ -lUVA-Arena

INCLUDEPATH += $$PWD/../src
DEPENDPATH += $$PWD/../src

win32:CONFIG(release, debug|release): LIBS += -L$$OUT_PWD/../QScintilla-gpl-2.9/qscintilla/release/ -lqscintilla2
else:win32:CONFIG(debug, debug|release): LIBS += -L$$OUT_PWD/../QScintilla-gpl-2.9/qscintilla/debug/ -lqscintilla2
else:unix: LIBS += -L$$OUT_PWD/../QScintilla-gpl-2.9/qscintilla/ -lqscintilla2

INCLUDEPATH += $$PWD/../QScintilla-gpl-2.9/qscintilla
DEPENDPATH += $$PWD/../QScintilla-gpl-2.9/qscintilla
