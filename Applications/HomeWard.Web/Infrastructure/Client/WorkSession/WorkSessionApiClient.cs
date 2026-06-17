using HomeWard.Application.Dtos;
using HomeWard.Application.WorkSessions;
using HomeWard.Domain.Dtos;
using HomeWard.Domain.Entities;

namespace HomeWard.Web.Infrastructure.Client.WorkSession;

public class WorkSessionApiClient : IWorkSessionApiClient
{
    private readonly HttpClient _httpClient;

    public WorkSessionApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

    }

    public async Task<IReadOnlyList<SessionTransitionDto>> GetTransactionsByWorkSessionIdAsync(Guid workSessionId, CancellationToken cancellationToken = default)
    {
        var transitions = await _httpClient.GetFromJsonAsync<List<SessionTransitionDto>>($"api/worksessions/{workSessionId}/transactions", cancellationToken);

        return transitions;
    }

    public async Task<IReadOnlyList<WorkSessionDto>> GetWorkSessionsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var workSession = await _httpClient.GetFromJsonAsync<List<WorkSessionDto>>($"api/worksessions/user/{userId}", cancellationToken);

        return workSession;
    }

    public async Task<UploadDocumentResponse> UploadDocumentAsync(UploadDocumentRequest uploadDocumentRequest, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<DownloadDocumentResponse> DownlaodDocumentAsync(DownloadDocumentRequest downloadDocumentRequest, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
