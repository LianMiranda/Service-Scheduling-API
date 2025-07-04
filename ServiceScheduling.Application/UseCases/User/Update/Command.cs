using MediatR;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.Update;

public sealed record Command(Guid Id, UpdateUserDto UserData) : IRequest<Result<Response>>;