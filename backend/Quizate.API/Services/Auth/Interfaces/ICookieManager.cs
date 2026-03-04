namespace Quizate.API.Services.Auth;

public interface ICookieManager
{
    public void SetAccessTokenCookie(string value, int expirationMinutes, HttpResponse response);
    public void RemoveAccessTokenCookie(HttpResponse response);
    public void SetRefreshTokenCookie(string value, int expirationDays, HttpResponse response);
    public void RemoveRefreshTokenCookie(HttpResponse response);
}
