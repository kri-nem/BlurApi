#include "library.h"

#include <iostream>

#include <opencv2/imgcodecs.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/highgui.hpp>

std::string getExtensionFor(EncodingType encodingType);


int process_image(unsigned char *src, int srcSize, EncodingType encodingType) {
    cv::Mat img = cv::imdecode({src, srcSize}, cv::IMREAD_COLOR);
    if (img.empty()) return 1;
    cv::GaussianBlur(img, img, {31, 31}, 0, 0);

    // For testing purposes
    /*
    cv::imshow("Display window", img);
    cv::waitKey(0);
    */

    std::vector<unsigned char> buf;
    cv::imencode(getExtensionFor(encodingType), img, buf);
    return 0;

}

std::string getExtensionFor(EncodingType encodingType) {
    switch (encodingType) {
        case JPEG:
            return ".jpeg";
        case PNG:
            return ".png";
        default: return ".jpeg";
    }
}
