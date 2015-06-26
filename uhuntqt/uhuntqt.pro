#-------------------------------------------------
#
# Project created by QtCreator 2015-06-15T20:31:41
#
#-------------------------------------------------

include(../defaults.pri)

QT       += network

QT       -= gui

TARGET = uhuntqt
TEMPLATE = lib

DEFINES += UHUNTQT_LIBRARY

SOURCES += uhuntqt.cpp \
    probleminfo.cpp \
    userinfo.cpp \
    userranklist.cpp \
    usersubmission.cpp \
    submissionmessage.cpp \
    judgestatus.cpp \
    categorynode.cpp \
    contextbook.cpp

HEADERS += uhuntqt.h\
    uhuntqt_global.h \
    probleminfo.h \
    userinfo.h \
    userranklist.h \
    usersubmission.h \
    submissionmessage.h \
    judgestatus.h \
    categorynode.h \
    contextbook.h \
    enums.h

unix {
    target.path = /usr/lib
    INSTALLS += target
}
