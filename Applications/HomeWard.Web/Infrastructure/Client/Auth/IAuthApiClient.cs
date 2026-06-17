using HomeWard.Application.Auth;

namespace HomeWard.Web.Infrastructure.Client.Auth;

public interface IAuthApiClient
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
}
