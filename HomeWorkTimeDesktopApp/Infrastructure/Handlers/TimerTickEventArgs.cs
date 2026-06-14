namespace HomeWard.Desktop.Infrastructure.Handlers;

public sealed class TimerTickEventArgs : EventArgs
{
    public TimerTickEventArgs(TimeSpan timeWorkedTotal)
    {
        TimeWorkedTotal = timeWorkedTotal;
    }

    public TimeSpan TimeWorkedTotal { get; }
}
