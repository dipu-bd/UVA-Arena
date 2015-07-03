set(TARGET uhuntqt)

# Find includes in corresponding build directories
set(CMAKE_INCLUDE_CURRENT_DIR ON)
# Instruct CMake to run moc automatically when needed
set(CMAKE_AUTOMOC ON)

find_package(Qt5Network REQUIRED HINTS ${Qt5_CMAKE_DIR})

set(HEADERS
    categorynode.h
    contextbook.h
    enums.h
    judgestatus.h
    probleminfo.h
    uhuntqt.h
    uhuntqt_global.h
    userinfo.h
    userranklist.h
    usersubmission.h
    submissionmessage.h
)

set(SOURCES
    categorynode.cpp
    contextbook.cpp
    judgestatus.cpp
    probleminfo.cpp
    submissionmessage.cpp
    uhuntqt.cpp
    userinfo.cpp
    userranklist.cpp
    usersubmission.cpp
)

option(${TARGET}_SHARED "Build ${TARGET} as a shared library." ON)
if(${TARGET}_SHARED)
    add_definitions(-DUHUNTQT_SHARED)
    add_library(${TARGET} SHARED ${HEADERS} ${SOURCES})
else()
    add_definitions(-DUHUNTQT_STATIC)
    add_library(${TARGET} STATIC ${HEADERS} ${SOURCES})
endif()

target_link_libraries(${TARGET} Qt5::Network)
set_target_properties(${TARGET} PROPERTIES FOLDER ${TARGET})
