using MediatR;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Save;

public sealed record Command(CreateServiceWithFileDto Service, string? ImagePath) : IRequest<Result<Response>>;