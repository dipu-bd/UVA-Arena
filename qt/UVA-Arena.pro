TEMPLATE = subdirs

SUBDIRS += \
    uhuntqt \
    QScintilla-gpl-2.9\qscintilla \
    gui \

gui.depends = uhuntqt QScintilla-gpl-2.9\qscintilla
