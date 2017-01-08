using HamburgerUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services.RepositoryServices
{
    class EFRepository : IRepository
    {
        EFRepositoryContext EFR = new EFRepositoryContext();

        public Catalog Load()
        {
            throw new NotImplementedException();
        }

        public void Save(Catalog catalog)
        {
            throw new NotImplementedException();
            //EFR.Catalogs.Add(catalog);
            //EFR.SaveChanges();
        }

        public void Search(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
