using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Files.Queries.GetFileList
{
    public class FileListVm
    {
        public IList<FileLookupDto> Files { get; set; }
    }
}
