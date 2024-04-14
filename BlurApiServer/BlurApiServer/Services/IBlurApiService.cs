using Microsoft.AspNetCore.Mvc;

namespace BlurApiServer.Services;

public interface IBlurApiService
{
    Stream ProcessImage(IFormFile uploadedFile, EncodingType encodingType);
}