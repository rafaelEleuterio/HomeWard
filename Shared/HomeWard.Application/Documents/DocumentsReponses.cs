namespace HomeWard.Application.Documents;

public sealed record UploadDocumentResponse(Guid Id, Guid SessionTransitionid, string FileName, string ContentType,
    long FileSize, byte[] Content, string Justification);
public sealed record DownloadDocumentResponse(string FileName, string ContentType, long FileSize, byte[] Content);