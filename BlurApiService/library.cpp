#include "library.h"

#include <iostream>

#include <opencv2/imgcodecs.hpp>
#include <opencv2/imgproc.hpp>

std::string getExtensionFor(EncodingType encodingType);
std::vector<int> getFlagsForEncoding(EncodingType encodingType);

int process_image(unsigned char* src, int srcSize, EncodingType encodingType)
{
    cv::Mat img = cv::imdecode({ src, srcSize }, cv::IMREAD_COLOR);
    if (img.empty()) return 1;
    cv::GaussianBlur(img, img, { 31, 31 }, 0, 0);
    std::vector<unsigned char> buf;
    cv::imencode(getExtensionFor(encodingType), img, buf, getFlagsForEncoding(encodingType));

    for (int i = 0; i < buf.size() && i < srcSize; i++)
    {
        src[i] = buf[i];
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
