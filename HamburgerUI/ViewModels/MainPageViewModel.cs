using HamburgerUI.Models;
using HamburgerUI.Services;
using HamburgerUI.Services.RepositoryServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace HamburgerUI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _Value = "";

        private IRepository repo;

        private bool searching = false;

        private List<File> searchResults;

        private string searchString;

        private bool searchSuccess = false;

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            Repo = ServicesController.Instance.Repo;
            this.PropertyChanged += OnSearchResults;
        }

        public EFRepository EFR { get; set; }

        public IRepository Repo
        {
            get { return repo; }
            set { Set(ref repo, value); }
        }

        /// <summary>
        /// Provides a public property for the ProgressRing to bind its visibility to.
        /// </summary>
        public bool Searching
        {
            get { return searching; }
            set { Set(ref searching, value); }
        }

        /// <summary>
        /// Public property for SearchResults ListView to bind to.
        /// </summary>
        public List<File> SearchResults
        {
            get { return searchResults; }
            set { Set(ref searchResults, value); }
        }

        public string SearchString
        {
            get { return searchString; }
            set { Set(ref searchString, value); }
        }

        public bool SearchSuccess
        {
            get { return searchSuccess; }
            set { Set(ref searchSuccess, value); }
        }

        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public void Clear()
        {
            if (SearchResults != null)
            {
                SearchResults = null;
                SearchString = "";
            }
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

        //public void GotoDetailsPage() =>
        //    NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Runs a search for SearchString in all cataloged file names.
        /// </summary>
        /// <returns>List of files whos name contains SearchString</returns>
        public async Task SearchCatalog()
        {
            Searching = true;
            SearchResults = new List<File>();
            SearchResults = await Repo.Search(SearchString);
            Searching = false;
        }

        private void OnSearchResults(object sender, System.EventArgs e)
        {
            if (searchResults != null && searchResults.Count > 0)
            {
                SearchSuccess = true;
            }
            else SearchSuccess = false;
        }
    }
}