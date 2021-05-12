using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Models
{
    public class VirtualFile : IFileSystemObject
    {
        public string Name { get; set; }

        public string Content { get; set; }
    }
}
