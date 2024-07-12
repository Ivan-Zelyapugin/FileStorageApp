using MediatR;

namespace FileStorageApp.Application.Files.Commands.UpdateFile
{
    public class UpdateFileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedOn { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsSingleUse { get; set; }
        public bool IsDownloaded { get; set; }
    }
}
