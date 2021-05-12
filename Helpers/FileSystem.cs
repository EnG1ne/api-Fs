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

            twoDeepDir.AddVirtualFile(new VirtualFile("Top-Secret.img", "s3xy stuff"));
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
                    allContent = selecteDirectory.GetAllSubFiles();
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

                //Console.WriteLine(separatedPathNames);
            }
            // Simple path with only 1 level
            else
            {
                foreach (var file in fileSystem)
                {
                    // File/Directory Found
                    if (file.Name.Equals(path))
                    {
                        if (file is VirtualFile)
                        {
                            Console.WriteLine("This command can only be ran on Folders!");
                            return null;
                        }
                        else
                        {
                            return (VirtualDirectory) file;
                        }
                    }
                }                
            }

            return null;
        }
    }
}
