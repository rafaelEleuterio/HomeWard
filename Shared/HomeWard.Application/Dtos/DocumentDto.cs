using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWard.Application.Dtos;

public class DocumentDto
{
    public Guid Id { get; set; }
    public Guid Transitionid { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public byte[] Content { get; set; } = [];

    public string Justification { get; set; } = string.Empty;

    public DocumentDto() { }

    public DocumentDto(Guid id, Guid transitionid, string fileName, string contentType, long fileSize, byte[] content, string justification)
    {
        Id = id;
        Transitionid = transitionid;
        FileName = fileName;
        ContentType = contentType;
        FileSize = fileSize;
        Content = content;
        Justification = justification;
    }
}
