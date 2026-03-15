namespace Quizate.API.Extensions;

public static class HttpResponseExtensions
{
    public static void SetCookie(
        this HttpResponse response,
        string key, string value,
        IConfiguration configuration)
    {
        switch (key)
        {
            case Cookies.AccessToken:
                response.SetAccessTokenCookie(value, configuration);
                break;
            case Cookies.RefreshToken:
                response.SetRefreshTokenCookie(value, configuration);
                break;
            default:
                throw new NotSupportedException($"The key '{key}' is not supported.");
        }
    }

    public static void DeleteCookie(this HttpResponse response, string key)
    {
        switch (key)
        {
            case Cookies.AccessToken:
                response.DeleteAccessTokenCookie();
                break;
            case Cookies.RefreshToken:
                response.DeleteRefreshTokenCookie();
                break;
            default:
                throw new NotSupportedException($"The key '{key}' is not supported.");
        }
    }

    public static void SetHeader(this HttpResponse response, string key, string value)
    {
        response.Headers.Append(key, value);
    }

    private static void SetAccessTokenCookie(this HttpResponse response, string value, IConfiguration configuration)
    {
        int expirationMinutes = configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes");

        response.Cookies.Append(Cookies.AccessToken, value, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes)
        });
    }

    private static void SetRefreshTokenCookie(this HttpResponse response, string value, IConfiguration configuration)
    {
        int expirationDays = configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays");

        response.Cookies.Append(Cookies.RefreshToken, value, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(expirationDays),
            Path = "/auth"
        });
    }

    private static void DeleteAccessTokenCookie(this HttpResponse response)
    {
        response.Cookies.Delete(Cookies.AccessToken);
    }

    private static void DeleteRefreshTokenCookie(this HttpResponse response)
    {
        response.Cookies.Delete(Cookies.RefreshToken, new CookieOptions
        {
            Path = "/auth"
        });
    }
}

public class Cookies
{
    public const string AccessToken = "ACCESS_TOKEN";
    public const string RefreshToken = "REFRESH_TOKEN";
}

public class Headers
{
    public const string XPagination = "X-Pagination";
}