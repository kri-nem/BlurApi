using BlurApiServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

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
            if (HasImageAsContentType(uploadedFile))
            {
                return File(blurApiService.ProcessImage(uploadedFile, encodingType), "image/jpeg");
            }

            return new BadRequestResult();
        }

        private static bool HasImageAsContentType(IFormFile uploadedFile)
        {
            return uploadedFile.ContentType.Split("/").First().Equals("image");
        }
    }
}