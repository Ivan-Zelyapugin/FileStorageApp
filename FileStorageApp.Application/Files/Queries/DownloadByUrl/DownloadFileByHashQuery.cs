using FileStorageApp.Application.Files.Queries.DownloadFile;
using MediatR;

namespace FileStorageApp.Application.Files.Queries.DownloadByUrl
{
    public class DownloadFileByHashQuery : IRequest<DownloadFileVm>
    {
        public Uri downloadUrl { get; set; }
    }
}
