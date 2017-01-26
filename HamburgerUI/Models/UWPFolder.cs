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
        List<File> fileList = new List<File>();
        
        public async Task<string> FolderPathGrabberAsync()
        {
            StorageFolder addFolder;
            var fP = new FolderPicker();
            fP.FileTypeFilter.Add("*");
            try
            {
                addFolder = await fP.PickSingleFolderAsync();
            }

            catch (Exception e)
            {
                addFolder=null;
            }

            if (addFolder != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", addFolder);
                Folder = addFolder;
                return addFolder.Path;
            }
            else return null;
        }

        public StorageFolder Folder { get; set; }

        public async Task<FileListReturnType> GetFileList()
        {
            FileListReturnType fileListReturn = new FileListReturnType();

            IReadOnlyList<StorageFile> filesInFolder;
            try
            {
                filesInFolder = await Folder.GetFilesAsync(CommonFileQuery.OrderByName);
            }
            
            catch (Exception e)
            {
                fileListReturn.Success = false;
                fileListReturn.ErrorMessage = e.Message;
                filesInFolder = null;
            }

            if (filesInFolder != null)
            {
                foreach (var currentFile in filesInFolder)
                {
                    DateTimeOffset dateCreated = currentFile.DateCreated;
                    string name = currentFile.Name;
                    string path = currentFile.Path;
                    var properties = await currentFile.GetBasicPropertiesAsync();
                    ulong size = properties.Size;
                    string extension = currentFile.FileType;
                    File newFile = new File(name, dateCreated, path, size, extension);
                    fileList.Add(newFile);
                }
            }
            if (filesInFolder != null && filesInFolder.Count>0)
            {
                fileListReturn.Success = true;
                fileListReturn.FileList = fileList;
                return fileListReturn;
            }
            else return null;            
            
        }
    }
}
