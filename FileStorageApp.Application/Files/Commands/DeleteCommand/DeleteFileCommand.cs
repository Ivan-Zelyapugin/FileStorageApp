using MediatR;

namespace FileStorageApp.Application.Files.Commands.DeleteCommand
{
    public class DeleteFileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
