using FileStorageApp.Application.Common.Exceptions;
using FileStorageApp.Application.Interfaces;
using MediatR;
using Minio;
using Minio.DataModel.Args;
using DomainFile = FileStorageApp.Domain.Entity.File;

namespace FileStorageApp.Application.Files.Commands.DeleteCommand
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMinioClient _minioClient;
        private const string _bucketName = "filestorage"; // бакет

        public DeleteFileCommandHandler(IMinioClient minioClient, IFileRepository fileRepository)
        {
            _minioClient = minioClient;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileByIdAsync(request.Id);
            if (file == null || file.userId != request.UserId)
            {
                throw new NotFoundException(nameof(DomainFile), request.Id);
            }

            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_bucketName).WithObject(file.fileName));

            await _fileRepository.DeleteFileAsync(file.id);

            return Unit.Value;
        }
    }
}
