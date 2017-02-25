using HamburgerUI.Models;
using HamburgerUI.Services;
using HamburgerUI.Services.RepositoryServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace HamburgerUI.ViewModels
{
    public class EditPageViewmodel : ViewModelBase
    {
        private string _Value = "Default";

        private ObservableCollection<Archive> archives;

        private IRepository repo;

        private Archive selectedArchive;

        public EditPageViewmodel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            Repo = ServicesController.Instance.Repo;

            selectedArchive = new Archive();
        }

        public ObservableCollection<Archive> Archives
        {
            get { return archives; }
            set { Set(ref archives, value); }
        }

        public IRepository Repo
        {
            get { return repo; }
            set { Set(ref repo, value); }
        }

        public Archive SelectedArchive
        {
            get { return selectedArchive; }
            set { Set(ref selectedArchive, value); }
        }

        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

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
            Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
            Archives = new ObservableCollection<Archive>(await Repo.Load());
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Removes the selected Archive.
        /// </summary>
        /// <returns></returns>
        public async Task Remove()
        {
            await Repo.Remove(selectedArchive);
            Archives = new ObservableCollection<Archive>(await Repo.Load());
        }
    }
}