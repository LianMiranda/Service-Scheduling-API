using MediatR;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Delete;

public sealed class Handler(IServiceRepository repository, IAwsS3Service s3Service)
    : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var serviceExists = await repository.GetByIdAsync(request.serviceId, cancellationToken);

        if (serviceExists == null) return Result.Failure<Response>(new Error("404", "User not found"));

        if (!string.IsNullOrWhiteSpace(serviceExists.ImageUrl))
        {
            await s3Service.DeleteFileAsync(serviceExists.ImageUrl, cancellationToken);
        }

        await repository.DeleteAsync(serviceExists, cancellationToken);
        return Result.Success(new Response());
    }
}