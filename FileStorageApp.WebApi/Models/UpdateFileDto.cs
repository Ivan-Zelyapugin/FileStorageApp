using AutoMapper;
using FileStorageApp.Application.Common.Mappings;
using FileStorageApp.Application.Files.Commands.UpdateFile;

namespace FileStorageApp.WebApi.Models
{
    public class UpdateFileDto : IMapWith<UpdateFileCommand>
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedOn { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsSingleUse { get; set; }
        public bool IsDownloaded { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFileDto, UpdateFileCommand>()
                .ForMember(fileCommand => fileCommand.Id,
                    opt => opt.MapFrom(fileDto => fileDto.Id))
                .ForMember(fileCommand => fileCommand.FileName,
                    opt => opt.MapFrom(fileDto => fileDto.FileName))
                .ForMember(fileCommand => fileCommand.FileType,
                    opt => opt.MapFrom(fileDto => fileDto.FileType))
                .ForMember(fileCommand => fileCommand.FileSize,
                    opt => opt.MapFrom(fileDto => fileDto.FileSize))
                .ForMember(fileCommand => fileCommand.FileUrl,
                    opt => opt.MapFrom(fileDto => fileDto.FileUrl))
                .ForMember(fileCommand => fileCommand.UploadedOn,
                    opt => opt.MapFrom(fileDto => fileDto.UploadedOn))
                .ForMember(fileCommand => fileCommand.ExpiryDate,
                    opt => opt.MapFrom(fileDto => fileDto.ExpiryDate))
                .ForMember(fileCommand => fileCommand.IsSingleUse,
                    opt => opt.MapFrom(fileDto => fileDto.IsSingleUse))
                .ForMember(fileCommand => fileCommand.IsDownloaded,
                    opt => opt.MapFrom(fileDto => fileDto.IsDownloaded));
        }
    }
}
