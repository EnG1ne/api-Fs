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
        public string ModifyFsObject([FromQuery] string p, [FromBody] ModifyFileRequest contentReq)
        {
            _logger.LogInformation($"Modifying new file at path {p}");

            return _fileSystemService.ModifyContentOfFile(p, contentReq.Content);
        }

        [HttpDelete]
        public string DeleteFsObject([FromQuery] string p, [FromQuery] string flag)
        {
            _logger.LogInformation($"Deleting file at path {p}");
            bool deleteCompleted;

            // Permission to delete directory if path contains
            if (flag == "-r")
            {
               deleteCompleted = _fileSystemService.DeleteFileOrFolder(p, true);
            }
            else
            {
                deleteCompleted = _fileSystemService.DeleteFileOrFolder(p, false);
            }


            return deleteCompleted ? "Delete Successful!" : "Delete Failed";

        }

        [HttpGet("content")]
        public string GetFsContent([FromQuery] string p)
        {
            _logger.LogInformation($"Getting content of file at path {p}");

            return _fileSystemService.GetContentOfFile(p);
        }
    }
}