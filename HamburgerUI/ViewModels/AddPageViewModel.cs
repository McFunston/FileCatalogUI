using HamburgerUI.Models;
using HamburgerUI.Services;
using HamburgerUI.Services.RepositoryServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace HamburgerUI.ViewModels
{
    public class AddPageViewModel : ViewModelBase
    {
        private string addFolderPathText;

        private ObservableCollection<Archive> archives;

        private string catalogName = "Default";

        private bool goEnabled = false;

        private IFolder newFolder;

        private double percentDone;

        private IRepository repo;

        public AddPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                CatalogName = "Designtime value";
            }

            Repo = ServicesController.Instance.Repo;

            newFolder = ServicesController.Instance.FServe;

            newFolder.PropertyChanged += percentDoneChanged;

            PercentDone = newFolder.PercentDone;

            this.PropertyChanged += OnInputValuesChanged;
        }

        public string AddFolderPathText
        {
            get { return addFolderPathText; }
            set { Set(ref addFolderPathText, value); }
        }

        public ObservableCollection<Archive> Archives
        {
            get { return archives; }
            set { Set(ref archives, value); }
        }

        public string CatalogName
        {
            get { return catalogName; }
            set { Set(ref catalogName, value); }
        }

        /// <summary>
        /// Public property to map visibility of GoButton to. Prevents the user from doing a bad Add.
        /// </summary>
        public bool GoEnabled
        {
            get { return goEnabled; }
            set { Set(ref goEnabled, value); }
        }

        /// <summary>
        /// Public property for AddProgress progress bar to bind to.
        /// </summary>
        public double PercentDone
        {
            get { return percentDone; }
            set { Set(ref percentDone, value); }
        }

        public IRepository Repo
        {
            get { return repo; }
            set { Set(ref repo, value); }
        }

        /// <summary>
        /// Indexes folder that user has picked.
        /// </summary>
        /// <returns></returns>
        public async Task AddArchiveAsync()
        {
            var fileListReturn = await newFolder.GetFileList();

            if (fileListReturn.Success)
            {
                Archive archiveToAdd = new Archive(CatalogName, fileListReturn.FileList);
                await Repo.Add(archiveToAdd);
            }

            Archives = new ObservableCollection<Archive>(await Repo.Load());
            CatalogName = null;
            AddFolderPathText = null;
        }

        /// <summary>
        /// Picks the folder that the user wants to index
        /// </summary>
        public async void GetFolder()
        {
            await newFolder.FolderPathGrabberAsync();
            if (newFolder.FolderName != null)
            {
                AddFolderPathText = newFolder.FolderName;
            }
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(CatalogName)] = CatalogName;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Archives = new ObservableCollection<Archive>(await Repo.Load());
            CatalogName = (suspensionState.ContainsKey(nameof(CatalogName))) ? suspensionState[nameof(CatalogName)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Checks whether any conditions exists that should prevent the user from being able to run AddArchiveAsync (no folder chosen, name has already been used etc)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInputValuesChanged(object sender, System.EventArgs e)
        {
            Archive archiveWithMatchingName = new Archive();

            if (CatalogName != null)
            {
                archiveWithMatchingName = Archives.FirstOrDefault(archiveToCheck => archiveToCheck.Name.Equals(CatalogName));
            }

            bool match = false;

            if (archiveWithMatchingName != null) match = true;

            if (addFolderPathText?.Length > 0 && CatalogName?.Length > 0 && !(match) && PercentDone == 0) GoEnabled = true; else GoEnabled = false;
        }

        /// <summary>
        /// Updates PercentDone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void percentDoneChanged(object sender, System.EventArgs e)
        {
            PercentDone = newFolder.PercentDone;
        }
    }
}