# BlurApi
An ASP.NET Core Web API for blurring images and encoding them to JPEG or
PNG. Decoding, blurring, and encoding of the image is implemented in C++
using OpenCV library.   

I created this project as part of an assignment. Although it is my first
ASP.NET project, it was quite surprising how self-explanatory it is, having
experience in Spring & C++.

# Documentation
The code has XML documentation in code. The API is documented with OpenAPI.
The easiest way of getting familiar with it is by building and running the
project. SwaggerUI is turned on by default and documents the API extensively.

# Building and running the project
The project's primary target is Linux. I developed and tested it on Ubuntu
22.04. I'm quite sure You can run in WSL too, but I haven't tried it.

## Step-by-step guide
Clone the repository:
```bash
git clone git@github.com:kri-nem/BlurApi.git
```
Then build the C++ library from the ./BlurApu/BlurApiService/ directory. To do
this, you need CMake, g++, and OpenCV library and headers installed
on your system. To build and run the server part, we need dotnet-sdk-8.0:
```bash
sudo apt update && sudo apt -y install build-essential cmake libopencv-dev dotnet-sdk-8.0
```
After installing the dependencies you can build libBlurApiService.so:
```bash
cd BlurApi/BlurApiService/
mkdir build
cmake -DCMAKE_BUILD_TYPE=Release -S . -B ./build/
cmake --build ./build/
```
The shared object is in the ./build/ directory, we are going to need it later.
Let's build the server:
```bash
cd ../BlurApiServer/BlurApiServer/
dotnet build --configuration Release --runtime linux-x64
```
We need the shared object to be visible to the built dll. You can move it
to a convenient location, like /usr/local/lib/, and add it to your PATH, or
simply move it next to the dll:
```bash
mv ../../BlurApiService/build/libBlurApiService.so ./bin/Release/net8.0/linux-x64/
```
Now you can run the API with the command:
```bash
dotnet run ./bin/Release/net8.0/linux-x64/BlurApiServer.dll
```

In the terminal, you should see the URL of the running API, something like
http://localhost:5128  
SwaggerUI with the API documentation can be visited at: http://localhost:5128/swagger
