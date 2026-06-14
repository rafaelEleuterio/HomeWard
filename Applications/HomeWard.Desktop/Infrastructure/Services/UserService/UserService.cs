using HomeWard.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Desktop.Infrastructure.Services.UserService;

public class UserService : IUserService
{
    public UserDto? User { get; private set; }
    public bool IsAuthenticated => User is not null;

    public event EventHandler? UserChanged;

    public void SetUser(UserDto user)
    {
        User = user;
        UserChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Clear()
    {
        User = null;
        UserChanged?.Invoke(this, EventArgs.Empty);
    }
}
