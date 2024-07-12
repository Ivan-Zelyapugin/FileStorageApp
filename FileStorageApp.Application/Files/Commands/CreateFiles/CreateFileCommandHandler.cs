using MediatR;
using FileStorageApp.Application.Interfaces;
using DomainFile = FileStorageApp.Domain.Entity.File;

namespace FileStorageApp.Application.Files.Commands.CreateFiles
{
    // логика создания
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, Guid>
    {
        private readonly IFileRepository _fileRepository;

        public CreateFileCommandHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<Guid> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var file = new DomainFile
            {
                id = Guid.NewGuid(),
                fileName = request.fileName,
                fileType = request.fileType,
                fileSize = request.fileSize,
                fileUrl = request.fileUrl,
                uploadedOn = request.uploadedOn,
                expiryDate = request.expiryDate,
                isSingleUse = request.isSingleUse,
                isDownloaded = request.isDownloaded
            };

            await _fileRepository.AddFileAsync(file);

            return file.id;
        }
    }
}
