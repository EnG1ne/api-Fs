using backend_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Helpers
{
    // Creates and manages virtual file system
    public static class FileSystem
    {
        public static List<IFileSystemObject> fileSystem = new List<IFileSystemObject>();

        // CREATES SOME FILES AND FOLDERS FOR TESTING PURPOSES
        public static void CreateMockFileSystem()
        {
            VirtualDirectory deepDir = new VirtualDirectory("Deep");
            VirtualDirectory twoDeepDir = new VirtualDirectory("DeepTwo");
            VirtualDirectory threeDeepDir = new VirtualDirectory("DeepThree");

            deepDir.AddVirtualFile(new VirtualFile("notSoFast.txt", "nice try"));
            twoDeepDir.AddVirtualFile(new VirtualFile("Top-Secret.img", "s3xy stuff"));
            threeDeepDir.AddVirtualFile(new VirtualFile("ITS-TOO-DEEP-STOP.txt", "BLBLLLBLBLBLB"));
            threeDeepDir.AddVirtualFile(new VirtualFile("NUCLEAR-CODES.txt", "1234"));
            twoDeepDir.AddVirtualDirectory(threeDeepDir);
            deepDir.AddVirtualDirectory(twoDeepDir);

            fileSystem.Add(deepDir);
            fileSystem.Add(new VirtualDirectory("Mantle-Files"));
            fileSystem.Add(new VirtualDirectory("School-Files"));

            fileSystem.Add(new VirtualFile("Private-Keys.txt", "kdnasjkfn87h823ashfajk289"));
            fileSystem.Add(new VirtualFile("FinalAssignment.pdf", "Gena Hahn Sux"));
            fileSystem.Add(new VirtualFile("personalProject.txt"));
        }

        public static List<string> GetAllContent(string path)
        {
            List<string> allContent = new List<string>();
            VirtualDirectory selecteDirectory;

            // Show files in active directory
            if (path == "" || path == null)
            {
                foreach (IFileSystemObject file in fileSystem)
                {
                    allContent.Add(file.Name);
                }
            }
            // Show files in given path
            else
            {
                selecteDirectory = NavigateToPath(path);

                if (selecteDirectory != null)
                {
                    allContent = selecteDirectory.GetAllDirectoryContent();
                }
            }

            return allContent;
        }

        public static VirtualDirectory NavigateToPath(string path)
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
                        targetDirectory = GetTargetDirectory(separatedPathNames[i], fileSystem);
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
                return GetTargetDirectory(path, fileSystem);
            }
        }

        // Helper for nested path
        private static VirtualDirectory GetTargetDirectory(string path, List<IFileSystemObject> contentList)
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
