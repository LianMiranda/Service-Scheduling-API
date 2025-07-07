using Microsoft.AspNetCore.Http;

namespace ServiceScheduling.Application.Interfaces;

public interface IAwsS3Service
{
    Task UploadFileAsync(Stream file, string key, string contentType, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string key, CancellationToken cancellationToken = default);
    Task<Stream> DownloadFileAsync(string key, CancellationToken cancellationToken = default);
    string GetFileUrl(string key, CancellationToken cancellationToken = default);
}