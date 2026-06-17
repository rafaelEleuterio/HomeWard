using HomeWard.Application.Repositories;
using HomeWard.Application.WorkSessions;
using HomeWard.Domain.Entities;
using HomeWard.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeWard.Api.Controllers;

[ApiController]
[Route("api/worksessions")]
[Authorize]
public class WorkSessionController : ControllerBase
{
    private readonly IWorkSessionRepository _repository;

    public WorkSessionController(IWorkSessionRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<WorkSessionResponse>> Start(StartWorkSessionRequest request, CancellationToken cancellationToken)
    {
        var session = new WorkSession
        {
            UserId = request.UserId,
            State = SessionState.Working,
        };

        var created = await _repository.CreateAsync(session, cancellationToken);

        return Ok(MapToResponse(created));
    }

    [HttpPost("{id}/transitions")]
    public async Task<ActionResult<WorkSessionResponse>> AddTransition(Guid id, AddTransitionRequest request, CancellationToken cancellationToken)
    {
        var session = await _repository.GetByIdAsync(id, cancellationToken);

        if (session is null)
            return NotFound();

        var transition = new SessionTransition
        {
            WorkSessionId = id,
            FromState = request.FromState,
            ToState = request.ToState,
            Timestamp = request.Timestamp,
            TimeElapsed = request.TimeElapsed,
        };

        await _repository.AddTransitionAsync(transition, cancellationToken);

        session.State = request.ToState;
        await _repository.UpdateAsync(session, cancellationToken);

        var updated = await _repository.GetByIdAsync(id, cancellationToken);

        return Ok(MapToResponse(updated!));
    }

    [HttpPatch("{id}/finish")]
    public async Task<ActionResult<WorkSessionResponse>> Finish(Guid id, FinishWorkSessionRequest request, CancellationToken cancellationToken)
    {
        var session = await _repository.GetByIdAsync(id, cancellationToken);

        if (session is null)
            return NotFound();

        session.State = SessionState.Finished;
        session.BillableTime = request.BillableTime;
        session.WorkedTime = request.WorkedTime;
        session.RestedTime = request.RestedTime;
        session.PausedTime = request.PausedTime;

        await _repository.UpdateAsync(session, cancellationToken);

        return Ok(MapToResponse(session));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IReadOnlyList<WorkSessionResponse>>> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        var sessions = await _repository.GetByUserAsync(userId, cancellationToken);

        return Ok(sessions.Select(MapToResponse).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkSessionResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var session = await _repository.GetByIdAsync(id, cancellationToken);

        if (session is null)
            return NotFound();

        return Ok(MapToResponse(session));
    }

    private static WorkSessionResponse MapToResponse(WorkSession session) =>
        new(session.Id, session.UserId, session.State, session.BillableTime, session.WorkedTime, session.RestedTime, session.PausedTime, session.CreatedAt,
            session.History.Select(t => new SessionTransitionResponse(
                t.Id,
                t.FromState,
                t.ToState,
                t.Timestamp,
                t.TimeElapsed)).ToList());
}