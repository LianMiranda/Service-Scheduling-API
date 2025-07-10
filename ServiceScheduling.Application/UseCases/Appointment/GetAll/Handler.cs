using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.GetAll;

public class Handler(IAppointmentRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var appointments = await repository.GetAllAsync(request.skip, request.take, cancellationToken);

        if (appointments is null || appointments.Count <= 0)
            return Result.Failure<Response>(new Error("404", "Appointments not found"));
        
        var list = appointments.Select(a => a.ToDto()).ToList();
        
        return Result.Success(new Response(list));
    }
}