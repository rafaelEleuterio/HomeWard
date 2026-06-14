using HomeWard.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Desktop.Infrastructure.Services.UserService;

public interface IUserService
{
    UserDto? User { get; }
    bool IsAuthenticated { get; }
    event EventHandler? UserChanged;
    void SetUser(UserDto user);
    void Clear();
}
