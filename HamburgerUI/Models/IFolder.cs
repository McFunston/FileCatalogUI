using HamburgerUI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HamburgerUI.Models
{
    public interface IFolder
    {
        Task<FileListReturnType> GetFileList();
    }
}