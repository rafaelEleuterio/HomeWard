using HomeWard.Domain.Enums;
using HomeWard.Domain.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWard.Domain.Entities;

public class SessionTransition
{
    public Guid Id { get; set; }
    public Guid WorkSessionId { get; set; }
    public WorkSession WorkSession { get; set; } = null!;

    public DateTime Timestamp { get; set; }
    public TimeSpan TimeElapsed { get; set; }

    public SessionState FromState { get; set; }
    public SessionState ToState { get; set; }
    public DateTime CreatedAt { get; set; }

    [NotMapped]
    public string Title => $"{Timestamp:yyyy/MM/dd HH:mm:ss} - {ToState.GetDescription()}!";
    [NotMapped]
    public string Description => $"{TimeElapsed.ToString(@"hh\:mm\:ss")} - {FromState} -> {ToState}";
}
