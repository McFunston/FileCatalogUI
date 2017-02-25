using HamburgerUI.Models;
using HamburgerUI.Services.RepositoryServices;

namespace HamburgerUI.Services
{
    /// <summary>
    /// Singleton for maintaining one IRepository and IFolder throughout all viewmodels.
    /// </summary>
    public sealed class ServicesController
    {
        private static readonly ServicesController instance = new ServicesController();

        private ServicesController()
        {
            IsConfigured = false;
        }

        public static ServicesController Instance
        {
            get
            {
                return instance;
            }
        }

        public IFolder FServe { get; set; }
        public bool IsConfigured { get; set; }

        public IRepository Repo { get; set; }

        /// <summary>
        /// Since singleton constructor's are private this is my workaround for injecting the Repository and Folder Picker
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="fServe"></param>
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