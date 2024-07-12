using AutoMapper;
using FileStorageApp.Application.Common.Mappings;
using FileStorageApp.Application.Files.Commands.CreateFiles;

namespace FileStorageApp.WebApi.Models
{
    public class CreateFileDto : IMapWith<CreateFileCommand>
    {
        public string fileName { get; set; } = string.Empty;
        public string fileType { get; set; } = string.Empty;
        public long fileSize { get; set; }
        public string fileUrl { get; set; } = string.Empty;
        public DateTime uploadedOn { get; set; }
        public DateTime? expiryDate { get; set; }
        public bool isSingleUse { get; set; }
        public bool isDownloaded { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFileDto, CreateFileCommand>()
                .ForMember(fileCommand => fileCommand.fileName,
                    opt => opt.MapFrom(fileDto => fileDto.fileName))
                .ForMember(fileCommand => fileCommand.fileType,
                    opt => opt.MapFrom(fileDto => fileDto.fileType))
                .ForMember(fileCommand => fileCommand.fileSize,
                    opt => opt.MapFrom(fileDto => fileDto.fileSize))
                .ForMember(fileCommand => fileCommand.fileUrl,
                    opt => opt.MapFrom(fileDto => fileDto.fileUrl))
                .ForMember(fileCommand => fileCommand.uploadedOn,
                    opt => opt.MapFrom(fileDto => fileDto.uploadedOn))
                .ForMember(fileCommand => fileCommand.expiryDate,
                    opt => opt.MapFrom(fileDto => fileDto.expiryDate))
                .ForMember(fileCommand => fileCommand.isSingleUse,
                    opt => opt.MapFrom(fileDto => fileDto.isSingleUse))
                .ForMember(fileCommand => fileCommand.isDownloaded,
                    opt => opt.MapFrom(fileDto => fileDto.isDownloaded));
        }
    }
}
