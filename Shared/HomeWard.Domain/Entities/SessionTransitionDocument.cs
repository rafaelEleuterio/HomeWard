using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Domain.Entities;

public class SessionTransitionDocument
{
    public Guid Id { get; set; }
    public Guid SessionTransitionId { get; set; }
    public SessionTransition SessionTransition { get; set; } = null!;

    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public byte[] Content { get; set; } = [];

    public string Justification { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
