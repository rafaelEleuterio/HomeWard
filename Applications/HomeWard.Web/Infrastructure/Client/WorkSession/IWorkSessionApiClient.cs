using HomeWard.Application.Dtos;
using HomeWard.Application.WorkSessions;

namespace HomeWard.Web.Infrastructure.Client.WorkSession;

public interface IWorkSessionApiClient
{
    Task<IReadOnlyList<SessionTransitionDto>> GetTransactionsByWorkSessionIdAsync(Guid workSessionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WorkSessionDto>> GetWorkSessionsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UploadDocumentResponse> UploadDocumentAsync(UploadDocumentRequest uploadDocumentRequest, CancellationToken cancellationToken = default);
    Task<DownloadDocumentResponse> DownlaodDocumentAsync(DownloadDocumentRequest downloadDocumentRequest, CancellationToken cancellationToken = default);
}
