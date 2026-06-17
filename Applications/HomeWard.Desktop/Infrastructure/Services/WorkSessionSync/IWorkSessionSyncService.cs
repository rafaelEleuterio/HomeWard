namespace HomeWard.Desktop.Infrastructure.Services.WorkSessionSync;

public interface IWorkSessionSyncService
{
    Guid? ActiveSessionId { get; }
    void Start();
    void Stop();
}