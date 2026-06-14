using System.Net.Http.Headers;

namespace HomeWard.Web.Infrastructure.Authentication;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;

        Console.WriteLine($"AuthHeaderHandler - Token found: {!string.IsNullOrEmpty(token)}");
        Console.WriteLine($"AuthHeaderHandler - User authenticated: {_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated}");

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
