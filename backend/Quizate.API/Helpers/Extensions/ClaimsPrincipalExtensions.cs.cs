using System.Security.Claims;

namespace Quizate.API.Helpers.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool GetUserId(this ClaimsPrincipal user, out Guid userId)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out userId))
            return false;

        return true;
    }
}
