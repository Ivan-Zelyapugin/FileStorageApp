using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Interfaces
{
    public interface IMinioClientWrapper
    {
        Task PutObjectAsync(string bucketName, string objectName, Stream data, long size, string contentType);
    }
}
