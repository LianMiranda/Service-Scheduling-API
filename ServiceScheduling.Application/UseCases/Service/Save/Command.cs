using MediatR;
using ServiceScheduling.Application.DTOs.Service;
using ServiceScheduling.Domain.Interfaces;

namespace ServiceScheduling.Application.UseCases.Service.Save;

public sealed record Command(CreateServiceDto Service) : IRequest<Result<Response>>;