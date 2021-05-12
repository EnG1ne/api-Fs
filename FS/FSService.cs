using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using backend_test.Helpers;

namespace backend_test.Models
{
    public interface IFSService
    {
        List<string> GetAllFilesInPath(string path);
    }

    public class FSService : IFSService
    {
        public List<string> GetAllFilesInPath(string path)
        {
            return FileSystem.GetAllContent(path);
        }
    }
}
