using backend_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Helpers
{
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

            FileSystem.fileSystem.Add(deepDir);
            FileSystem.fileSystem.Add(new VirtualDirectory("Mantle-Files"));
            FileSystem.fileSystem.Add(new VirtualDirectory("School-Files"));

            FileSystem.fileSystem.Add(new VirtualFile("Private-Keys.txt", "kdnasjkfn87h823ashfajk289"));
            FileSystem.fileSystem.Add(new VirtualFile("FinalAssignment.pdf", "Gena Hahn Sux"));
            FileSystem.fileSystem.Add(new VirtualFile("personalProject.txt"));
        }
    }
}
