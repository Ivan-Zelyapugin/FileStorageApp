using MediatR;

namespace FileStorageApp.Application.Files.Queries.GetFileList
{
    public class GetFileListQuery : IRequest<FileListVm>
    {
    }
}
