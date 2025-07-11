using MediatR;
using ServiceScheduling.Application.DTOs.Appointment;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Appointment.GetAll;

public sealed record Command(int skip = 0, int take = 10) : IRequest<Result<Response>>;