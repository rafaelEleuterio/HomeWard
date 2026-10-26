using HomeWard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Application.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string FirstLastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Role Role { get; set; }

    public UserDto() { }

    public UserDto(Guid id, string username, string password, string fullName, string firstLastName, string email, bool isActive, Role role)
    {
        Id = id;
        Username = username;
        Password = password;
        FullName = fullName;
        FirstLastName = firstLastName;
        Email = email;
        IsActive = isActive;
        Role = role;
    }
}