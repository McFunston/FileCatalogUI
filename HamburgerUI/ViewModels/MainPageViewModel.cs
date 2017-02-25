using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using HamburgerUI.Models;
using HamburgerUI.Services.RepositoryServices;
using HamburgerUI.Services;

namespace HamburgerUI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            Repo = ServicesController.Instance.Repo;
            this.PropertyChanged += OnSearchResults;
        }

        private IRepository repo;
        public IRepository Repo
        {
            get { return repo; }
            set { Set(ref repo, value); }
        }

        private List<File> searchResults;
        /// <summary>
        /// Public property for SearchResults ListView to bind to.
        /// </summary>
        public List<File> SearchResults
        {
            get { return searchResults; }
            set { Set(ref searchResults, value); }
        }

        private bool searchSuccess = false;
        public bool SearchSuccess
        {
            get { return searchSuccess; }
            set { Set(ref searchSuccess, value); }
        }

        private bool searching = false;
        /// <summary>
        /// Provides a public property for the ProgressRing to bind its visibility to.
        /// </summary>
        public bool Searching
        {
            get { return searching; }
            set { Set(ref searching, value); }
        }

        public EFRepository EFR { get; set; }

        private string searchString;
        public string SearchString
        {
            get { return searchString; }
            set { Set(ref searchString,  value); }
        }

        string _Value = "";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
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

        public void Clear()
        {
            if (SearchResults != null)
            {
                SearchResults = null;
                SearchString = "";
            }
        }

        private void OnSearchResults(object sender, System.EventArgs e)
        {
            if (searchResults != null)
            {
                SearchSuccess = true;
            }
            else SearchSuccess = false;
        }

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

