using BlurApiServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlurApiServer.Controllers
{
    /// <summary>
    /// Controller for handling API requests
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="blurApiService"></param>
    [Route("[controller]")]
    [ApiController]
    [Produces("image/jpeg", "image/png")]
    public class BlurApiController(ILogger<BlurApiController> logger, IBlurApiService blurApiService)
        : ControllerBase
    {
        /// <summary>
        /// POST image in multipart/form-data and desired encoding type, responses with the processed image
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="encodingType"></param>
        /// <response code="200">Returns the processed image</response>
        /// <response code="400">If the POST-ed file or encoding type is not supported</response>
        /// <response code="500">If error occurs during processing the image</response>
        [HttpPost]
        [Route("blur_image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> BlurImage(IFormFile uploadedFile, EncodingType encodingType)
        {
            if (!HasImageAsContentType(uploadedFile)) return new BadRequestResult();

            await using var fileStream = uploadedFile.OpenReadStream();

            return File(
                await blurApiService.ProcessImage(fileStream, encodingType),
                GetContentTypeFor(encodingType));
        }

        private static string GetContentTypeFor(EncodingType encodingType) =>
            encodingType.Equals(EncodingType.Jpeg)
                ? "image/jpeg"
                : "image/png";

        private static bool HasImageAsContentType(IFormFile uploadedFile) =>
            uploadedFile.ContentType
                .Split("/")
                .First()
                .Equals("image");
    }
}