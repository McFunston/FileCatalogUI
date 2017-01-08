using HamburgerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services
{
    public class FileListReturnType
    {
        public bool Success { get; set; }
        public List<File> FileList { get; set; }
        public string ErrorMessage { get; set; }
    }
}
