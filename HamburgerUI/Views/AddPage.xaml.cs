using System;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HamburgerUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPage : Page
    {
        public AddPage()
        {
            this.InitializeComponent();
        }

        //private async void AddButton_Click(object sender, RoutedEventArgs e)
        //{
            
        //    var fP = new FolderPicker();
        //    fP.FileTypeFilter.Add("*");

        //    var addFolderPath = await fP.PickSingleFolderAsync();
        //    AddFolderPath.Text = addFolderPath.Path;
        //    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", addFolderPath);

        //}
    }
}
