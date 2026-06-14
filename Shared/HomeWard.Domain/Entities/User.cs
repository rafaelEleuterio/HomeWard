using System.ComponentModel.DataAnnotations;

namespace HomeWard.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FirstLastName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
