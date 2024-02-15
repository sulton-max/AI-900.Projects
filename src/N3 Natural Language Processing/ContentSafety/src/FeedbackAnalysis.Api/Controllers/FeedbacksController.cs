using FeedbackAnalysis.Application.Feedbacks.Services;
using FeedbackAnalysis.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalysis.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbacksController(IFeedbackProcessingService feedbackProcessingService) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> CreateAsync([FromBody] Feedback feedback)
    {
        var result = await feedbackProcessingService.CreateAsync(feedback);
        return Ok(result.ConfirmationAction.ToString());
    }
}