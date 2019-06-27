using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Services;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;
using hfupilot.Models;
using hfupilot.Models.api;
using System.Threading.Tasks;
using hfupilot.app.Helper;

namespace hfupilot.app.ViewModels
{

    public class AnmeldenViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;
        private string benutzer;
        private string passwort;
        private NavigationViewModel navigation;

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

        public NavigationViewModel Navigation
        {
            get => navigation;
            set
            {
                navigation = value;
                RaisePropertyChanged();
            }
        }

        public AnmeldenViewModel(INavigation navigation,
                                 IViewMapper viewMapper,
                                 UserContext userContext,
                                 HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;

            Benutzer = "";
            Passwort = "";

            Navigation = new NavigationViewModel(navigation, viewMapper, userContext, httpClient);
            LoginCommand = new RelayCommand(LoginHandler, CanExecute);
        }

        public void LoginHandler(object obj)
        {
            //Login Aufbereitet
            BenutzerLogin benutzerLogin = new BenutzerLogin() { Benutzer = benutzer, Passwort = passwort };

            var content = new StringContent(JsonConvert.SerializeObject(benutzerLogin), Encoding.UTF8, "application/json");

            //Request an Web-API senden.
            Task<HttpResponseMessage> Response = _httpClient.PostAsync("/api/Authentifizierung/anmelden", content);
            Response.Wait();

            //Message auslessen
            Task<string> anmeldung = Response.Result.Content.ReadAsStringAsync();
            anmeldung.Wait();

            Anmelden anmelden = JsonConvert.DeserializeObject<Anmelden>(anmeldung.Result);
            if (anmelden.Fehler == 0)
            {
                _userContext.SessionID = anmelden.Session;
                _userContext.Stufe = anmelden.Stufe;
                _navigation.PushAsync(_viewMapper.Map(new DashboardViewModel()));
            }
            else
            {
                ((Page)obj).DisplayAlert("Bestätigen", anmelden.FehlerMeldung, "OK");
            }

        }

        public bool CanExecute(object obj)
        {
            return Benutzer != "" && Passwort != "";
        }
    }
}
