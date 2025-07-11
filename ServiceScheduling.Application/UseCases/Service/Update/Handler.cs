using MediatR;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Update;

public sealed class Handler(IServiceRepository repository, IImageService imageService)
    : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var file = request.ServiceData.Image;
        var service = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (service == null) return Result.Failure<Response>(new Error("404", "Service not found"));
        if (!string.IsNullOrWhiteSpace(request.ServiceData.Name)) service.UpdateName(request.ServiceData.Name);
        if (!string.IsNullOrWhiteSpace(request.ServiceData.Description))
            service.UpdateDescription(request.ServiceData.Description);
        if (file != null && file.Length > 0)
        {
            string? key;

            if (!string.IsNullOrWhiteSpace(request.ServiceData.Name))
            {
                key = await imageService.ReplaceImageAsync(file, service.ImageUrl, request.ServiceData.Name,
                    cancellationToken);
            }
            else
            {
                key = await imageService.ReplaceImageAsync(file, service.ImageUrl, service.Name, cancellationToken);
            }

            service.UpdateImageUrl(key);
        }

        if (request.ServiceData.Price != null)
        {
            if (request.ServiceData.Price.Value > 0)
            {
                service.UpdatePrice(request.ServiceData.Price.Value);
            }
            else
            {
                return Result.Failure<Response>(new Error("400", "Price cannot have a negative value"));
            }
        }
        if (Guid.TryParse(request.ServiceData.ProviderId, out Guid providerId)) service.UpdateProviderId(providerId);

        await repository.UpdateAsync(service, cancellationToken);

        return Result.Success(new Response());
    }
}