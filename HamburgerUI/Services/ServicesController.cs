using HamburgerUI.Models;
using HamburgerUI.Services.FolderServices;
using HamburgerUI.Services.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services
{
    public sealed class ServicesController : IFolder, IRepository
    {
        private static ServicesController instance = null;
        private static readonly object padlock = new object();
        public bool IsConfigured { get; private set; }
        IRepository Repo;
        IFolder FServe;
        
        ServicesController()
        {
            IsConfigured = false;
        }

        public static ServicesController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServicesController();
                    }
                    return instance;
                }
            }
        }

        public double PercentDone
        {
            get
            {
                return FServe.PercentDone;
            }
        }

        void Configure(IRepository repo, IFolder fServe)
        {
            Repo = repo;
            FServe = fServe;
            if (FServe != null && Repo != null)
            {
                IsConfigured = true;
            }
        }

        public async Task Add(Archive archive)
        {
            await Repo.Add(archive);
        }

        public void Remove(Archive archive)
        {
            Repo.Remove(archive); 
        }

        public List<Archive> Load()
        {
            var archives = Repo.Load();
            return archives;
        }

        public List<File> Search(string searchString)
        {
            var results = Repo.Search(searchString);
            return results;
        }

        public Task<FileListReturnType> GetFileList()
        {
            throw new NotImplementedException();
        }

        public async Task<string> FolderPathGrabberAsync()
        {
            string path = await FServe.FolderPathGrabberAsync();
            return path;
        }
    }
}
