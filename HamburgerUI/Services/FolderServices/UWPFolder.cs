﻿using HamburgerUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Search;

namespace HamburgerUI.Models
{
    public class UWPFolder : IFolder, INotifyPropertyChanged
    {

        public UWPFolder()
        {
            PercentDone = 0;
        }

        

        int numberOfFiles;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FolderName
        {            
            get {
                if ( this.Folder != null && this.Folder.Name != null)
                    return this.Folder.Name;
                else return null;
            }
        }
        
        private double percentDone;
                      
        public double PercentDone
        {
            get { if (this != null) return percentDone; else return 0; }
            private set { percentDone = value; NotifyPropertyChanged("PercentDone"); }
        }
              
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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
            List<File> fileList = new List<File>();
            PercentDone = 1;
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
                numberOfFiles = filesInFolder.Count;
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
                    Math.Max(PercentDone = ((double)fileList.Count / (double)numberOfFiles) * 100, 1);
                }
                PercentDone = 0;
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
