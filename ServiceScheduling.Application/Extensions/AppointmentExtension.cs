using ServiceScheduling.Application.DTOs.Appointment;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Entities;

namespace ServiceScheduling.Application.Extensions;

public static class AppointmentExtension
{
    public static Appointment ToEntity(this CreateAppointmentDto dto)
    {
        return new Appointment(dto.Date, dto.ClientId, dto.ServiceId);
    }
    
    public static ViewAppointmentDto ToDto(this Appointment appointment)
    {
        return new ViewAppointmentDto
        {
            Id = appointment.Id,
            Date = appointment.Date,
            Status = appointment.Status,  
            Client = new ViewLimitedUserDto(appointment.Client.Name, appointment.Client.Email),
            Service = new ViewLimitedServiceDto(appointment.Service.Name, appointment.Service.Description, appointment.Service.Price)
        };
    }
}