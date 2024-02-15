using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Common.StorageFiles.Services;

namespace SmartStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController([FromServices] IStorageFileProcessingService storageFileProcessingService) : ControllerBase
{
    [HttpPost("upload")]
    public async ValueTask<IActionResult> Upload([FromForm]IFormFileCollection files)
    {
        var result = await storageFileProcessingService.UploadAsync(files);
        return Ok(result);
    }
}