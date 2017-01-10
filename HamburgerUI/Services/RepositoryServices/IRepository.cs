using HamburgerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services.RepositoryServices
{
    interface IRepository
    {
        Task Add(Archive archive);
        List<Archive> Load();
        List<File> Search(string searchString);
    }
}
