using FeedbackAnalyzer.Application.Ratings.Commands;
using FeedbackAnalyzer.Application.Ratings.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingsController(IRatingProcessingService ratingProcessingService) : ControllerBase
{
    [HttpPost("upload")]
    public async ValueTask<IActionResult> Upload([FromBody] CreateRatingCommand command)
    {
        var result = await ratingProcessingService.CreateRatingAsync(command);
        return Ok(result);
    }
}