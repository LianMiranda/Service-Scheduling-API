using ServiceScheduling.Application.DTOs;

namespace ServiceScheduling.Application.UseCases.User.GetAll;

public sealed record Response(List<ViewUserDto> users);