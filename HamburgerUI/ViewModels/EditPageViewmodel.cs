﻿using HamburgerUI.Models;
using HamburgerUI.Services.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace HamburgerUI.ViewModels
{
    public class EditPageViewmodel : ViewModelBase
    {
        public EditPageViewmodel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            CatRef = Catalog.Cat;
            selectedArchive = new Archive();
        }

        private Archive selectedArchive;
        public Archive SelectedArchive
        {
            get { return selectedArchive; }
            set { Set(ref selectedArchive, value); }
        }

        private Catalog catRef;
        public Catalog CatRef
        {
            get { return catRef; }
            set { Set(ref catRef, value); }
        }

        private string _Value = "Default";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private EFRepository repo;
        public EFRepository Repo
        {
            get { return repo; }
            set { Set(ref repo, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
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

        public void Remove()
        {
            catRef.Remove(SelectedArchive);
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
    }
}
