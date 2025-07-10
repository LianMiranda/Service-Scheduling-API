using MediatR;
using ServiceScheduling.Application.DTOs.Appointment;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.Update;

public sealed record Command(Guid AppointmentId, UpdateAppointmentDto AppointmentData) : IRequest<Result<Response>>;