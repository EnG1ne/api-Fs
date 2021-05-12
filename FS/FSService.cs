using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace backend_test.Models
{
    public interface IFSService
    {
        List<string> GetAllFilesInPath(string path);
    }

    public class FSService : IFSService
    {
        public List<string> GetAllFilesInPath(string path)
        {
            try
            {
                List<string> allFilesAndDirectories = new List<string>();

                // Get TRUE root based on project location and not on run location
                string rootPath = "";

                // User gave path
                if (path != null)
                {

                }
                // User gave no path, return files in root
                else
                {

                }

                return allFilesAndDirectories;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
    }
}
