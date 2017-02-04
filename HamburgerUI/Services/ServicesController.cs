using HamburgerUI.Models;
using HamburgerUI.Services.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerUI.Services
{
    public sealed class ServicesController
    {
        public bool IsConfigured { get; set; }

        public IRepository Repo { get; set; }
        public IFolder FServe { get; set; }

        private static readonly ServicesController instance = new ServicesController();
                     
        public static ServicesController Instance
        {
            get
            {
                return instance;
            }
        }

        private ServicesController()
        {
            IsConfigured = false;
        }

        public void Configure(IRepository repo, IFolder fServe)
        {
            Repo = repo;
            FServe = fServe;
            if (FServe != null && Repo != null)
            {
                IsConfigured = true;
            }
            else IsConfigured = false;
        }
                
    }
}
