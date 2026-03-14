using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.API.Extensions;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Interfaces;

namespace Quizate.API.Controllers;

[Route("topics")]
[ApiController]
public class TopicController(ITopicService topicService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<TopicResponse>>> GetTopics()
    {
        var result = await topicService.GetTopicsAsync();

        return Ok(result);
    }

    [Authorize]
    [HttpPatch("{topicName}")]
    public async Task<ActionResult> UpdateTopic([FromRoute] string topicName, [FromBody] UpdateTopicRequest request)
    {
        var result = await topicService.UpdateTopic(request, topicName);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "topicErrors");
            return ValidationProblem();
        }

        return NoContent();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TopicResponse>> CreateTopic(CreateTopicRequest request)
    {
        var result = await topicService.CreateTopic(request);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "topicErrors");
            return ValidationProblem();
        }

        return Created();
    }

    [Authorize]
    [HttpDelete("{topicName}")]
    public async Task<ActionResult> DeleteTopic([FromRoute] string topicName)
    {
        var result = await topicService.DeleteTopic(topicName);

        if (result.IsFailure)
        {
            result.AddErrorsToModelState(ModelState, "topicErrors");
            return ValidationProblem();
        }

        return NoContent();
    }
}
