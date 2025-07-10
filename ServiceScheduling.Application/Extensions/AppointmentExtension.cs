using ServiceScheduling.Application.DTOs.Appointment;
using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Application.Extensions;

public static class AppointmentExtension
{
    public static Appointment ToEntity(this CreateAppointmentDto dto)
    {
        return new Appointment(dto.Date, dto.Status, dto.ClientId, dto.ServiceId);
    }
}