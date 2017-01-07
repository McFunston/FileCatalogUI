using HamburgerUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Search;

namespace HamburgerUI.Models
{
    public class UWPFolder : IFolder
    {
        List<IFile> fileList = new List<IFile>();

        //public UWPFolder(StorageFolder folder)
        //{
        //    Folder = folder;
        //}
        
        public async Task<string> FolderPathGrabberAsync()
        {
            var fP = new FolderPicker();
            fP.FileTypeFilter.Add("*");
            var addFolder = await fP.PickSingleFolderAsync();
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", addFolder);
            Folder = addFolder;
            return addFolder.Path;
        }

        public StorageFolder Folder { get; set; }

        public async Task<FileListReturnType> GetFileList()
        {
            FileListReturnType fileListReturn = new FileListReturnType();

            var filesInFolder = await Folder.GetFilesAsync(CommonFileQuery.OrderByName);
            foreach (var currentFile in filesInFolder)
            {                
                DateTimeOffset dateCreated = currentFile.DateCreated;
                string name = currentFile.DisplayName;
                string path = currentFile.Path;
                var properties = await currentFile.GetBasicPropertiesAsync();
                ulong size = properties.Size;
                string extension = currentFile.FileType;
                File newFile = new File(name, dateCreated, path, size, extension);
                fileList.Add(newFile);
            }

            fileListReturn.Success = true;
            fileListReturn.FileList = fileList;

            return fileListReturn;
        }

        public void PickAFolder()
        {

        }

    }
}
