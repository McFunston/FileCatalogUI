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
       
        public async Task<List<Archive>> Load()
        {
            using (var EFR = new EFRepositoryContext())
            {                
                var EFRList = await EFR.Archives.ToListAsync<Archive>();
                //var EFRFiles = EFR.Files.ToList<File>();
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

        public async Task Remove (Archive archive)
        {
            using (var EFR = new EFRepositoryContext())
            {
                EFR.Archives.Remove(archive);
                var EFRFiles = EFR.Files.ToList<File>();
                if (archive.Files != null)
                {
                    EFR.Files.RemoveRange(archive.Files);
                }                
                await EFR.SaveChangesAsync();
            }
        }

        public async Task<List<File>> Search(string searchString)
        {
            using (var EFR = new EFRepositoryContext())
            {
                //var returnList = EFR.Files.Where(f => f.Name.ToLower().Contains(searchString.ToLower())).SelectMany();
                //var returnList = from f in EFR.Files.Include("Archive") where f.Name.ToLower().Contains(searchString.ToLower()) select f;
                var returnList = from f in EFR.Files where f.Name.ToLower().Contains(searchString.ToLower()) select f;
                //var returnList = EFR.Files.Where(f => f.Name == searchString).Select(x => x).Include("Archive") ;

                //List<File> returnList = EFR.Files.Include("Archive").Where(f => f.Name == searchString).ToList();
                return await returnList.Include("Archive").ToListAsync();
            }            
        }
    }
}
