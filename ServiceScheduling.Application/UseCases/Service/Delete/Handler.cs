using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Delete;

public sealed class Handler(IServiceRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var serviceExists = await repository.GetByIdAsync(request.serviceId, cancellationToken);
        
        if(serviceExists == null) return Result.Failure<Response>(new Error("404", "User not found"));
        
        await repository.DeleteAsync(serviceExists, cancellationToken);
        return Result.Success(new Response());
    }
}