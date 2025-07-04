using ServiceScheduling.Application.DTOs.User;

namespace ServiceScheduling.Application.UseCases.User.GetById;

public sealed record Response(ViewUserDto User);