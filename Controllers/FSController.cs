using System.Collections.Generic;
using backend_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend_test.Controllers
{
    [ApiController]
    [Route("")]
    public class FSController : ControllerBase
    {
        private readonly ILogger<FSController> _logger;
        private readonly IFSService _fsService;

        public FSController(ILogger<FSController> logger, IFSService fsService)
        {
            _logger = logger;
            _fsService = fsService;
        }

        [HttpGet]
        public List<string> GetFiles([FromQuery] string p)
        {
            List<string> list = _fsService.GetAllFilesInPath(p);

            return list;
        }

        [HttpPost]
        public string CreateFsObject([FromQuery] string p)
        {

            return null;
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