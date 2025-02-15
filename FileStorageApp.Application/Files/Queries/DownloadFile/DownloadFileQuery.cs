﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Files.Queries.DownloadFile
{
    public class DownloadFileQuery : IRequest<DownloadFileVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
