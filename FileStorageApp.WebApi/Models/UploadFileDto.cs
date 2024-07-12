using AutoMapper;
using FileStorageApp.Application.Common.Mappings;
using FileStorageApp.Application.Files.Commands.UploadFile;

namespace FileStorageApp.WebApi.Models
{
    public class UploadFileDto : IMapWith<UploadFileCommand>
    {
        public IFormFile file { get; set; }
        public bool isSingleUse { get; set; }
        public ExpiryDuration expiryDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UploadFileDto, UploadFileCommand>()
                .ForMember(fileCommand => fileCommand.file,
                    opt => opt.MapFrom(fileDto => fileDto.file))
                .ForMember(fileCommand => fileCommand.isSingleUse,
                    opt => opt.MapFrom(fileDto => fileDto.isSingleUse))
                .ForMember(fileCommand => fileCommand.expiryDate,
                    opt => opt.MapFrom(fileDto => fileDto.expiryDate));
        }
    }  
}
