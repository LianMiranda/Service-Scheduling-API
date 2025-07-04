using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.GetById;

public class Handler(IServiceRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var service = await repository.GetByIdAsync(request.Id, cancellationToken);

        return service is null
            ? Result.Failure<Response>(new Error("404", $"Service not found"))
            : Result.Success(new Response(service.ToDto()));
    }
}