using HamburgerUI.Services.RepositoryServices;
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

        private EFRepository repo = new EFRepository();

        private Catalog()
        {
            var repoArchives = repo.Load();
            if (repoArchives != null)
            {
                Archives = new ObservableCollection<Archive>(repoArchives);
            }
            else Archives = new ObservableCollection<Archive>();            
        }

        public async Task Add(Archive archive)
        {
            this.Archives.Add(archive);
            await repo.Add(archive);
        }

        public async Task GetPathAsync(IFolder iFolder)
        {
            pathToAdd = await iFolder.FolderPathGrabberAsync();
        }

        public async Task AddFolderAsync(string archiveName, IFolder iFolder)
        {
            var fileListReturn = await iFolder.GetFileList();
            if (fileListReturn.Success)
            {
                Archive archiveToAdd = new Archive(archiveName, fileListReturn.FileList);
                await this.Add(archiveToAdd);

            }
            pathToAdd = null;
        }

        public void Remove(Archive archiveToRemove)
        {
            this.Archives.Remove(archiveToRemove);
            repo.Remove(archiveToRemove);
        }

        public static Catalog Cat
        {
            get
            {
                return cat;
            }
        }

        public ObservableCollection<Archive> Archives { get; set; }

    }
}
