using HomeWard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Domain.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string FirstLastName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public RoleEnum Role { get; set; }

    public UserDto() { }
    public UserDto(Guid id, string username, string password, string fullName, string firstLastName, string email, bool isActive, RoleEnum role)
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