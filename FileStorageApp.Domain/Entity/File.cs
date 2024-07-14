using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Domain.Entity
{
    public class File
    {
        public Guid id { get; set; } 
        public Guid userId { get; set; }
        public string fileName { get; set; } = string.Empty; 
        public string fileType { get; set; } = string.Empty; 
        public long fileSize { get; set; } 
        public string fileUrl { get; set; } = string.Empty; 
        public DateTime uploadedOn { get; set; } 
        public DateTime? expiryDate { get; set; } 
        public bool isSingleUse { get; set; } 
        public bool isDownloaded { get; set; } 
    }
}
