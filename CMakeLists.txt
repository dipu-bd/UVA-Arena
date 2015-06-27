cmake_minimum_required(VERSION 2.8.11)
project(UVA-Arena)

set(UVA_BIN_DIR ${PROJECT_SOURCE_DIR}/bin)
set(UVA_LIB_DIR ${PROJECT_SOURCE_DIR}/lib)

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${UVA_LIB_DIR})
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${UVA_BIN_DIR})
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${UVA_BIN_DIR})

set(CMAKE_CXX_STANDARD 11)

# Find includes in corresponding build directories
set(CMAKE_INCLUDE_CURRENT_DIR ON)
# Instruct CMake to run moc automatically when needed
set(CMAKE_AUTOMOC ON)

if(NOT Qt5_CMAKE_DIR AND NOT Qt5Widgets_DIR)

    set(Qt5_CMAKE_DIR "CHANGE ME" CACHE PATH "The path to the directory containing the Qt5 cmake files")

    message(FATAL_ERROR "Set Qt5_CMAKE_DIR to the directory containing the Qt5 cmake files")

endif()

set(Qt5_HINTS_DIRS 
    ${Qt5_CMAKE_DIR}/Qt5Widgets 
)

find_package(Qt5Widgets COMPONENTS gui core widgets REQUIRED HINTS ${Qt5_HINTS_DIRS})

qt5_add_resources(UVA_RESOURCES 
    submodules/QDarkStyleSheet/qdarkstyle/style.qrc
    images/images.qrc
)

set(UVA_DEPENDENCIES ${UVA_DEPENDENCIES} Qt5::Widgets)

add_subdirectory(submodules/Qscintilla)
set(UVA_DEPENDENCIES ${UVA_DEPENDENCIES} qscintilla)
include_directories(submodules/Qscintilla/Qt4Qt5)

add_subdirectory(uhuntqt/)
set(UVA_DEPENDENCIES ${UVA_DEPENDENCIES} uhuntqt)
include_directories(uhunqt/)

add_subdirectory(src/)
set(UVA_DEPENDENCIES ${UVA_DEPENDENCIES} UVA-Arena-lib)
include_directories(src/)

add_subdirectory(tests/)

# Create the main executable

if(WIN32)

    add_executable(UVA-Arena app/main.cpp images/icon.rc ${UVA_RESOURCES})

else()
    
    add_executable(UVA-Arena app/main.cpp ${UVA_RESOURCES})

endif()

target_link_libraries(UVA-Arena ${UVA_DEPENDENCIES})


if(WIN32)

    message(STATUS "Copying qt dlls...")

    set(QT_BIN_DIR ${Qt5Widgets_DIR}/../../../bin)

    file(GLOB QT_DLLS ${QT_BIN_DIR}/*.dll)
    file(GLOB COMMON_DLLS ${QT_BIN_DIR}/ic*.dll)
    
    set(QT_DEBUG_DLLS ${COMMON_DLLS})
    set(QT_RELEASE_DLLS ${COMMON_DLLS})

    foreach(DLL ${QT_DLLS})

        message(STATUS "-- ${DLL}")
        string(REGEX MATCH ".*d.dll" IS_DEBUG ${DLL})

        if(IS_DEBUG)
            set(QT_DEBUG_DLLS ${QT_DEBUG_DLLS} ${DLL})
        else()
            set(QT_RELEASE_DLLS ${QT_RELEASE_DLLS} ${DLL})
        endif()

    endforeach()

    file(COPY ${QT_DEBUG_DLLS} DESTINATION ${UVA_BIN_DIR}/Debug)
    file(COPY ${QT_RELEASE_DLLS} DESTINATION ${UVA_BIN_DIR}/Release)

    message(STATUS "Finished copying qt dlls")

endif()
