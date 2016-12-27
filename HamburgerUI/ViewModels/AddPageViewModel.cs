using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Navigation;

namespace HamburgerUI.ViewModels
{
    public class AddPageViewModel : ViewModelBase
    {
        public AddPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                CatalogName = "Designtime value";
            }
            
        }

        private bool goEnabled = false;

        public bool GoEnabled
        {
            get { return goEnabled; }
            set { Set(ref goEnabled,  value); }
        }
        

        private string addFolderPath;
        public string AddFolderPath
        {
            get { return addFolderPath; }
            set { Set(ref addFolderPath, value); if (addFolderPath?.Length > 0 && CatalogName?.Length > 0) GoEnabled = true; else GoEnabled = false; }
        }

        private string catalogName = "Default";
        public string CatalogName
        {
            get { return catalogName; }
            set { Set(ref catalogName, value); if (AddFolderPath?.Length > 0 && catalogName?.Length > 0) GoEnabled = true; else GoEnabled = false; }
        }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            CatalogName = (suspensionState.ContainsKey(nameof(CatalogName))) ? suspensionState[nameof(CatalogName)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(CatalogName)] = CatalogName;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

    }
}
