﻿using MoviesRate.Domain.Security.Tokens.Provider;

namespace MoviesRate.API.Tokens;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor) => _contextAccessor = httpContextAccessor;

    public string Value()
    {
        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization["Bearer ".Length..].Trim();
    }
}
