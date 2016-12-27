using HamburgerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services
{
    public static class ArchiveMaker
    {
        public static async Task<Archive> MakeNewArchiveAsync(string name, string path)
        {
            Archive archive = new Archive(name);
            var fileList = await FileListFactory.GetFileListFromPathAsync(path);
            archive.Files = fileList.FileList;
            return archive;
        }
    }
}
