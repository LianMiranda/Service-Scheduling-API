using ServiceScheduling.Application.DTOs.Service;

namespace ServiceScheduling.Application.UseCases.Service.GetById;

public sealed record Response(ViewServiceWithProviderDto Service);