set(TARGET UVA-Arena-lib)

# Find includes in corresponding build directories
set(CMAKE_INCLUDE_CURRENT_DIR ON)
# Instruct CMake to run moc automatically when needed
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)

set(HEADERS
    uvaarenawidget.h
    mainwindow.h
    uvalib_global.h
    models/arenatablemodel.h
    models/modelstyle.h
    widgets/codeswidget.h
    widgets/judgestatuswidget.h
    widgets/problemswidget.h
    widgets/profileswidget.h
    uhunt/categorynode.h
    uhunt/enums.h
    uhunt/judgestatus.h
    uhunt/problem.h
    uhunt/uhunt.h
    uhunt/userinfo.h
    uhunt/rankinfo.h
    uhunt/usersubmission.h
    uhunt/submissionmessage.h
)

set(SOURCES
    uvaarenawidget.cpp
    mainwindow.cpp
    models/arenatablemodel.cpp
    models/modelstyle.cpp
    widgets/codeswidget.cpp
    widgets/judgestatuswidget.cpp
    widgets/problemswidget.cpp
    widgets/profileswidget.cpp
    uhunt/categorynode.cpp
    uhunt/judgestatus.cpp
    uhunt/problem.cpp
    uhunt/submissionmessage.cpp
    uhunt/uhunt.cpp
    uhunt/userinfo.cpp
    uhunt/rankinfo.cpp
    uhunt/usersubmission.cpp
)

qt5_wrap_ui(UI_FILES
    mainwindow.ui
    widgets/codeswidget.ui
    widgets/judgestatuswidget.ui
    widgets/problemswidget.ui
    widgets/profileswidget.ui
)

option(${TARGET}_SHARED "Build ${TARGET} as a shared library." ON)
if(${TARGET}_SHARED)
    add_definitions(-DUVA_SHARED)
    add_library(${TARGET} SHARED ${HEADERS} ${SOURCES} ${UI_FILES} ${UVA_RESOURCES})
else()
    add_definitions(-DUVA_STATIC)
    add_library(${TARGET} STATIC ${HEADERS} ${SOURCES} ${UI_FILES} ${UVA_RESOURCES})
endif()

target_link_libraries(${TARGET} ${UVA_DEPENDENCIES})
set_target_properties(${TARGET} PROPERTIES FOLDER "UVA-Arena")
