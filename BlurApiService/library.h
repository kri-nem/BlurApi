#ifndef BLURAPISERVICE_LIBRARY_H
#define BLURAPISERVICE_LIBRARY_H

enum EncodingType {
    JPEG, PNG
};

extern "C" {
int process_image(unsigned char *image, int imageSize, EncodingType encodingType);
};

#endif //BLURAPISERVICE_LIBRARY_H
