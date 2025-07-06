using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Infrastructure.Aws;

namespace ServiceScheduling.Infrastructure.Services;

public class AwsS3Service : IAwsS3Service
{
    private readonly IAmazonS3 _client;
    private readonly string _bucketName = string.Empty;

    private AwsS3Service(IAmazonS3 client)
    {
        _client = client;
    }

    public AwsS3Service(IOptions<AwsS3Settings> options)
    {
        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(options.Value.Region)
        };

        _client = new AmazonS3Client(options.Value.AccessKey, options.Value.SecretKey, config);
        _bucketName = options.Value.BucketName;
    }

    public async Task UploadFileAsync(Stream file, string key, string contentType,
        CancellationToken cancellationToken = default)
    {
        var req = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = file,
            ContentType = contentType
        };

        await _client.PutObjectAsync(req, cancellationToken);
    }

    public async Task DeleteFileAsync(string key, CancellationToken cancellationToken = default)
    {
        var req = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        await _client.DeleteObjectAsync(req, cancellationToken);
    }

    public string GetFileUrl(string key, CancellationToken cancellationToken = default)
    {
        return $"https://{_bucketName}.s3.amazonaws.com/{key}";
    }
}