using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.Application.Common.Errors;
using Quizate.Application.Features.Topics.DTOs.Requests;
using Quizate.Application.Features.Topics.DTOs.Responses;
using Quizate.Application.Features.Topics.Interfaces;

namespace Quizate.API.Controllers;

[Route("topics")]
[ApiController]
public class TopicController(
    ITopicQueryService topicQuery,
    ITopicCommandService topicCommand) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<TopicResponse>>> GetTopics(CancellationToken ct)
    {
        var result = await topicQuery.GetTopicsAsync(ct);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{topicName}")]
    public async Task<ActionResult> UpdateTopic([FromRoute] string topicName, [FromBody] UpdateTopicRequest request)
    {
        var result = await topicCommand.UpdateTopicAsync(request, topicName);

        if (result.IsFailure)
        {
            if (result.Error == CommonErrors.NotFound)
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<TopicResponse>> CreateTopic(CreateTopicRequest request)
    {
        var result = await topicCommand.CreateTopicAsync(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Created();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{topicName}")]
    public async Task<ActionResult> DeleteTopic([FromRoute] string topicName)
    {
        var result = await topicCommand.DeleteTopicAsync(topicName);

        if (result.IsFailure)
        {
            if (result.Error == CommonErrors.NotFound)
                return NotFound(result.Error);

            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
