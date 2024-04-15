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
        public ActionResult BlurImage(IFormFile uploadedFile, EncodingType encodingType)
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