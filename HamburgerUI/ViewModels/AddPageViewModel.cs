using HamburgerUI.Models;
using HamburgerUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CatRef = Catalog.Cat;
        }

        private Catalog catRef;

        public Catalog CatRef
        {
            get { return catRef; }
            set { Set(ref catRef,  value); }
        }
        
        private bool goEnabled = false;
        
        public bool GoEnabled
        {
            get { return goEnabled; }
            set { Set(ref goEnabled,  value); }
        }
        
        private string addFolderPathText;
        public string AddFolderPathText
        {
            get { return addFolderPathText; }
            set { Set(ref addFolderPathText, value); if (addFolderPathText?.Length > 0 && CatalogName?.Length > 0) GoEnabled = true; else GoEnabled = false; }
        }

        private string catalogName = "Default";
        public string CatalogName
        {
            get { return catalogName; }
            set { Set(ref catalogName, value); if (AddFolderPathText?.Length > 0 && catalogName?.Length > 0) GoEnabled = true; else GoEnabled = false; }
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
                
        public UWPFolder NewUWPFolder { get; set; }
        
        public async void AddButton()
        {
            var fP = new FolderPicker();
            fP.FileTypeFilter.Add("*");
            var addFolder = await fP.PickSingleFolderAsync();
            AddFolderPathText = addFolder.Path;
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", addFolder);
            NewUWPFolder = new UWPFolder(addFolder);
        }

        public async void AddArchiveAsync()
        {
            var addArchiveTry = await NewUWPFolder.GetFileList();
            
            if (addArchiveTry.Success)
            {
                Archive newArchive = new Archive(catalogName, addArchiveTry.FileList);          
                CatRef.Add(newArchive);                                               
            }
        }

    }
}
