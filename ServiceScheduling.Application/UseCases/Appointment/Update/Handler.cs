using MediatR;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.Update;

public class Handler(IAppointmentRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var appointmentData = request.AppointmentData;
        if (request.AppointmentId == Guid.Empty)
            return Result.Failure<Response>(new Error("400", "Id not defined"));

        var appointment = await repository.GetByIdAsync(request.AppointmentId, cancellationToken);

        if (appointment is null)
            return Result.Failure<Response>(new Error("404", "Appointment not found"));
        
        if(appointmentData.Date.HasValue) appointment.UpdateDate(appointmentData.Date.Value);
        if(appointmentData.Status.HasValue) appointment.UpdateStatus(appointmentData.Status.Value);
        if(appointmentData.ServiceId.HasValue) appointment.UpdateServiceId(appointmentData.ServiceId.Value);
        if(appointmentData.ClientId.HasValue) appointment.UpdateClientId(appointmentData.ClientId.Value);
        
        await repository.UpdateAsync(appointment, cancellationToken);

        return Result.Success(new Response());
    }
}