using Microsoft.AspNetCore.Http;

namespace ServiceScheduling.Application.Interfaces;

public interface IImageService
{
    Task<string> ReplaceImageAsync(IFormFile file, string currentKey, string serviceName, CancellationToken cancellationToken);
}