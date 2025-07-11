using MediatR;
using ServiceScheduling.Application.Extensions;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.GetById;

public class Handler(IAppointmentRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        if (request.AppointmentId == Guid.Empty)
            return Result.Failure<Response>(new Error("400", "Id not defined"));

        var appointment = await repository.GetByIdAsync(request.AppointmentId, cancellationToken);

        return appointment is null
            ? Result.Failure<Response>(new Error("404", "Appointment not found"))
            : Result.Success(new Response(appointment.ToDto()));
    }
}