using AutoMapper;
using FileStorageApp.Application.Files.Commands.DeleteCommand;
using FileStorageApp.Application.Files.Commands.UploadFile;
using FileStorageApp.Application.Files.Queries.DownloadByUrl;
using FileStorageApp.Application.Files.Queries.DownloadFile;
using FileStorageApp.Application.Files.Queries.GetFileDetails;
using FileStorageApp.Application.Files.Queries.GetFileList;
using FileStorageApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly IMapper _mapper;

        public FileController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<FileListVm>> GetAll()
        {
            var query = new GetFileListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileDetailsVm>> Get(Guid id)
        {
            var query = new GetFileDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileDto uploadFileDto)
        {
            var command = _mapper.Map<UploadFileCommand>(uploadFileDto);
            var link = await Mediator.Send(command);
            return Ok(link);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var query = new DownloadFileQuery { Id = id };
            var result = await Mediator.Send(query);
     
            if (result == null)
            {
                return NotFound();
            }

            return File(result.FileStream, result.ContentType, result.FileName);
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] string url)
        {
            var uri = new Uri(url);
            var query = new DownloadFileByHashQuery { downloadUrl = uri };
            var result = await Mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return File(result.FileStream, result.ContentType, result.FileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFileCommand { Id = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
