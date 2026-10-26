using HomeWard.Domain.Enums;

namespace HomeWard.Application.Users;

public sealed record GetUserResposnse(Guid userid, string username, string fullName, string firstLastName, string email, bool isActive, Role role);
