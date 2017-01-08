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
        void Save(Catalog catalog);
        Catalog Load();
        void Search(string searchString);
    }
}
