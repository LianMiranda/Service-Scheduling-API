using MediatR;
using ServiceScheduling.Application.DTOs;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.Delete;

public sealed record Command(Guid UserId) : IRequest<Result<Response>>;