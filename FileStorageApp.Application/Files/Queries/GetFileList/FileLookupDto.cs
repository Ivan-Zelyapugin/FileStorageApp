using AutoMapper;
using FileStorageApp.Application.Common.Mappings;
using FileStorageApp.Application.Files.Queries.GetFileDetails;
using MediatR;
using DomainFile = FileStorageApp.Domain.Entity.File;

namespace FileStorageApp.Application.Files.Queries.GetFileList
{
    public class FileLookupDto : IMapWith<DomainFile>
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DomainFile, FileLookupDto>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(file => file.id))
                .ForMember(vm => vm.FileName,
                    opt => opt.MapFrom(file => file.fileName))
                .ForMember(vm => vm.FileType,
                    opt => opt.MapFrom(file => file.fileType))
                .ForMember(vm => vm.FileSize,
                    opt => opt.MapFrom(file => file.fileSize))
                .ForMember(vm => vm.FileUrl,
                    opt => opt.MapFrom(file => file.fileUrl));
        }
    }
}
