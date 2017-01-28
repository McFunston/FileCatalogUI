﻿using HamburgerUI.Models;
using HamburgerUI.Services;
using HamburgerUI.Services.RepositoryServices;
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
            Archives = new ObservableCollection<Archive>(Repo.Load());

            newUWPFolder = new UWPFolder();

            newUWPFolder.PropertyChanged += percentDoneChanged;

            this.PropertyChanged += OnInputValuesChanged;
        }

        private ObservableCollection<Archive> archives;

        public ObservableCollection<Archive> Archives
        {
            get { return archives; }
            set { Set(ref archives, value); }
        }     

        private EFRepository repo = new EFRepository();
        public EFRepository Repo
        {
            get { return repo; }
            set { Set(ref repo, value); }
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
            set { Set(ref addFolderPathText, value);}
        }

        private string catalogName = "Default";
        public string CatalogName
        {
            get { return catalogName; }
            set { Set(ref catalogName, value);}
        }

        private double percentDone;

        public double PercentDone
        {
            get { return percentDone; }
            set { Set(ref percentDone, value); }
        }

        UWPFolder newUWPFolder;

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

        private void OnInputValuesChanged(object sender, System.EventArgs e)
        {
            Archive archiveWithMatchingName = new Archive();

            if (CatalogName != null)
            {
                archiveWithMatchingName = Archives.FirstOrDefault(archiveToCheck => archiveToCheck.Name.Equals(CatalogName));
            }
            
            bool match = false;

            if (archiveWithMatchingName != null) match = true; 

            if (addFolderPathText?.Length > 0 && CatalogName?.Length > 0 && !(match)) GoEnabled = true; else GoEnabled = false;
        }
        
        private void percentDoneChanged(object sender, System.EventArgs e)
        {
            PercentDone = newUWPFolder.PercentDone;
        }
              
        public async void GetFolder()
        {
            await newUWPFolder.FolderPathGrabberAsync();
            if (newUWPFolder.Folder != null)
            {
                AddFolderPathText = newUWPFolder.Folder.Name;
            }            
        }

        public async void AddButton()
        {
            await newUWPFolder.FolderPathGrabberAsync();
            AddFolderPathText = newUWPFolder.Folder.Name;
        }

        public async Task AddArchiveAsync()
        {
            var fileListReturn = await newUWPFolder.GetFileList();

            if (fileListReturn.Success)
            {
                Archive archiveToAdd = new Archive(CatalogName, fileListReturn.FileList);
                await Repo.Add(archiveToAdd);                
            }
            Archives = new ObservableCollection<Archive>(Repo.Load());
        }

    }
}
