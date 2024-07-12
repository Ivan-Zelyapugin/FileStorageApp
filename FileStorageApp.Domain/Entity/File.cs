using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Domain.Entity
{
    public class File
    {
        public Guid id { get; set; } // уникальный идентификатор
        public string fileName { get; set; } = string.Empty; // имя файла
        public string fileType { get; set; } = string.Empty; // тип файла
        public long fileSize { get; set; } // размер файла в байтах
        public string fileUrl { get; set; } = string.Empty; // URL для доступа к файлу в Minio
        public DateTime uploadedOn { get; set; } // дата и время загрузки файла
        public DateTime? expiryDate { get; set; } // дата и время истечения срока хранения файла
        public bool isSingleUse { get; set; } // флаг, указывающий, является ли ссылка на файл одноразовой
        public bool isDownloaded { get; set; } // флаг, указывающий, был ли файл скачан
    }
}
