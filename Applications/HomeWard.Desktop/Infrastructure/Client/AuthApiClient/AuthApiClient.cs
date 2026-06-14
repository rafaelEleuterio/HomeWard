using HomeWard.Application.Auth;
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

    public async Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var request = new LoginRequest(username, password);

        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return new LoginResult(false, null, "Email ou senha inválidos.");
        }

        response.EnsureSuccessStatusCode();

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);

        return new LoginResult(true, loginResponse.User, null);
    }
}