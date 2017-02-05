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
        Task Remove(Archive archive);
        Task<List<Archive>> Load();
        Task<List<File>> Search(string searchString);
    }
}
