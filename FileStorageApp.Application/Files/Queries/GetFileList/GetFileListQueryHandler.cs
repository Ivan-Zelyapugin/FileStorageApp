using AutoMapper;
using FileStorageApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Files.Queries.GetFileList
{
    public class GetFileListQueryHandler
        : IRequestHandler<GetFileListQuery, FileListVm>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileListQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileListVm> Handle (GetFileListQuery request,
            CancellationToken cancellationToken)
        {
            var files = await _fileRepository.GetAllFilesAsync();

            var fileDtos = _mapper.Map<List<FileLookupDto>>(files);

            return new FileListVm { Files = fileDtos };
        }
    }
}
