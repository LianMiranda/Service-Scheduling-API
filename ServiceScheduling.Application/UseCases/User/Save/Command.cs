using MediatR;
using ServiceScheduling.Application.DTOs.User;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.User.Save;

public sealed record Command(CreateUserDto User) : IRequest<Result<Response>>;