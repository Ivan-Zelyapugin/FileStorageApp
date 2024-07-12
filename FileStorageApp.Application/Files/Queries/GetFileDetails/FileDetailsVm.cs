using AutoMapper;
using FileStorageApp.Application.Common.Mappings;
using DomainFile = FileStorageApp.Domain.Entity.File;

namespace FileStorageApp.Application.Files.Queries.GetFileDetails
{
    public class FileDetailsVm : IMapWith<DomainFile>
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string FileUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DomainFile, FileDetailsVm>()

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
