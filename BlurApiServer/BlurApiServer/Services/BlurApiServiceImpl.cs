using Microsoft.AspNetCore.Mvc;

namespace BlurApiServer.Services;

public class BlurApiServiceImpl : IBlurApiService
{
    [DllImport("/home/k/Development/BlurApi/BlurApiService/cmake-build-release/libBlurApiService.so",
        EntryPoint = "process_image")]
    private static extern int ProcessImageExternally(byte[] src, int srcSize, int encTy);

    public Stream ProcessImage(IFormFile uploadedFile, EncodingType encodingType)
    {
        uploadedFile.OpenReadStream();
        return uploadedFile.OpenReadStream();
    }
}