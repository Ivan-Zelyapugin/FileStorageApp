using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Minio;


namespace FileStorageApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
