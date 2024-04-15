using Microsoft.AspNetCore.Mvc;

namespace BlurApiServer.Services;

public class BlurApiServiceImpl : IBlurApiService
{
    public Stream ProcessImage(IFormFile uploadedFile, EncodingType encodingType)
    {
        uploadedFile.OpenReadStream();
        return uploadedFile.OpenReadStream();
    }
}