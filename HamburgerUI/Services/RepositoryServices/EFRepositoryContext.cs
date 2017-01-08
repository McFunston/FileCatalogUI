using HamburgerUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services.RepositoryServices
{
    class EFRepositoryContext : DbContext
    {
        public EFRepositoryContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DisCat.db");
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Archive> Archives { get; set; }
    }
}
