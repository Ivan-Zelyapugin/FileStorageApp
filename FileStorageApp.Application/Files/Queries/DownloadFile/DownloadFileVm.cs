using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Files.Queries.DownloadFile
{
    public class DownloadFileVm
    {
        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
