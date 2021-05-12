using System.Collections.Generic;
using backend_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend_test.Helpers;

namespace backend_test.Controllers
{
    [ApiController]
    [Route("")]
    public class FSController : ControllerBase
    {
        private readonly ILogger<FSController> _logger;
        private readonly IFileSystemService _fileSystemService;

        public FSController(ILogger<FSController> logger, IFileSystemService fileSystemService)
        {
            _logger = logger;
            _fileSystemService = fileSystemService;
        }

        [HttpGet]
        public List<string> GetFiles([FromQuery] string p)
        {
            _logger.LogInformation($"Displaying all contents at path {p}");

            return _fileSystemService.GetAllContent(p);
        }

        [HttpPost]
        public VirtualFile CreateFsObject([FromQuery] string p, [FromBody] CreateFileRequest newFileReq)
        {
            _logger.LogInformation($"Creating new file at path {p}");

            VirtualFile newFile = new VirtualFile(newFileReq.Name, newFileReq.Content);

            var isCreated = _fileSystemService.CreateNewVirtualFile(p, newFile);

            return isCreated ? newFile : new VirtualFile("ERROR CREATING FILE");
        }


        [HttpPost("modify")]
        public string ModifyFsObject([FromQuery] string p)
        {
            return null;
        }

        [HttpDelete]
        public string DeleteFsObject([FromQuery] string p)
        {
            return null;
        }

        [HttpGet("content")]
        public string GetFsContent([FromQuery] string p)
        {
            return null;
        }
    }
}