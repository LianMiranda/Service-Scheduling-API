using ServiceScheduling.Application.DTOs.Service;

namespace ServiceScheduling.Application.UseCases.Service.GetAll;

public sealed record Response(List<ViewServiceWithProviderDto> services);