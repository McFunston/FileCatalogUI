using HamburgerUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services
{
    public static class FileListFactory
    {
        /// <summary>
        /// Returns a FileListReturnType which contains a bool for success, a List of IFile, and a string for error message.
        /// </summary>
        /// <param name="path">The path that we want to get a file list from</param>
        /// <returns></returns>
        public async static Task<FileListReturnType> GetFileListFromPathAsync(string path)
        {
            var fileListReturn = new FileListReturnType();
            fileListReturn.Success = true;

            var GetFileListTask = Task.Run(() =>
            {
                var folder = new DirectoryInfo(@path);
                var fileList = new List<IFile>();

                try
                {
                    foreach (var fi in folder.EnumerateFiles())
                    {
                        var tempFile = new Models.File(fi.Name, fi.CreationTime, fi.FullName.Substring(2), fi.Length, fi.Extension);
                        fileList.Add(tempFile);
                    }
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    fileListReturn.Success = false;
                    fileListReturn.ErrorMessage = "The path that you selected was not found. Did you remove the media?";
                }

                catch (SecurityException)
                {
                    fileListReturn.Success = false;
                    fileListReturn.ErrorMessage = "You need to have read permision on every folder.";
                }

                return fileList;
            });

            fileListReturn.FileList = await GetFileListTask;

            return fileListReturn;


        }

    }
}
