using BikeUsers.Application.BikeUsages.Brokers;
using BikeUsers.Application.BikeUsages.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeUsers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikeUsagesController : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> GetUsages([FromBody] BikeUsageRequest request, [FromServices]IBikeUsagePredictionApiBroker bikeUsageBroker)
    {
        var result = await bikeUsageBroker.GetPredictedUsersAsync(request);
        return Ok(result!.Results.First());
    }
}