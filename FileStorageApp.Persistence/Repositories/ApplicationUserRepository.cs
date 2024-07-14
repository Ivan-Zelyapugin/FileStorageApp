using Dapper;
using FileStorageApp.Application.Interfaces;
using FileStorageApp.Domain.Entity;

namespace FileStorageApp.Persistence.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ApplicationUserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<ApplicationUser>("SELECT * FROM users");
            }
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid Id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ApplicationUser>("SELECT * FROM users WHERE \"Id\" = @Id", new { id = Id });
            }
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string Email)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ApplicationUser>("SELECT * FROM users WHERE \"Email\" = @Email", new { Email = Email });
            }
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "INSERT INTO users (\"Id\", \"Name\", \"Email\", \"Password\") VALUES (@Id, @Name, @Email, @Password)";

                await connection.ExecuteAsync(sql, user);
            }
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "UPDATE users SET \"Name\" = @Name, \"Email\" = @Email, \"Password\" = @Password WHERE \"Id\" = @Id";
                await connection.ExecuteAsync(sql, user);
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM users WHERE \"Id\" = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }      
    }
}
