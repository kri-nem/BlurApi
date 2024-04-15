cmake_minimum_required(VERSION 3.22)
project(BlurApiService)

set(CMAKE_CXX_STANDARD 17)

find_package(OpenCV REQUIRED)

include_directories(${OpenCV_INCLUDE_DIRS})

add_library(BlurApiService SHARED library.cpp
)

target_link_libraries(BlurApiService ${OpenCV_LIBS})
