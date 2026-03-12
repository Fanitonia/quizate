using Microsoft.AspNetCore.Http;

namespace Quizate.Application.Features.Auth.Interfaces;

public interface ICookieService
{
    public void SetAccessTokenCookie(string value, int expirationMinutes, HttpResponse response);
    public void DeleteAccessTokenCookie(HttpResponse response);
    public void SetRefreshTokenCookie(string value, int expirationDays, HttpResponse response);
    public void DeleteRefreshTokenCookie(HttpResponse response);
}
