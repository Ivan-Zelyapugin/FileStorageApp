using AutoMapper;
using FileStorageApp.Application.Common.Exceptions;
using FileStorageApp.Application.Interfaces;
using MediatR;

namespace FileStorageApp.Application.Files.Queries.GetFileDetails
{
    public class GetFileDetailsQueryHandler
        : IRequestHandler<GetFileDetailsQuery, FileDetailsVm>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileDetailsQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileDetailsVm> Handle(GetFileDetailsQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileByIdAsync(request.Id);

            if (file == null || file.userId != request.UserId)
            {
                throw new NotFoundException(nameof(File), request.Id);
            }

            return _mapper.Map<FileDetailsVm>(file);
        }
    }
}
