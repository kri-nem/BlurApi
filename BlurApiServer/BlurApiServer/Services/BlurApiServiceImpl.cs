using System.Runtime.InteropServices;

namespace BlurApiServer.Services;

public class BlurApiServiceImpl : IBlurApiService
{
    [DllImport("/home/k/Development/BlurApi/BlurApiService/cmake-build-release/libBlurApiService.so",
        EntryPoint = "process_image")]
    private static extern int ProcessImageExternally(byte[] src, int srcSize, int encTy);

    public Stream ProcessImage(IFormFile uploadedFile, EncodingType encodingType)
    {
        using (var stream = new MemoryStream())
        {
            uploadedFile.CopyTo(stream);
            var bytes = stream.ToArray();
            ProcessImageExternally(bytes, bytes.Length, (int)encodingType);
        }

        return uploadedFile.OpenReadStream();
    }
}