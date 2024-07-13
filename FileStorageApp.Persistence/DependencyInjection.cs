using DbUp.Engine.Transactions;
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
            services.AddScoped<IDbConnectionFactory>( sp =>
                new NpgsqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IFileRepository, FileRepository>();

            return services;
        }
    }
}
