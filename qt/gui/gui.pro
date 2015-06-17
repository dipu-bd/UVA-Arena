#-------------------------------------------------
#
# Project created by QtCreator 2015-06-15T18:35:05
#
#-------------------------------------------------

QT       += core gui
CONFIG += qscintilla2

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = UVA-Arena
TEMPLATE = app

SOURCES += main.cpp\
        mainwindow.cpp \
    widgets/problemswidget.cpp \
    widgets/codeswidget.cpp \
    widgets/judgestatuswidget.cpp \
    widgets/profileswidget.cpp

HEADERS  += mainwindow.h \
    widgets/problemswidget.h \
    widgets/codeswidget.h \
    widgets/judgestatuswidget.h \
    widgets/profileswidget.h

FORMS    += mainwindow.ui \
    widgets/problemswidget.ui \
    widgets/codeswidget.ui \
    widgets/judgestatuswidget.ui \
    widgets/profileswidget.ui

SUBMODULES_DIR = ../../submodules
QDARKSTYLE = $$SUBMODULES_DIR/QDarkStyleSheet/qdarkstyle

RESOURCES += $$QDARKSTYLE/style.qrc

win32:CONFIG(release, debug|release): LIBS += -L$$OUT_PWD/../QScintilla-gpl-2.9/qscintilla/release/ -lqscintilla2
else:win32:CONFIG(debug, debug|release): LIBS += -L$$OUT_PWD/../QScintilla-gpl-2.9/qscintilla/debug/ -lqscintilla2
else:unix: LIBS += -L$$OUT_PWD/../QScintilla-gpl-2.9/qscintilla/ -lqscintilla2

INCLUDEPATH += $$PWD/../QScintilla-gpl-2.9/qscintilla
DEPENDPATH += $$PWD/../QScintilla-gpl-2.9/qscintilla

win32:CONFIG(release, debug|release): LIBS += -L$$OUT_PWD/../uhuntqt/release/ -luhuntqt
else:win32:CONFIG(debug, debug|release): LIBS += -L$$OUT_PWD/../uhuntqt/debug/ -luhuntqt
else:unix: LIBS += -L$$OUT_PWD/../uhuntqt/ -luhuntqt

INCLUDEPATH += $$PWD/../uhuntqt
DEPENDPATH += $$PWD/../uhuntqt
