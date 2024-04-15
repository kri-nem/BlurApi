cmake_minimum_required(VERSION 3.28)
project(BlurApiService)

set(CMAKE_CXX_STANDARD 17)

add_library(BlurApiService SHARED library.cpp)
