using System.Runtime.InteropServices;

namespace BlurApiServer.Services;

public class BlurApiServiceImpl : IBlurApiService
{
    [DllImport("/home/k/Development/BlurApi/BlurApiService/cmake-build-release/libBlurApiService.so",
        EntryPoint = "process_image")]
    private static extern int ProcessImageExternally(byte[] image, int imageSize, int encodingType);

    public Stream ProcessImage(IFormFile uploadedFile, EncodingType encodingType)
    {
        byte[] bytes;
        byte[] bytesWithPadding;

        using (var stream = new MemoryStream())
        {
            uploadedFile.CopyTo(stream);
            bytes = stream.ToArray();
        }
        
        bytesWithPadding = new byte[bytes.Length * 10];
        bytes.CopyTo(bytesWithPadding, 0);

        ProcessImageExternally(bytesWithPadding, bytes.Length, (int)encodingType);

        return new MemoryStream(bytesWithPadding);
    }
}