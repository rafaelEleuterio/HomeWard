using HomeWard.Application.Users;
using HomeWard.Domain.Dtos;

namespace HomeWard.Web.Infrastructure.Client;

public class UserApiClient : IUserApiClient
{
    private readonly HttpClient _httpClient;

    public UserApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<UserDto>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users", cancellationToken);
        return users ?? [];
    }

    public async Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var user = await response.Content.ReadFromJsonAsync<UserDto>(cancellationToken);
        return user!;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync("api/users", userDto, cancellationToken);
        response.EnsureSuccessStatusCode();

        var user = await response.Content.ReadFromJsonAsync<UserDto>(cancellationToken);
        return user!;
    }

    public async Task SetUserStatusAsync(Guid userId, bool isActive, CancellationToken cancellationToken = default)
    {
        var request = new UpdateUserStatusRequest(userId, isActive);

        var response = await _httpClient.PatchAsJsonAsync($"api/users/{userId}/status", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
