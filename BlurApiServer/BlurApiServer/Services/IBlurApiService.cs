namespace BlurApiServer.Services;

public interface IBlurApiService
{
    Task<Stream> ProcessImage(Stream fileStream, EncodingType encodingType);
}