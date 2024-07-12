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
        private readonly MinioClient _minioClient;
        private readonly string _bucketName;

        public DeleteFileCommandHandler(MinioClient minioClient, IFileRepository fileRepository)
        {
            _minioClient = minioClient;
            _fileRepository = fileRepository;
            _bucketName = "fileeeee";
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileByIdAsync(request.Id);
            if (file == null)
            {
                throw new NotFoundException(nameof(DomainFile), request.Id);
            }

            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_bucketName).WithObject(file.fileName));

            await _fileRepository.DeleteFileAsync(file.id);

            return Unit.Value;
        }
    }
}
