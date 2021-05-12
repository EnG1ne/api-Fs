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
            FilesAndDirectories = new List<IFileSystemObject>();
        }

        // Add a virtual file into the virtual directory
        public void AddVirtualFile(VirtualFile newFile)
        {
            FilesAndDirectories.Add(newFile);
        }
        
        // Add a virtual directory into the virtual directory
        public void AddVirtualDirectory(VirtualDirectory newDirectory)
        {
            FilesAndDirectories.Add(newDirectory);
        }

        public List<string> GetAllSubFiles()
        {
            return FilesAndDirectories.Select(file => file.Name).ToList();
        }
    }
}
