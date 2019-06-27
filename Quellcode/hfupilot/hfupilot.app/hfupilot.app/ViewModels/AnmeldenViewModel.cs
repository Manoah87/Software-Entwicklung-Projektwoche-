using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Services;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using hfupilot.app.models.api;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using hfupilot.app.Models;

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

        public void LoginHandler(object obj)
        {
            var login = new { benutzer = this.benutzer, passwort = this.passwort };
            BenutzerLogin benutzerLogin = new BenutzerLogin() { Benutzer = benutzer, Passwort = passwort };

            var content = new StringContent(JsonConvert.SerializeObject(benutzerLogin), Encoding.UTF8, "application/json");
          
            Task<HttpResponseMessage> Response = _httpClient.PostAsync("/api/Authentifizierung/anmelden", content);
            Response.Wait();

            Task<string> anmeldung = Response.Result.Content.ReadAsStringAsync();
            anmeldung.Wait();

            Anmelden anmelden = (Anmelden)JsonConvert.DeserializeObject(anmeldung.Result);
            if (anmelden.Fehler == 0)
            {
                _navigation.PushAsync();
            }
            else
            {
                //show Message
            }

        }

        public bool CanExecute(object obj)
        {
            return Benutzer != "" && Passwort != "";
        }
    }
}
