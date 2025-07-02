using Microsoft.Extensions.DependencyInjection;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infra.Repositories;
using ServiceScheduling.Infra.Security;

namespace ServiceScheduling.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        return services;
    }
}