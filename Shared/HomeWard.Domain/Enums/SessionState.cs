using System.ComponentModel;

/// <summary>
/// IMPORTANTE: nunca reordene ou insira valores no meio deste enum.
/// Sempre adicione novos valores no final com o próximo inteiro disponível.
/// Valores existentes são persistidos no banco e alterar sua ordem corrompe o histórico.
/// </summary>
namespace HomeWard.Domain.Enums;
public enum SessionState
{
    [Description("Not started")]
    NotStarted = 1,
    [Description("Working")]
    Working = 2,
    [Description("Resting")]
    Resting = 3,
    [Description("Paused (Not Billable)")]
    Paused = 4,
    [Description("Finished")]
    Finished = 5
}
