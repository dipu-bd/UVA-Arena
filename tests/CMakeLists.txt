# Find includes in corresponding build directories
set(CMAKE_INCLUDE_CURRENT_DIR ON)
# Instruct CMake to run moc automatically when needed
set(CMAKE_AUTOMOC ON)

set(TESTS
    modelstest
    uhuntqttest
    profileswidgettest
    problemswidgettest
    codeswidgettest
    judgestatuswidgettest
)

foreach(TEST ${TESTS})

    add_executable(${TEST} ${TEST}.cpp)
    target_link_libraries(${TEST} ${UVA_DEPENDENCIES})
    set_target_properties(${TEST} PROPERTIES FOLDER "Tests")

endforeach()
