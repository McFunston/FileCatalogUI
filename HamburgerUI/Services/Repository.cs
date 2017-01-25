using HamburgerUI.Models;
using HamburgerUI.Services.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services
{
    class Repository
    {
        IRepository Repo;
        public Repository(IRepository repo)
        {
            Repo = repo;
        }

        public async Task Add(Archive archive)
        {
            await Repo.Add(archive);
        }

        public void Remove(Archive archive)
        {
            Repo.Remove(archive);
        }

        public List<File> Search(string searchString)
        {
            var results = Repo.Search(searchString);
            return results;
        }

        public List<Archive> Load()
        {
            var archives = Repo.Load();
            return archives;
        }

    }
}
