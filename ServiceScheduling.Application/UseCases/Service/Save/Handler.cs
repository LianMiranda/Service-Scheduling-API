using MediatR;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Save;

public class Handler(IServiceRepository serviceRepository, IUserRepository userRepository)
    : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var verifyUserRole = await userRepository.GetByIdAsync(request.Service.ProviderId, cancellationToken);

        if (verifyUserRole?.ProfileId != 2)
            return Result.Failure<Response>(new Error("401", "you are not authorized"));

        var service = request.Service.ToCreateServiceDto(request.ImagePath);
        var entity = service.ToEntity();

        await serviceRepository.SaveAsync(entity, cancellationToken);
        return Result.Success(new Response());
    }
}