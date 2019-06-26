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
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //_httpClient.DefaultRequestHeaders
            //.Accept
            //.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // error _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("hfupilot"));


            //var httpResponseMessage =  _httpClient.PostAsync("/api/Authentifizierung/anmelden", content);
            //httpResponseMessage.Wait();

            Task<HttpResponseMessage> task = _httpClient.PostAsync("/api/Authentifizierung/anmelden", content);
            task.Wait();

            Task<string> res = task.Result.Content.ReadAsStringAsync();
            res.Wait();

            var result = _httpClient.GetAsync("/api/Authentifizierung");
            result.Wait();

            Debug.WriteLine("*==============>>>>>> Test <<<<<<<=============*");
            Debug.WriteLine("Content: " + task.Result.Content);
            Debug.WriteLine("StatusCode: " + task.Result.StatusCode);

        }

        public bool CanExecute(object obj)
        {
            return Benutzer != "" && Passwort != "";
        }
    }
}
