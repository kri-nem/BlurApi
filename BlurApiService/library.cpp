#include "library.h"

#include <iostream>

#include <opencv2/imgcodecs.hpp>
#include <opencv2/imgproc.hpp>

std::string getExtensionFor(EncodingType encodingType);
std::vector<int> getFlagsForEncoding(EncodingType encodingType);

int process_image(unsigned char* image, int imageSize, EncodingType encodingType)
{
    cv::Mat decodedImage = cv::imdecode(
        { image, imageSize },
        cv::IMREAD_COLOR);

    if (decodedImage.empty()) return 1;

    cv::GaussianBlur(
        decodedImage,
        decodedImage,
        { 31, 31 },
        0,
        0);

    std::vector<unsigned char> buffer;

    bool isEncodingSuccessful = cv::imencode(
        getExtensionFor(encodingType),
        decodedImage,
        buffer,
        getFlagsForEncoding(encodingType));

    if(!isEncodingSuccessful) return 2;

    bool isBufferTooSmallForEncodedImage = buffer.size() > imageSize;

    if(isBufferTooSmallForEncodedImage) return 3;

    for (int i = 0; i < buffer.size(); i++)
    {
        image[i] = buffer[i];
    }

    return 0;

}

std::vector<int> getFlagsForEncoding(EncodingType encodingType)
{
    switch (encodingType)
    {
    case JPEG:
        return std::vector<int>{ cv::IMWRITE_JPEG_OPTIMIZE, 20 };
    case PNG:
        return std::vector<int>{ cv::IMWRITE_PNG_COMPRESSION, 7 };
    default:
        return std::vector<int>{};
    }
}

std::string getExtensionFor(EncodingType encodingType)
{
    switch (encodingType)
    {
    case JPEG:
        return ".jpeg";
    case PNG:
        return ".png";
    default:
        return ".jpeg";
    }
}
