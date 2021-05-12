using backend_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Helpers
{
    public interface IFileSystemService
    {
        public List<string> GetAllContent(string path);
        public bool CreateNewVirtualFile(string path, VirtualFile file);
    }

    // Creates and manages virtual file system
    public class FileSystemService : IFileSystemService
    {
        public List<string> GetAllContent(string path)
        {
            List<string> allContent = new List<string>();
            VirtualDirectory selectedDirectory;

            // Show files in active directory
            if (path == "" || path == null)
            {
                foreach (IFileSystemObject file in FileSystem.fileSystem)
                {
                    allContent.Add(file.Name);
                }
            }
            // Show files in given path
            else
            {
                selectedDirectory = NavigateToPath(path);

                if (selectedDirectory != null)
                {
                    allContent = selectedDirectory.GetAllDirectoryContent();
                }
            }

            return allContent;
        }

        public bool CreateNewVirtualFile(string path, VirtualFile file)
        {
            // Create new file in root
            if (path == "" || path == null)
            {
                FileSystem.fileSystem.Add(file);
                return true;
            }
            // Create new file in given path
            else
            {
                VirtualDirectory selectedDirectory;
            }

            return true;
        }

        private VirtualDirectory NavigateToPath(string path)
        {
            // Path is deep
            if (path.Contains('/'))
            {
                var separatedPathNames = path.Split('/');
                VirtualDirectory targetDirectory = null;

                // Navigate to each param
                for (var i = 0; i < separatedPathNames.Length; i++)
                {
                    Console.WriteLine($"Finding directory {separatedPathNames[i]}");

                    // First execution uses root of file system
                    if (i == 0)
                    {
                        targetDirectory = GetTargetDirectory(separatedPathNames[i], FileSystem.fileSystem);
                    }
                    // Subsequent executions use contents of directory
                    else
                    {
                        targetDirectory = GetTargetDirectory(separatedPathNames[i], targetDirectory.GetAllSubFiles());
                    }

                    if (targetDirectory == null)
                    {
                        Console.WriteLine($"No {separatedPathNames[i]} directory found!");
                        return targetDirectory;
                    }
                }

                return targetDirectory;
            }
            // Simple path with only 1 level
            else
            {
                return GetTargetDirectory(path, FileSystem.fileSystem);
            }
        }

        // Helper for nested path
        private VirtualDirectory GetTargetDirectory(string path, List<IFileSystemObject> contentList)
        {
            foreach (var file in from file in contentList
                                 where file.Name.Equals(path)
                                 select file)
            {
                return (file is VirtualFile) ? null : (VirtualDirectory)file;
            }

            return null;
        }
    }
}
