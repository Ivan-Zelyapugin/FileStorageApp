using DbUp;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace FileStorageApp.WebApi.Migrations
{
    public class MigrationService : IMigrationService
    {
        private readonly IConfiguration _configuration;

        public MigrationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RunMigrations()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                throw new Exception("Database migration failed", result.Error);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database migration successful!");
            Console.ResetColor();
        }
    }
}
