using HamburgerUI.Models;
using System.Collections.Generic;

namespace HamburgerUI.Services
{
    public class FileListReturnType
    {
        public string ErrorMessage { get; set; }
        public List<File> FileList { get; set; }
        public bool Success { get; set; }
    }
}