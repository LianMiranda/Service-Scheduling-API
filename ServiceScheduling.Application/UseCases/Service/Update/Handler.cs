using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Update;

public sealed class Handler(IServiceRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var service = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (service == null) return Result.Failure<Response>(new Error("404", "Service not found"));
        if (!string.IsNullOrWhiteSpace(request.ServiceData.Name)) service.UpdateName(request.ServiceData.Name);
        if (!string.IsNullOrWhiteSpace(request.ServiceData.Description))
            service.UpdateDescription(request.ServiceData.Description);
        if (!string.IsNullOrWhiteSpace(request.ServiceData.ImageUrl))
            service.UpdateImageUrl(request.ServiceData.ImageUrl);
        if (!double.IsNaN(request.ServiceData.Price.Value)) service.UpdatePrice(request.ServiceData.Price.Value);
        if (Guid.TryParse(request.ServiceData.ProviderId, out Guid providerId)) service.UpdateProviderId(providerId);


        await repository.UpdateAsync(service, cancellationToken);

        return Result.Success(new Response());
    }
}