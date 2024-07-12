using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FileStorageApp.Application.Files.Commands.UploadFile
{
    public class UploadFileCommand : IRequest<string>
    {
        public IFormFile file { get; set; }
        public bool isSingleUse { get; set; }
        public ExpiryDuration expiryDate { get; set; }
    }

    public enum ExpiryDuration
    {
        [Display(Name = "1 день")]
        OneDay = 1,
        [Display(Name = "1 неделя")]
        OneWeek = 7,
        [Display(Name = "1 месяц")]
        OneMonth = 30
    }
}
