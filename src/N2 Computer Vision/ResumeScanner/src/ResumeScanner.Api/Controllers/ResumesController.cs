using Microsoft.AspNetCore.Mvc;
using ResumeScanner.Application.Resumes.Services;

namespace ResumeScanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumesController(IResumeProcessingService resumeProcessingService) : ControllerBase
{
    [HttpPost("upload")]
    public async ValueTask<IActionResult> Upload([FromForm] IFormFileCollection files)
    {
        var result = await resumeProcessingService.UploadResumeAsync(files.First());
        return Ok(result);
    }
}