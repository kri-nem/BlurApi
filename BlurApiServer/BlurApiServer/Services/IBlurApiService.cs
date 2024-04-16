namespace BlurApiServer.Services;

/// <summary>
/// Blurs the image provided in stream, and encodes it to give encoding type
/// </summary>
public interface IBlurApiService
{
    /// <summary>
    /// Blurs the image provided in stream, and encodes it to give encoding type
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="encodingType"></param>
    /// <returns></returns>
    Task<Stream> ProcessImage(Stream fileStream, EncodingType encodingType);
}