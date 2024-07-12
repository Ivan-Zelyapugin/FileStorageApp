using FileStorageApp.Application.Interfaces;
using FileStorageApp.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace FileStorageApp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddScoped<IFileRepository>(provider => new FileRepository(connectionString));
            return services;
        }
    }
}
