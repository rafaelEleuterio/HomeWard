using System.ComponentModel;

namespace HomeWardDesktopApp.Domain.Enums;

public enum SessionState
{
    [Description("Não iniciado")]
    NotStarted,
    [Description("Trabalhando")]
    Working,
    [Description("Descansando")]
    Resting,
    [Description("Pausado")]
    Paused,
    [Description("Dia terminado")]
    Finished // Stop
}
