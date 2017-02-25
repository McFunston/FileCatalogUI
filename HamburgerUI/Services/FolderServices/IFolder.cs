using HamburgerUI.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HamburgerUI.Models
{
    /// <summary>
    /// Since UWP apps use a FolderPicker and WPF uses another class I thought it would be good to abstract that away.
    /// </summary>
    public interface IFolder
    {
        Task<FileListReturnType> GetFileList();
        Task<string> FolderPathGrabberAsync();
        double PercentDone { get;}
        event PropertyChangedEventHandler PropertyChanged;
        string FolderName { get; }
    }
}