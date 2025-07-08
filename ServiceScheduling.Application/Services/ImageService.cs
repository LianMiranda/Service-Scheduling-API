using Microsoft.AspNetCore.Http;
using ServiceScheduling.Application.Interfaces;

namespace ServiceScheduling.Application.Services;

public class ImageService : IImageService
{
    private readonly IAwsS3Service _awsS3Service;

    public ImageService(IAwsS3Service awsS3Service)
    {
        _awsS3Service = awsS3Service;
    }

    public async Task<string> ReplaceImageAsync(IFormFile file, string currentKey, string serviceName, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(currentKey))
        {
            await _awsS3Service.DeleteFileAsync(currentKey, cancellationToken);
        }
        
        string key = serviceName + "-" + Guid.NewGuid();
        
        using var stream = file.OpenReadStream();
        
        await _awsS3Service.UploadFileAsync(stream, key, file.ContentType, cancellationToken);

        return key;
    }
}