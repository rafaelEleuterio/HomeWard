using HomeWard.Application.WorkSessions;
using System.Net.Http;
using System.Net.Http.Json;

namespace HomeWard.Desktop.Infrastructure.Client.WorkSessionApiClient;

public class WorkSessionApiClient : IWorkSessionApiClient
{
    private readonly HttpClient _httpClient;

    public WorkSessionApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WorkSessionResponse> StartAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var request = new StartWorkSessionRequest(userId);

        var response = await _httpClient.PostAsJsonAsync("api/worksessions", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<WorkSessionResponse>(cancellationToken))!;
    }

    public async Task<WorkSessionResponse> AddTransitionAsync(AddTransitionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"api/worksessions/{request.WorkSessionId}/transitions", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<WorkSessionResponse>(cancellationToken))!;
    }

    public async Task<WorkSessionResponse> FinishAsync(FinishWorkSessionRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PatchAsJsonAsync(
            $"api/worksessions/{request.WorkSessionId}/finish", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<WorkSessionResponse>(cancellationToken))!;
    }

    public async Task<IReadOnlyList<WorkSessionResponse>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<List<WorkSessionResponse>>(
            $"api/worksessions/user/{userId}");

        return result ?? [];
    }
}