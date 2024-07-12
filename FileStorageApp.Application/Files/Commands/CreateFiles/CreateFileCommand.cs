using MediatR;

namespace FileStorageApp.Application.Files.Commands.CreateFiles
{
    public class CreateFileCommand : IRequest<Guid>
    {
        public string fileName { get; set; } = string.Empty; 
        public string fileType { get; set; } = string.Empty;
        public long fileSize { get; set; } 
        public string fileUrl { get; set; } = string.Empty; 
        public DateTime uploadedOn { get; set; } 
        public DateTime? expiryDate { get; set; } 
        public bool isSingleUse { get; set; } 
        public bool isDownloaded { get; set; }
    }
}
