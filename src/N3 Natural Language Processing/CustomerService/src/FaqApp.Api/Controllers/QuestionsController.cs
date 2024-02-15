using FaqApp.Application.KnowledgeBase.Models;
using FaqApp.Application.KnowledgeBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaqApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController(IKnowledgeBaseService knowledgeBaseService) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> Upload([FromBody] KnowledgeBaseQuestion model)
    {
        var result = await knowledgeBaseService.GetAnswerAsync(model);
        return result.IsFound ? Ok(result) : NotFound(result);
    }
}