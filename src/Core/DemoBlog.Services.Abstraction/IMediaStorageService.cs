namespace DemoBlog.Services.Abstraction;

public interface IMediaStorageService
{
    public Task<Uri> GetMediaUrl(string blobName);

    public Task<Uri> UploadMediaStreamAsync(string blobName, Stream mediaStream, string mimeType);

    public Task<Uri> UploadMediaAsync(string blobName, string filePath);

    public Task<Stream> GetMediaStream(string blobName);

    public Task<Uri?> GetSASBlobUrl(string blobName);
}