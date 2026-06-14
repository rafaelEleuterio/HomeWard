using HomeWard.Application.Auth;
using HomeWard.Web.Insfrastructure;
using Microsoft.AspNetCore.Identity.Data;
using System.Net.Http.Json;
namespace HomeWard.WebApp.Infrastructure.Client;

public class AuthApiClient : IAuthApiClient
{
    private readonly HttpClient _httpClient;

    public AuthApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var request = new Application.Auth.LoginRequest(username, password);

        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return new LoginResult(false, null, "Usuário ou senha inválidos.");
        }

        response.EnsureSuccessStatusCode();

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);

        return new LoginResult(true, loginResponse?.User, loginResponse?.Token);
    }
}
