using System.Runtime.InteropServices;

namespace BlurApiServer.Services;

/// <summary>
/// Implementation of IBlurService using native shared library and OpenCV
/// </summary>
public class BlurApiServiceImpl : IBlurApiService
{
    [DllImport("/home/k/Development/BlurApi/BlurApiService/cmake-build-release/libBlurApiService.so",
        EntryPoint = "process_image")]
    private static extern int ProcessImageExternally(byte[] image, int imageSize, int encodingType);

    /// <summary>
    /// Blurs the image provided in stream, and encodes it to give encoding type
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="encodingType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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

        var errorCode = ProcessImageExternally(bytesWithPadding, bytesWithPadding.Length, (int)encodingType);

        switch (errorCode)
        {
            case 1:
                throw new Exception("The uploaded image can't be decoded");
            case 2:
                throw new Exception("Encoding failed");
            case 3:
                throw new Exception("The uploaded image has very too high compression ratio");
        }

        return new MemoryStream(bytesWithPadding);
    }
}