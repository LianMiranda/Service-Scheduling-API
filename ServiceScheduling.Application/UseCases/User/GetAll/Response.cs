using ServiceScheduling.Application.DTOs.User;

namespace ServiceScheduling.Application.UseCases.User.GetAll;

public sealed record Response(List<ViewUserDto> users);