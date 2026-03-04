namespace Quizate.API.Services.Auth;

public class CookieManager : ICookieManager
{
    public void SetAccessTokenCookie(string value, int expirationMinutes, HttpResponse response)
    {
        response.Cookies.Append("ACCESS_TOKEN", value, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes)
        });
    }

    public void SetRefreshTokenCookie(string value, int expirationDays, HttpResponse response)
    {
        response.Cookies.Append("REFRESH_TOKEN", value, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(expirationDays),
            Path = "/auth/refreshToken"
        });
    }

    public void RemoveAccessTokenCookie(HttpResponse response)
    {
        response.Cookies.Delete("ACCESS_TOKEN");
    }

    public void RemoveRefreshTokenCookie(HttpResponse response)
    {
        response.Cookies.Delete("REFRESH_TOKEN");
    }
}
