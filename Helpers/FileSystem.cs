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
            fileSystem.Add(new VirtualDirectory("Mantle Files"));
            fileSystem.Add(new VirtualDirectory("School Files"));

            fileSystem.Add(new VirtualFile("Private Keys.txt", "kdnasjkfn87h823ashfajk289"));
            fileSystem.Add(new VirtualFile("FinalAssignment.pdf", "Gena Hahn Sux"));
            fileSystem.Add(new VirtualFile("personalProject.txt"));
        }

        public static List<string> GetAllContent(string path)
        {
            List<string> allContent = new List<string>();

            Console.WriteLine($"FILE SYSTEM INITIALISED, CHECKING FOR FILES IN {path}");

            foreach (IFileSystemObject file in fileSystem)
            {
                allContent.Add(file.Name);
            }

            return allContent;
        }
    }
}
