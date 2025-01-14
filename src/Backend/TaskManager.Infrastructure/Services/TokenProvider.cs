using Microsoft.AspNetCore.Http;
using TaskManager.Domain.Services;

namespace TaskManager.Infrastructure.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Value()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            throw new UnauthorizedAccessException("No valid token provided.");
        }

        return authorizationHeader.Substring("Bearer ".Length).Trim();
    }
}