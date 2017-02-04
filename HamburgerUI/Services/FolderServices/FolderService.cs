using HamburgerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services.FolderServices
{
    public class FolderService : IFolder
    {
        IFolder Folder;
        FolderService(IFolder FolderType)
        {
            Folder = FolderType;
        }

        public double PercentDone
        {
            get
            {
                return Folder.PercentDone;
            }

        }

        public async Task<string> FolderPathGrabberAsync()
        {
            string path = await Folder.FolderPathGrabberAsync();
            return path;
        }

        public async Task<FileListReturnType> GetFileList()
        {
            FileListReturnType listReturn = await Folder.GetFileList();
            return listReturn;
        }
    }
}
