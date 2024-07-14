using AutoMapper;
using FileStorageApp.Application.Files.Commands.DeleteCommand;
using FileStorageApp.Application.Files.Commands.UploadFile;
using FileStorageApp.Application.Files.Queries.DownloadByUrl;
using FileStorageApp.Application.Files.Queries.DownloadFile;
using FileStorageApp.Application.Files.Queries.GetFileDetails;
using FileStorageApp.Application.Files.Queries.GetFileList;
using FileStorageApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorageApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly IMapper _mapper;

        public FileController(IMapper mapper) => _mapper = mapper;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<FileListVm>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new GetFileListQuery();
            query.UserId = Guid.Parse(userId);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<FileDetailsVm>> Get(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new GetFileDetailsQuery
            {
                Id = id,
                UserId = Guid.Parse(userId)
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileDto uploadFileDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var command = _mapper.Map<UploadFileCommand>(uploadFileDto);

            command.userId = Guid.Parse(userId);
            

            var link = await Mediator.Send(command);
            return Ok(link);
        }

        [Authorize]
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new DownloadFileQuery { Id = id };
            query.UserId = Guid.Parse(userId);
            var result = await Mediator.Send(query);
     
            if (result == null)
            {
                return NotFound();
            }

            return File(result.FileStream, result.ContentType, result.FileName);
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var command = new DeleteFileCommand 
            { 
                Id = id,
                UserId = Guid.Parse(userId)
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
