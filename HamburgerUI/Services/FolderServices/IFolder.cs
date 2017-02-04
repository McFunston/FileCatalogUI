using HamburgerUI.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HamburgerUI.Models
{
    public interface IFolder
    {
        Task<FileListReturnType> GetFileList();
        Task<string> FolderPathGrabberAsync();
        double PercentDone { get;}
        event PropertyChangedEventHandler PropertyChanged;
        string FolderName { get; }
    }
}