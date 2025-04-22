using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace captura.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public VideosController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet("stream/{fileName}")]
    public IActionResult StreamVideo(string fileName)
    {
        var path = Path.Combine(_env.ContentRootPath, "Videos", fileName);

        if (!System.IO.File.Exists(path))
            return NotFound("Video not found");

        var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return File(stream, "video/mp4", enableRangeProcessing: true);
    }
}
