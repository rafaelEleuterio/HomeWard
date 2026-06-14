using HomeWard.Application.Auth;

namespace HomeWard.Web.Insfrastructure;

public interface IAuthApiClient
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
}
