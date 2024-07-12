using FileStorageApp.Application.Files.Commands.UploadFile;
using FileStorageApp.Application.Files.Queries.DownloadFile;
using FileStorageApp.Application.Interfaces;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Files.Queries.DownloadByUrl
{
    public class DownloadFileByHashQueryHandler : IRequestHandler<DownloadFileByHashQuery, DownloadFileVm>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMinioClient _minioClient;
        private const string _bucketName = "filestorage"; // бакет

        public DownloadFileByHashQueryHandler(IFileRepository fileRepository, IMinioClient minioClient)
        {
            _fileRepository = fileRepository;
            _minioClient = minioClient;
        }

        public async Task<DownloadFileVm> Handle(DownloadFileByHashQuery request, CancellationToken cancellationToken)
        {
            var objectName = request.downloadUrl.Segments[^1];
            var file = await _fileRepository.GetFileByNameAsync(objectName);

            if (file == null || file.expiryDate < DateTime.UtcNow || file.isSingleUse == true)
            {
                if (file != null)
                {
                    var fileDB = new Domain.Entity.File
                    {
                        id = file.id,
                        fileName = file.fileName,
                        fileType = file.fileName,
                        fileSize = file.fileSize,
                        fileUrl = file.fileUrl,
                        uploadedOn = file.uploadedOn,
                        expiryDate = file.expiryDate,
                        isSingleUse = file.isSingleUse,
                        isDownloaded = true
                    };

                    await _fileRepository.UpdateFileAsync(fileDB);

                    if (file.isDownloaded == true)
                    {
                        throw new Exception("The link has already been used");
                    }
                    if (file.expiryDate < DateTime.UtcNow)
                    {
                        await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                            .WithBucket(_bucketName)
                            .WithObject(file.fileName));
                        await _fileRepository.DeleteFileAsync(file.id);
                    }
                }
                else
                {
                    throw new Exception("File not found or expired");
                }
            }

            var memoryStream = new MemoryStream();

            try
            {
                await _minioClient.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(_bucketName)
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
