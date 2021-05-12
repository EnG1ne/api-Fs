using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Models
{
    public class VirtualFile : IFileSystemObject
    {
        public string Name { get; set; }

        private string Content { get; set; }

        public VirtualFile(string name)
        {
            Name = name;
        }

        public VirtualFile(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public string GetContent()
        {
            return Content;
        }
    }
}
