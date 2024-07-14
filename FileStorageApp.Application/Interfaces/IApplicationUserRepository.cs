using FileStorageApp.Domain.Entity;

namespace FileStorageApp.Application.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(Guid id);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(Guid id);
    }
}
