using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizate.Application.Common.Errors;
using Quizate.Application.Features.Languages.DTOs.Requests;
using Quizate.Application.Features.Languages.DTOs.Responses;
using Quizate.Application.Features.Languages.Interfaces;
using Quizate.Domain.Enums;

namespace Quizate.API.Controllers;

[Route("languages")]
[ApiController]
public class LanguageController(ILanguageQueryService queryService, ILanguageCommandService commandService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<LanguageResponse>>> GetLanguages(CancellationToken ct, [FromQuery] bool includeQuizCount = false)
    {
        var result = await queryService.GetAllAsync(ct, includeQuizCount);
        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    public async Task<ActionResult> CreateLanguage([FromBody] CreateLanguageRequest request)
    {
        var result = await commandService.CreateAsync(request);
        if (result.IsFailure)
            return BadRequest(result.Error);
        return Ok();
    }

    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpDelete]
    [Route("{code}")]
    public async Task<ActionResult> DeleteLanguage([FromRoute] string code)
    {
        var result = await commandService.DeleteAsync(code);
        if (result.IsFailure)
        {
            if (result.Error == CommonErrors.NotFound)
                return NotFound();

            return BadRequest(result.Error);
        }

        return Ok();
    }
}
