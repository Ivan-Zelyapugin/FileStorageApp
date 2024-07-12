using FileStorageApp.Application.Interfaces;
using Minio.DataModel.Args;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Persistence.Services
{
    public class MinioClientWrapper : IMinioClientWrapper
    {
        private readonly MinioClient _minioClient;

        public MinioClientWrapper(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task PutObjectAsync(string bucketName, string objectName, Stream data, long size, string contentType)
        {
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(size)
                .WithContentType(contentType));
        }
    }
}
