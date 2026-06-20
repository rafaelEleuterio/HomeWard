using HomeWard.Domain.Enums;

namespace HomeWard.Application.Users;

public sealed record GetUser(Guid userid, string username, string fullName, string firstLastName, string email, bool isActive, Role role);
