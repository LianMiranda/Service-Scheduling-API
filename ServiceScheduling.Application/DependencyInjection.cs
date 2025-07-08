using Microsoft.Extensions.DependencyInjection;
using ServiceScheduling.Application.Interfaces;
using ServiceScheduling.Application.Services;

namespace ServiceScheduling.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddScoped<IImageService, ImageService>();
        
        return services;
    }
}