using ServiceScheduling.Domain.Enums;

namespace ServiceScheduling.Application.DTOs.Appointment;

public class UpdateAppointmentDto
{
     public DateTime? Date { get; set; }
     public AppointmentStatus? Status { get; set; }
     public Guid? ClientId { get; set; }
     public Guid? ServiceId { get; set; }

     public UpdateAppointmentDto()
     {
     }

     public UpdateAppointmentDto(DateTime? date, AppointmentStatus? status, Guid? clientId, Guid? serviceId)
     {
          Date = date;
          Status = status;
          ClientId = clientId;
          ServiceId = serviceId;
     }
}