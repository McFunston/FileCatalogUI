using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;

namespace HamburgerUI.Models
{
    public sealed class Catalog
    {
        private static Catalog cat = new Catalog();

        private string pathToAdd;
        public string PathToAdd
        {
            get { return pathToAdd; }
            set { pathToAdd = value; }
        }

        private Catalog()
        {
            Archives = new ObservableCollection<Archive>();
        }

        public void Add(Archive archive)
        {
            this.Archives.Add(archive);
        }

        public async void GetPathAsync(IFolder iFolder)
        {
            var pathToAdd = await iFolder.FolderPathGrabberAsync();
        }

        public async void AddFolderAsync(string archiveName, IFolder iFolder)
        {
            var fileListReturn = await iFolder.GetFileList();
            if (fileListReturn.Success)
            {
                Archive archiveToAdd = new Archive(archiveName, fileListReturn.FileList);
                this.Add(archiveToAdd);
            }
            pathToAdd = null;
        }

        public void Remove(Archive archiveToRemove)
        {
            this.Archives.Remove(archiveToRemove);
        }

        public static Catalog Cat
        {
            get
            {
                return cat;
            }
        }

        public ObservableCollection<Archive> Archives { get; set; }
                     
        //public Catalog(Archive archive, ICollection<IFile> files)
        //{
        //    FileSet.Add(archive, files);
        //}
        
    }
}
