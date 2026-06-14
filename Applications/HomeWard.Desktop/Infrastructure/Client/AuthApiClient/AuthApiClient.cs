using HomeWard.Desktop.Domain.Records;
using System.Net.Http;
using System.Net.Http.Json;

namespace HomeWard.Desktop.Infrastructure.Client.AuthApiClient;

public sealed class AuthApiClient : IAuthApiClient
{
    private readonly HttpClient _httpClient;

    public AuthApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var request = new LoginRequest(email, password);

        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return new LoginResult(false, "Email ou senha inválidos.", null);
        }

        response.EnsureSuccessStatusCode();

        var user = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);

        return new LoginResult(true, null, user);
    }
}