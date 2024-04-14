using BlurApiServer.Services;
using Microsoft.AspNetCore.Http;
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
            return File(uploadedFile.OpenReadStream(), "image/jpeg");
        }
    }
}