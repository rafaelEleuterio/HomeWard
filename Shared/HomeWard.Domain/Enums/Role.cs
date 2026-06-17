namespace HomeWard.Domain.Enums;

/// <summary>
/// IMPORTANTE: nunca reordene ou insira valores no meio deste enum.
/// Sempre adicione novos valores no final com o próximo inteiro disponível.
/// Valores existentes são persistidos no banco e alterar sua ordem corrompe o histórico.
/// </summary>
public enum Role
{
    Director = 1,
    Manager = 2,
    User = 3
}
