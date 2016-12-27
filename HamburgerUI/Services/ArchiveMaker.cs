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
        public static async Task<ArchiveCreatorReturnType> MakeNewArchiveAsync(string name, string path)
        {
            ArchiveCreatorReturnType archive = new ArchiveCreatorReturnType();
            archive.archive.Name = name;
            var fileList = await FileListFactory.GetFileListFromPathAsync(path);
            archive.archive.Files = fileList.FileList;
            archive.ErrorMessage = fileList.ErrorMessage;
            return archive;
        }
    }
}
