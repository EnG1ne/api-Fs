using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Models
{
    public class VirtualDirectory : IFileSystemObject
    {
        public string Name { get; set; }

        private List<IFileSystemObject> FilesAndDirectories;

        // Create a virtual directory
        public VirtualDirectory(string name)
        {
            Name = name;
        }

        public void AddVirtualFile(VirtualFile newFile)
        {
            FilesAndDirectories.Add(newFile);
        }
        
        public void AddVirtualDirectory(VirtualDirectory newDirectory)
        {
            FilesAndDirectories.Add(newDirectory);
        }
    }
}
