#-------------------------------------------------
#
# Project created by QtCreator 2015-06-15T20:31:41
#
#-------------------------------------------------

QT       += network

QT       -= gui

TARGET = uhuntqt
TEMPLATE = lib

DEFINES += UHUNTQT_LIBRARY

SOURCES += uhuntqt.cpp

HEADERS += uhuntqt.h\
        uhuntqt_global.h

unix {
    target.path = /usr/lib
    INSTALLS += target
}
