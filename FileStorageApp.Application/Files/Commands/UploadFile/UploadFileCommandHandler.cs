using FileStorageApp.Application.Interfaces;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System.Text;

namespace FileStorageApp.Application.Files.Commands.UploadFile
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly IMinioClient _minioClient;
        private readonly IFileRepository _fileRepository;
        private const string _bucketName = "filestorage"; // бакет
        private const int expiryInSeconds = 3600 * 24 * 7; // длительность ссылки(7 дней)

        public UploadFileCommandHandler(IMinioClient minioClient, IFileRepository fileRepository)
        {
            _minioClient = minioClient;
            _fileRepository = fileRepository;
        }

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var fileName = request.file.FileName;
            var objectName = request.file.FileName;

            TimeSpan expiryDuration = request.expiryDate switch
            {
                ExpiryDuration.OneDay => TimeSpan.FromDays(1),
                ExpiryDuration.OneWeek => TimeSpan.FromDays(7),
                ExpiryDuration.OneMonth => TimeSpan.FromDays(30),
                _ => TimeSpan.FromDays(1)
            };

            try
            {
                bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
                if (!found)
                {
                    await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
                }

                using (var stream = request.file.OpenReadStream())
                {
                    await _minioClient.PutObjectAsync(new PutObjectArgs()
                        .WithBucket(_bucketName)
                        .WithObject(objectName)
                        .WithStreamData(stream)
                        .WithObjectSize(stream.Length)
                        .WithContentType(request.file.ContentType));
                }

                var file = new Domain.Entity.File
                {
                    id = Guid.NewGuid(),
                    fileName = request.file.FileName,
                    fileType = request.file.ContentType,
                    fileSize = request.file.Length,
                    fileUrl = Path.Combine(_bucketName,request.file.FileName),
                    uploadedOn = DateTime.UtcNow,
                    expiryDate = DateTime.UtcNow.Add(expiryDuration),
                    isSingleUse = request.isSingleUse,
                    isDownloaded = false
                };
           
                await _fileRepository.AddFileAsync(file);

                var downloadUrl = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithExpiry(expiryInSeconds));

                return downloadUrl;
            }
            catch (MinioException e)
            {
                throw new Exception("File upload failed", e);
            }
        }

    }
}
