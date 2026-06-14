using HomeWard.Desktop.Domain.Records;

namespace HomeWard.Desktop.Infrastructure.Client.AuthApiClient;

public interface IAuthApiClient
{
    Task<LoginResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}