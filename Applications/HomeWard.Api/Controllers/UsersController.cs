using HomeWard.Application.Repositories;
using HomeWard.Application.Users;
using HomeWard.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeWard.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.CreateAsync(request.user, cancellationToken);
        return Ok(user);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> SetStatus(Guid id, UpdateUserStatusRequest request, CancellationToken cancellationToken)
    {
        await _userRepository.SetActiveStatusAsync(id, request.IsActive, cancellationToken);
        return NoContent();
    }
}