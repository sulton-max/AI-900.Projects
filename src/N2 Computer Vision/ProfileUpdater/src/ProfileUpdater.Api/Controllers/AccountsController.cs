using Microsoft.AspNetCore.Mvc;
using ProfileUpdater.Application.Common.Identity.Services;

namespace ProfileUpdater.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(IAccountAggregationService accountService) : ControllerBase
{
    [HttpPost("upload")]
    public async ValueTask<IActionResult> Upload([FromForm] IFormFileCollection files)
    {
        var result = await accountService.UploadProfilePhotoAsync(files.First());
        return Ok(result);
    }
}