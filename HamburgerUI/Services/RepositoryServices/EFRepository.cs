using HamburgerUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services.RepositoryServices
{
    public class EFRepository : IRepository
    {
       
        public List<Archive> Load()
        {
            using (var EFR = new EFRepositoryContext())
            {
                var EFRList = EFR.Archives.ToList<Archive>();
                var EFRFiles = EFR.Files.ToList<File>();            
                return EFRList;
            }
                
        }

        public async Task Add(Archive archive)
        {
            using (var EFR = new EFRepositoryContext())
            {
                foreach (var fi in archive.Files)
                {
                    EFR.Files.Add(fi);
                }
                
                EFR.Archives.Add(archive);
                await EFR.SaveChangesAsync();
            }
        }

        public void Remove (Archive archive)
        {
            using (var EFR = new EFRepositoryContext())
            {
                EFR.Archives.Remove(archive);
                EFR.Files.RemoveRange(archive.Files);
                EFR.SaveChanges();
            }
        }

        public List<File> Search(string searchString)
        {
            using (var EFR = new EFRepositoryContext())
            {
                //var returnList = EFR.Files.Where (f => f.Name.ToLower().Contains(searchString.ToLower()));
                var returnList = from f in EFR.Files where f.Name.ToLower().Contains(searchString.ToLower()) select f;
                return returnList.ToList();
            }            
        }
    }
}
