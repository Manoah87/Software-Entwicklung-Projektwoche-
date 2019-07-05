using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using hfupilot.app.ViewModels;

namespace hfupilot.app.Views.partialview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : Grid
    {
        public Navigation()
        {
            InitializeComponent();
            NavigationViewModel.LoginMessageHandler += LoginMessageHandl;

        }

        private void LoginMessageHandl(string titel, string message)
        {
            App.Current.MainPage.DisplayAlert(titel, message, "OK");
        }
    }
}