# Find includes in corresponding build directories
set(CMAKE_INCLUDE_CURRENT_DIR ON)
# Instruct CMake to run moc automatically when needed
set(CMAKE_AUTOMOC ON)

find_package(Qt5Test REQUIRED HINTS ${Qt5_HINTS_DIRS})

set(TESTS
    modelstest
    uhunttest
    pdfviewertest
    profileswidgettest
    problemswidgettest
    editorwidgettest
    liveeventswidgettest
    categorytest
    commonstest
)

foreach(TEST ${TESTS})

    add_executable(${TEST} ${TEST}.cpp)
    target_link_libraries(${TEST} ${UVA_DEPENDENCIES} Qt5::Test)
    set_target_properties(${TEST} PROPERTIES FOLDER "Tests")

endforeach()
