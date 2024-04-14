cmake_minimum_required(VERSION 3.28)
project(libBlurApiService)

set(CMAKE_CXX_STANDARD 17)

add_library(libBlurApiService SHARED library.cpp)
