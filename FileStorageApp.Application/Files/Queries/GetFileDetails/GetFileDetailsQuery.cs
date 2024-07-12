using MediatR;

namespace FileStorageApp.Application.Files.Queries.GetFileDetails
{
    public class GetFileDetailsQuery : IRequest<FileDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
