using System.Security.Claims;

namespace Quizate.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetUserId(this ClaimsPrincipal user, out Guid userId)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out userId))
        {
            return false;
        }

        return true;
    }

    public static bool TryGetUserName(this ClaimsPrincipal user, out string? userName)
    {
        var usernameClaim = user.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(usernameClaim))
        {
            userName = null;
            return false;
        }

        userName = usernameClaim;
        return true;
    }
}
