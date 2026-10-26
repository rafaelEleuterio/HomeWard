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

    public DocumentDto(string fileName, string contentType, long fileSize, byte[] content)
    {
        FileName = fileName;
        ContentType = contentType;
        FileSize = fileSize;
        Content = content;
    }

    public DocumentDto(Guid id, Guid transitionid, string fileName, string contentType, long fileSize, byte[] content, string justification)
        :this(fileName, contentType, fileSize, content)
    {
        Id = id;
        Transitionid = transitionid;
        Justification = justification;
    }
}
