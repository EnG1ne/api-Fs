using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Models
{
    public class VirtualDirectory : IFileSystemObject
    {
        public string Name { get; set; }

        public List<VirtualFile> Files { get; set; }
    }
}
