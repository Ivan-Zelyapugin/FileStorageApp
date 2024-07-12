using FileStorageApp.Application.Interfaces;
using DomainFile = FileStorageApp.Domain.Entity.File;
using MediatR;
using FileStorageApp.Application.Common.Exceptions;

namespace FileStorageApp.Application.Files.Commands.UpdateFile
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, Unit>
    {
        private readonly IFileRepository _fileRepository;

        public UpdateFileCommandHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _fileRepository.GetFileByIdAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(DomainFile), request.Id);
            }

            var file = new DomainFile
            {
                id = request.Id,
                fileName = request.FileName,
                fileType = request.FileType,
                fileSize = request.FileSize,
                fileUrl = request.FileUrl,
                uploadedOn = request.UploadedOn,
                expiryDate = request.ExpiryDate,
                isSingleUse = request.IsSingleUse,
                isDownloaded = request.IsDownloaded
            };

            await _fileRepository.UpdateFileAsync(file);

            return Unit.Value;
        }
    }
}
