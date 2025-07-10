using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.Delete;

public class Handler(IAppointmentRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request.AppointmentId == Guid.Empty)
            return Result.Failure<Response>(new Error("400", "Id not defined"));

        var appointment = await repository.GetByIdAsync(request.AppointmentId, cancellationToken);

        if (appointment is null)
            return Result.Failure<Response>(new Error("404", "Appointment not found"));
        
        await repository.DeleteAsync(appointment, cancellationToken);
        return Result.Success(new Response());
    }
}