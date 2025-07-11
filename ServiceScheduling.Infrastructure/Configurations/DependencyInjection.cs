using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Domain.Interfaces;
using ServiceScheduling.Infrastructure.Aws;
using ServiceScheduling.Infrastructure.Repositories;
using ServiceScheduling.Infrastructure.Security;
using ServiceScheduling.Infrastructure.Services;

namespace ServiceScheduling.Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IServiceRepository, ServiceRepository>();
        services.AddTransient<IAppointmentRepository, AppointmentRepository>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IAwsS3Service, AwsS3Service>();
        services.AddTransient<AwsS3Settings>();
        services.AddAWSService<IAmazonS3>();

        return services;
    }
}