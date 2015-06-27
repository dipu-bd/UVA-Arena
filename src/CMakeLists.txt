set(TARGET UVA-Arena-lib)

# Find includes in corresponding build directories
set(CMAKE_INCLUDE_CURRENT_DIR ON)
# Instruct CMake to run moc automatically when needed
set(CMAKE_AUTOMOC ON)

set(HEADERS
    mainwindow.h
    src_global.h
    models/arenatablemodel.h
    widgets/codeswidget.h
    widgets/judgestatuswidget.h
    widgets/problemswidget.h
    widgets/profileswidget.h
)

set(SOURCES
    mainwindow.cpp
    models/arenatablemodel.cpp
    widgets/codeswidget.cpp
    widgets/judgestatuswidget.cpp
    widgets/problemswidget.cpp
    widgets/profileswidget.cpp
)

qt5_wrap_ui(UI_FILES
    mainwindow.ui
    widgets/codeswidget.ui
    widgets/judgestatuswidget.ui
    widgets/problemswidget.ui
    widgets/profileswidget.ui
)

option(${TARGET}_LIBRARY "Build ${TARGET} as a shared library." ON)
if(${TARGET}_LIBRARY)
    add_definitions(-DSRC_LIBRARY)
    add_library(${TARGET} SHARED ${HEADERS} ${SOURCES} ${UI_FILES} ${UVA_RESOURCES})
else()
    add_library(${TARGET} STATIC ${HEADERS} ${SOURCES} ${UI_FILES} ${UVA_RESOURCES})
endif()

target_link_libraries(${TARGET} ${UVA_DEPENDENCIES})
set_target_properties(${TARGET} PROPERTIES FOLDER "UVA-Arena")
