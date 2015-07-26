set(TARGET UVA-Arena-lib)

# Find includes in corresponding build directories
set(CMAKE_INCLUDE_CURRENT_DIR ON)
# Instruct CMake to run moc automatically when needed
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)

set(UHUNT_SOURCES
    uhunt/uhunt.cpp
    uhunt/uhunt.h
    uhunt/category.h
    uhunt/category.cpp
    uhunt/liveevent.h
    uhunt/liveevent.cpp
    uhunt/problem.h
    uhunt/problem.cpp
    uhunt/usersubmission.h
    uhunt/usersubmission.cpp
    uhunt/submission.h
    uhunt/submission.cpp
    uhunt/userrank.h
    uhunt/userrank.cpp
)

set(MODELS_SOURCES
    models/arenatablemodel.h
    models/arenatablemodel.cpp
    models/problemstablemodel.h
    models/problemstablemodel.cpp
    models/liveeventstablemodel.h
    models/liveeventstablemodel.cpp
)

set(WIDGETS_SOURCES
    widgets/uvaarenawidget.h
    widgets/uvaarenawidget.cpp
    widgets/codeswidget.h
    widgets/codeswidget.cpp
    widgets/liveeventswidget.h
    widgets/liveeventswidget.cpp
    widgets/problemswidget.h
    widgets/problemswidget.cpp
    widgets/profileswidget.h
    widgets/profileswidget.cpp
    widgets/mupdfwidget.h
    widgets/mupdfwidget.cpp
    widgets/pdfviewer.h
    widgets/pdfviewer.cpp
)

set(MAIN_SOURCES
    commons/conversion.h
    commons/conversion.cpp
    commons/colorizer.h
    commons/colorizer.cpp
    mainwindow.h
    mainwindow.cpp
    uvaarenasettings.h
    uvaarenasettings.cpp
    uvalib_global.h
)

set(ALL_SOURCES
    ${OTHER_SOURCES}
    ${UHUNT_SOURCES}
    ${WIDGETS_SOURCES}
    ${MODELS_SOURCES}
    ${MAIN_SOURCES}
)

set(UI_SOURCES
    mainwindow.ui
    widgets/codeswidget.ui
    widgets/liveeventswidget.ui
    widgets/problemswidget.ui
    widgets/profileswidget.ui
    widgets/pdfviewer.ui
)

qt5_wrap_ui(UI_FILES
    ${UI_SOURCES}
)

option(${TARGET}_SHARED "Build ${TARGET} as a shared library." ON)
if(${TARGET}_SHARED)
    add_definitions(-DUVA_SHARED)
    add_library(${TARGET} SHARED ${ALL_SOURCES} ${UI_FILES} ${UVA_RESOURCES})
else()
    add_definitions(-DUVA_STATIC)
    add_library(${TARGET} STATIC ${ALL_SOURCES} ${UI_FILES} ${UVA_RESOURCES})
endif()

target_link_libraries(${TARGET} ${UVA_DEPENDENCIES})
set_target_properties(${TARGET} PROPERTIES FOLDER "UVA-Arena")

source_group("Uhunt" FILES ${UHUNT_SOURCES})
source_group("Source" FILES ${MAIN_SOURCES})
source_group("Widgets" FILES ${WIDGETS_SOURCES})
source_group("Models" FILES ${MODELS_SOURCES})
source_group("UI Files" FILES ${UI_SOURCES})
source_group("Resources" FILES ${UVA_RESOURCES})
source_group("Generated Files" FILES ${UI_FILES})
source_group("Generated Files" REGULAR_EXPRESSION ".*(qrc_|_automoc).*")
