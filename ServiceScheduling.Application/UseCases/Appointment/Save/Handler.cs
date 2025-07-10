using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.Save;

public class Handler(IAppointmentRepository repository, IServiceRepository serviceRepository, IUserRepository userRepository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var clientExists = await userRepository.GetByIdAsync(request.Appointment.ClientId, cancellationToken);
        if (clientExists == null)
            return Result.Failure<Response>(new Error("404","Client not found"));
        
        var serviceExists = await serviceRepository.GetByIdAsync(request.Appointment.ServiceId, cancellationToken);
        if (serviceExists == null)
        {
            return Result.Failure<Response>(new Error("404","Service not found"));
        }

        var entity = request.Appointment.ToEntity();
        await repository.SaveAsync(entity, cancellationToken);
        
        return Result.Success(new Response());
    }
}