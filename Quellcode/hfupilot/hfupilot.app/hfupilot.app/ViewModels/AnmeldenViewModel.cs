using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Services;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;

namespace hfupilot.app.ViewModels
{

    public class AnmeldenViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private string benutzer;
        private string passwort;

        public ICommand LoginCommand { get; set; }
        public string Benutzer
        {
            get => benutzer;
            set
            {
                benutzer = value;
                RaisePropertyChanged();
               ((RelayCommand)LoginCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Passwort
        {
            get => passwort;
            set
            {
                passwort = value;
                RaisePropertyChanged();
                ((RelayCommand)LoginCommand)?.RaiseCanExecuteChanged();
            }
        }

        public AnmeldenViewModel(INavigation navigation, IViewMapper viewMapper, HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;

            Benutzer = "";
            Passwort = "";
            LoginCommand = new RelayCommand(LoginHandler, CanExecute);
        }

        public async void LoginHandler(object obj)
        {
            var login = new { benutzer = this.benutzer, passwort = this.passwort };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("http://localhost:55939/api/Authentifizierung/anmelden", content);
        }

        public bool CanExecute(object obj)
        {
            return Benutzer != "" && Passwort != "";
        }
    }
}
