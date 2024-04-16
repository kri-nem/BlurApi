using BlurApiServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlurApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlurApiController(ILogger<BlurApiController> logger, IBlurApiService blurApiService)
        : ControllerBase
    {
        [HttpPost]
        [Route("blur_image")]
        public async Task<ActionResult> BlurImage(IFormFile uploadedFile, EncodingType encodingType)
        {
            if (!HasImageAsContentType(uploadedFile)) return new BadRequestResult();
            await using var fileStream = uploadedFile.OpenReadStream();
            return File(await blurApiService.ProcessImage(fileStream, encodingType), GetContentTypeFor(encodingType));
        }

        private static string GetContentTypeFor(EncodingType encodingType) =>
            encodingType.Equals(EncodingType.Jpeg) ? "image/jpeg" : "image/png";

        private static bool HasImageAsContentType(IFormFile uploadedFile) =>
            uploadedFile.ContentType.Split("/").First().Equals("image");
    }
}