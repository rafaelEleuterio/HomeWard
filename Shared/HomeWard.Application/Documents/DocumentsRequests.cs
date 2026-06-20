namespace HomeWard.Application.Documents;


public sealed record UploadDocumentRequest(string FileName, string ContentType, byte[] Content, string Justification);

public sealed record DownloadDocumentRequest(Guid DocumentId, Guid TransitionId, 
    string FileName, string ContentType, byte[] Content, string Justification);