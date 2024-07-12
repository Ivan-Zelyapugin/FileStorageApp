using FileStorageApp.Application.Interfaces;
using MediatR;
using Minio;
using Minio.DataModel.Args;

namespace FileStorageApp.Application.Files.Queries.DownloadFile
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileVm>
    {
        private readonly IFileRepository _fileRepository;
        private readonly MinioClient _minioClient;

        public DownloadFileQueryHandler(IFileRepository fileRepository, MinioClient minioClient)
        {
            _fileRepository = fileRepository;
            _minioClient = minioClient;
        }

        public async Task<DownloadFileVm> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileByIdAsync(request.Id);
            var bucketName = "fileeeee";
            var objectName = file.fileName;

            if (file == null || file.expiryDate < DateTime.UtcNow)
            {
                if (file != null)
                {
                    await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(file.fileName));
                    await _fileRepository.DeleteFileAsync(file.id);
                }
                throw new Exception("File not found or expired");
            }

            var memoryStream = new MemoryStream();

            try
            {
                await _minioClient.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                    }), cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return null;
            }

            return new DownloadFileVm
            {
                FileStream = memoryStream,
                ContentType = file.fileType,
                FileName = file.fileName
            };
        }
    }
}
