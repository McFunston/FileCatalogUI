using System;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;

namespace HamburgerUI.ViewModels
{
    public class AboutPartViewModel : ViewModelBase
    {
        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;
        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public Uri RateMe => new Uri("http://aka.ms/template10");

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }
    }

    public class SettingsPageViewModel : ViewModelBase
    {
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();
        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
    }

    public class SettingsPartViewModel : ViewModelBase
    {
        private string _BusyText = "Please wait...";
        private Services.SettingsServices.SettingsService _settings;

        private DelegateCommand _ShowBusyCommand;

        public SettingsPartViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                _settings = Services.SettingsServices.SettingsService.Instance;
            }
        }

        public string BusyText
        {
            get { return _BusyText; }
            set
            {
                Set(ref _BusyText, value);
                _ShowBusyCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsFullScreen
        {
            get { return _settings.IsFullScreen; }
            set
            {
                _settings.IsFullScreen = value;
                base.RaisePropertyChanged();
                if (value)
                {
                    ShowHamburgerButton = false;
                }
                else
                {
                    ShowHamburgerButton = true;
                }
            }
        }

        public DelegateCommand ShowBusyCommand
            => _ShowBusyCommand ?? (_ShowBusyCommand = new DelegateCommand(async () =>
            {
                Views.Busy.SetBusy(true, _BusyText);
                await Task.Delay(5000);
                Views.Busy.SetBusy(false);
            }, () => !string.IsNullOrEmpty(BusyText)));

        public bool ShowHamburgerButton
        {
            get { return _settings.ShowHamburgerButton; }
            set { _settings.ShowHamburgerButton = value; base.RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set { _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; base.RaisePropertyChanged(); }
        }

        public bool UseShellBackButton
        {
            get { return _settings.UseShellBackButton; }
            set { _settings.UseShellBackButton = value; base.RaisePropertyChanged(); }
        }
    }
}