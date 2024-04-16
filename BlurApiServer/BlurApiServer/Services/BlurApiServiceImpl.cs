using System.Runtime.InteropServices;

namespace BlurApiServer.Services;

public class BlurApiServiceImpl : IBlurApiService
{
    [DllImport("/home/k/Development/BlurApi/BlurApiService/cmake-build-release/libBlurApiService.so",
        EntryPoint = "process_image")]
    private static extern int ProcessImageExternally(byte[] image, int imageSize, int encodingType);

    public async Task<Stream> ProcessImage(Stream fileStream, EncodingType encodingType)
    {
        byte[] bytes;

        await using (var memoryStream = new MemoryStream())
        {
            await fileStream.CopyToAsync(memoryStream);
            bytes = memoryStream.ToArray();
        }

        var bytesWithPadding = new byte[bytes.Length * 10];
        bytes.CopyTo(bytesWithPadding, 0);

        ProcessImageExternally(bytesWithPadding, bytesWithPadding.Length, (int)encodingType);

        return new MemoryStream(bytesWithPadding);
    }
}