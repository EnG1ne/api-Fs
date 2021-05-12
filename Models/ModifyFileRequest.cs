using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_test.Models
{
    public class ModifyFileRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
