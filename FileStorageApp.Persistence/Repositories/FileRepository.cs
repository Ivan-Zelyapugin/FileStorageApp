using Dapper;
using FileStorageApp.Application.Interfaces;
using Npgsql;
using System.Data;
using DomainFile = FileStorageApp.Domain.Entity.File;

namespace FileStorageApp.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public FileRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<DomainFile>> GetAllFilesAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<DomainFile>("SELECT * FROM files");
            }
        }

        public async Task<DomainFile> GetFileByIdAsync(Guid Id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<DomainFile>("SELECT * FROM files WHERE \"id\" = @id", new { id = Id });
            }
        }

        public async Task<DomainFile> GetFileByNameAsync(string fileName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<DomainFile>("SELECT * FROM files WHERE \"fileName\" = @fileName", new { fileName = fileName });
            }
        }

        public async Task AddFileAsync(DomainFile file)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = @"INSERT INTO files (""id"", ""fileName"", ""fileType"", ""fileSize"", ""fileUrl"", ""uploadedOn"", ""expiryDate"", ""isSingleUse"", ""isDownloaded"") 
            VALUES (@id, @Filename, @Filetype, @Filesize, @Fileurl, @Uploadedon, @Expirydate, @Issingleuse, @Isdownloaded)";

                await connection.ExecuteAsync(sql, file);
            }
        }

        public async Task UpdateFileAsync(DomainFile file)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "UPDATE files SET \"fileName\" = @fileName, \"fileType\" = @fileType, \"fileSize\" = @fileSize, \"fileUrl\" = @fileUrl, " +
                          "\"uploadedOn\" = @uploadedOn, \"expiryDate\" = @expiryDate, \"isSingleUse\" = @isSingleUse, \"isDownloaded\" = @isDownloaded " +
                          "WHERE \"id\" = @id";
                await connection.ExecuteAsync(sql, file);
            }
        }

        public async Task DeleteFileAsync(Guid id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM files WHERE \"id\" = @id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }     
    }
}
