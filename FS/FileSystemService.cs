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
        public bool DeleteFileOrFolder(string path, bool isFlagActive);
        public string GetContentOfFile(string path);
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

        public bool CreateNewVirtualFile(string path, VirtualFile newFile)
        {
            // Create new file in root
            if (path == "" || path == null)
            {
                return CreateFileIfNoDuplicate(newFile, null);
            }
            // Create new file in given path
            else
            {
                VirtualDirectory selectedDirectory = NavigateToPath(path);

                bool isCreated = CreateFileIfNoDuplicate(newFile, selectedDirectory);

                foreach (var file in selectedDirectory.GetAllSubFiles())
                {
                    Console.WriteLine(file.Name);
                }

                return isCreated;
            }
        }

        public bool DeleteFileOrFolder(string path, bool isFlagActive)
        {
            // Delete File or Directory
            if (isFlagActive)
            {
                if (path == "" || path == null || !path.Contains('/'))
                {
                    foreach (IFileSystemObject file in FileSystem.fileSystem)
                    {
                        if (file.Name == path)
                        {
                            FileSystem.fileSystem.Remove(file);
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    var separatedPathNames = path.Split('/');
                    var pathWithoutTargetArray = separatedPathNames.Take(separatedPathNames.Length - 1).ToArray();
                    var fileOrDirectoryName = separatedPathNames[separatedPathNames.Length - 1];

                    string pathWithoutTarget = "";

                    for (var i = 0; i < pathWithoutTargetArray.Length; i++)
                    {
                        if ( i == 0 )
                        {
                            pathWithoutTarget = pathWithoutTargetArray[i];
                        }
                        else
                        {
                            pathWithoutTarget += "/" + pathWithoutTargetArray[i];
                        }
                    }

                    VirtualDirectory selectedDirectory = NavigateToPath(pathWithoutTarget);

                    if (selectedDirectory != null)
                    {
                        foreach (IFileSystemObject file in selectedDirectory.GetAllSubFiles())
                        {
                            if (file.Name == fileOrDirectoryName)
                            {
                                selectedDirectory.RemoveFileOrDirectory(file);
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            // Only delete if it's a file
            else
            {
                var separatedPathNames = path.Split('/');
                var pathWithoutTargetArray = separatedPathNames.Take(separatedPathNames.Length - 1).ToArray();
                var fileOrDirectoryName = separatedPathNames[separatedPathNames.Length - 1];

                string pathWithoutTarget = "";

                for (var i = 0; i < pathWithoutTargetArray.Length; i++)
                {
                    if (i == 0)
                    {
                        pathWithoutTarget = pathWithoutTargetArray[i];
                    }
                    else
                    {
                        pathWithoutTarget += "/" + pathWithoutTargetArray[i];
                    }
                }

                VirtualDirectory selectedDirectory = NavigateToPath(pathWithoutTarget);

                if (selectedDirectory != null)
                {
                    foreach (IFileSystemObject file in selectedDirectory.GetAllSubFiles())
                    {
                        // Only delete if it's a file
                        if (file.Name == fileOrDirectoryName && file is VirtualFile)
                        {
                            selectedDirectory.RemoveFileOrDirectory(file);
                            return true;
                        }
                        // Error
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public string GetContentOfFile(string path)
        {
            if (path == "" || path == null || !path.Contains('/'))
            {
                foreach (IFileSystemObject file in FileSystem.fileSystem)
                {
                    if (file.Name == path && file is VirtualFile)
                    {
                        return ((VirtualFile)file).GetContent();
                    }
                }
                return "No matching file found";
            }
            else
            {
                var separatedPathNames = path.Split('/');
                var pathWithoutTargetArray = separatedPathNames.Take(separatedPathNames.Length - 1).ToArray();
                var fileName = separatedPathNames[separatedPathNames.Length - 1];

                string pathWithoutTarget = "";

                for (var i = 0; i < pathWithoutTargetArray.Length; i++)
                {
                    if (i == 0)
                    {
                        pathWithoutTarget = pathWithoutTargetArray[i];
                    }
                    else
                    {
                        pathWithoutTarget += "/" + pathWithoutTargetArray[i];
                    }
                }

                Console.WriteLine(pathWithoutTarget);

                VirtualDirectory selectedDirectory = NavigateToPath(pathWithoutTarget);

                if (selectedDirectory != null)
                {
                    foreach (IFileSystemObject file in selectedDirectory.GetAllSubFiles())
                    {
                        if (file.Name == fileName && file is VirtualFile)
                        {
                            return ((VirtualFile)file).GetContent();
                        }
                    }
                }
            }

            return "File not Found!";
        }

        private bool CreateFileIfNoDuplicate(VirtualFile newFile, VirtualDirectory targetDirectory)
        {
            bool fileAlreadyExist;
 
            // Use root
            if (targetDirectory == null)
            {
                fileAlreadyExist = FileSystem.fileSystem.Any(f => f.Name == newFile.Name);
            }
            else
            {
                fileAlreadyExist = targetDirectory.GetAllSubFiles().Any(f => f.Name == newFile.Name);
            }

            if (!fileAlreadyExist)
            {
                if (targetDirectory == null) {
                    FileSystem.fileSystem.Add(newFile);
                }
                else
                {
                    targetDirectory.AddVirtualFile(newFile);
                }

                return true;
            }
            else
            {
                return false;
            }
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
