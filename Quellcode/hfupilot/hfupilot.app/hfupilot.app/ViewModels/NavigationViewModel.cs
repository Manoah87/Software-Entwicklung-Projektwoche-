using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using hfupilot.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace hfupilot.app.ViewModels
{
    public class NavigationViewModel : ObservableObject
    {

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly UserContext _userContext;
        private readonly HttpClient _httpClient;

        public ICommand StundenplanCommand { get; }
        public ICommand MeldungenAnzeigenCommand { get; }
        public ICommand MeldungenErfassenCommand { get; }

        public ICommand LogoutCommand { get; }

        public delegate void LoginMessage(string title, string message);
        public static LoginMessage LoginMessageHandler { get; set; }

        public NavigationViewModel(INavigation navigation,
                                   IViewMapper viewMapper,
                                   UserContext userContext,
                                   HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _userContext = userContext;
            _httpClient = httpClient;

            StundenplanCommand = new RelayCommand(StundenplanOpen);
            MeldungenAnzeigenCommand = new RelayCommand(MeldungenAnzeigenOpen);
            MeldungenErfassenCommand = new RelayCommand(MeldungenErfassenOpen);
            LogoutCommand = new RelayCommand(LogoutHandler);
        }

        private void StundenplanOpen (object obj)
        {
            _navigation.PushAsync(_viewMapper.Map(App.Services.GetInstance<StundenplanViewModel>()));
        }
        private void MeldungenAnzeigenOpen (object obj)
        {
            _navigation.PushAsync(_viewMapper.Map(App.Services.GetInstance <MeldungenAnzeigenViewModel>()));
        }
        private void MeldungenErfassenOpen (object obj)
        {
            _navigation.PushAsync(_viewMapper.Map(App.Services.GetInstance <MeldungViewModel>()));
        }

        public void LogoutHandler(object obj)
        {
           
            Task<HttpResponseMessage> Response = _httpClient.GetAsync($"/api/Authentifizierung/{_userContext.SessionID}");
            Response.Wait();

            //Message auslessen
            Task<string> abmelden = Response.Result.Content.ReadAsStringAsync();
            abmelden.Wait();

            BasisFehlerProperties objBasisFehler = JsonConvert.DeserializeObject<BasisFehlerProperties>(abmelden.Result);
            if (objBasisFehler != null)
            {
                if (objBasisFehler.Fehler == 0)
                {
                    LoginMessageHandler.Invoke("Tschaui", "Bis bald.");
                    _navigation.PushAsync(_viewMapper.Map(App.Services.GetInstance<AnmeldenViewModel>()));
                }
                else
                {
                    LoginMessageHandler.Invoke("Fehler", objBasisFehler.FehlerMeldung);
                }
            }

        }
    }
}
