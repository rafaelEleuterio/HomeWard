using HomeWard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Domain.Entities;

public class WorkSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public SessionState State { get; set; }

    public TimeSpan BillableTime { get; set; }
    public TimeSpan WorkedTime { get; set; }
    public TimeSpan RestedTime { get; set; }
    public TimeSpan PausedTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<SessionTransition> History { get; } = [];
}
