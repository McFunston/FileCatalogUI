using Windows.UI.Xaml;
using System.Threading.Tasks;
using HamburgerUI.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using HamburgerUI.Models;
using Microsoft.EntityFrameworkCore;
using HamburgerUI.Services.RepositoryServices;
using HamburgerUI.Services;

namespace HamburgerUI
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {

        public ServicesController ServCon { get; set; }

        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;
            AutoSuspendAllFrames = true;
            AutoRestoreAfterTerminated = true;
            AutoExtendExecutionSession = true;

            #endregion

            using (var EFR = new EFRepositoryContext())
            {
                EFR.Database.Migrate();
            }

            UWPFolder folder = new UWPFolder();
            EFRepository EFRepo = new EFRepository();

            ServCon = ServicesController.Instance;
            if (false == ServCon.IsConfigured) ServCon.Configure(EFRepo, folder);                    
                        
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }
    }
}

