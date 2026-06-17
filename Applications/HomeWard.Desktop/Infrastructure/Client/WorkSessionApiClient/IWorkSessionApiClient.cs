using HomeWard.Application.WorkSessions;
using HomeWard.Domain.Enums;

namespace HomeWard.Desktop.Infrastructure.Client.WorkSessionApiClient;

public interface IWorkSessionApiClient
{
    Task<WorkSessionResponse> StartAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<WorkSessionResponse> AddTransitionAsync(AddTransitionRequest request, CancellationToken cancellationToken = default);
    Task<WorkSessionResponse> FinishAsync(FinishWorkSessionRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WorkSessionResponse>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}