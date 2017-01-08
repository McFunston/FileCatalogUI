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
       
        public List<Archive> Load()
        {
            using (var EFR = new EFRepositoryContext())
            {
                var EFRList = EFR.Archives.ToList<Archive>();
                return EFRList;
            }
                
        }

        public async Task Add(Archive archive)
        {
            using (var EFR = new EFRepositoryContext())
            {
                EFR.Archives.Add(archive);
                await EFR.SaveChangesAsync();
            }
        }

        public void Remove (Archive archive)
        {
            using (var EFR = new EFRepositoryContext())
            {
                EFR.Archives.Remove(archive);
                EFR.SaveChanges();
            }
        }

        public void Search(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
