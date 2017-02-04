using HamburgerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services.RepositoryServices
{
    public interface IRepository
    {
        Task Add(Archive archive);
        void Remove(Archive archive);
        List<Archive> Load();
        List<File> Search(string searchString);
    }
}
