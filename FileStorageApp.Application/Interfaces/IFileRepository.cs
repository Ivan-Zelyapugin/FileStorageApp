using DomainFile = FileStorageApp.Domain.Entity.File;

namespace FileStorageApp.Application.Interfaces
{
    public interface IFileRepository
    {
        Task<IEnumerable<DomainFile>> GetAllFilesAsync();
        Task<DomainFile> GetFileByIdAsync(Guid id);
        Task<DomainFile> GetFileByNameAsync(string name);
        Task AddFileAsync(DomainFile file);
        Task UpdateFileAsync(DomainFile file);
        Task DeleteFileAsync(Guid id);
    }
}