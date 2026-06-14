using HomeWorkTimeDesktopApp.Domain.Enums;
using HomeWorkTimeDesktopApp.Extensions;

namespace HomeWorkTimeDesktopApp.Domain.Models;

public class SessionTransition
{
    public DateTime Timestamp { get; init; }
    public TimeSpan TimeElapsed { get; init; }
    public SessionState FromState { get; init; }
    public SessionState ToState { get; init; }
    public string Title => GetTitle();
    public string Description => GetDescription();
    private string GetDescription()
    {
        return $"{TimeElapsed.ToString(@"hh\:mm\:ss")} - {FromState} -> {ToState}";
    }
    private string GetTitle()
    {
        return $"{Timestamp:yyyy/MM/dd HH:mm:ss} - {ToState.GetDescription()}!";
    }
}
