using MediatR;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.GetAll;

public class Handler(IServiceRepository repository) : IRequestHandler<Command,Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
       var services = await repository.GetAllAsync(request.Skip, request.Take, cancellationToken);

       if (services is null || services.Count == 0) return Result.Failure<Response>(new Error("404", $"Services not found"));

       var servicesView = services.Select(s => s.ToDto()).ToList();
       
       return Result.Success(new Response(servicesView));
    }
}