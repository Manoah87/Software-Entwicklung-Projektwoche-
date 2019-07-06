using hfupilot.app.CustomFramework.mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using hfupilot.app.Services;
using hfupilot.app.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using hfupilot.Models.api;
using System.Threading.Tasks;
using hfupilot.Models;

namespace hfupilot.app.ViewModels
{
    class AbwesenheitViewModel : ObservableObject
    {
        private string _begruendung;
        private string _begruendungLaenge;

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public ICommand SpeichernCommand { get; }
        public ICommand BegruendungLaengeCommand { get; set; }

        public AbwesenheitViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;

            _begruendung = "";
            _begruendungLaenge = "2";

            SpeichernCommand = new RelayCommand(SpeichernHandler, CanExecuteSpeichern);
           // BegruendungLaengeCommand = new RelayCommand(SpeichernHandler);
        }

        public void SpeichernHandler(object obj)
        {
            TermineViewModel selectedTermin = App.Services.GetInstance<DashboardTermineViewModel>().SelectedTermine;

            VerspaetungParameter verspaetungParameter = new VerspaetungParameter()
            {
                Anzal = 0,
                Grund = Begruendung,
                ID = selectedTermin.Id,
                SessionID = _userContext.SessionID
            };

            var content = new StringContent(JsonConvert.SerializeObject(verspaetungParameter),
                             Encoding.UTF8, "application/json");

            //Request an Web-API senden.
            Task<HttpResponseMessage> Response = _httpClient.PostAsync("/api/Meldungen/abwesenheit", content);
            Response.Wait();

            //Message auslessen
            Task<string> reponseContent = Response.Result.Content.ReadAsStringAsync();
            reponseContent.Wait();

            BasisFehlerProperties result = JsonConvert.DeserializeObject<BasisFehlerProperties>(reponseContent.Result);

            if (result.Fehler == 0)
            {
                ((Page)obj).DisplayAlert("Meldung", "Meldung wurde gespeichert", "OK");
                Begruendung = "";
                _navigation.PopAsync();
            }
            else
            {
                ((Page)obj).DisplayAlert("Fehler", result.FehlerMeldung, "OK");
            }
        }

        public bool CanExecuteSpeichern(object obj)
        {
            return  _begruendung != "";
        }

        public string Begruendung
        {
            get => _begruendung;
            set
            {
                if (value.Length <= 512)
                {
                    _begruendung = value;
                    BegruendungLaenge = (512 - value.Length).ToString();
                }
                RaisePropertyChanged();
                ((RelayCommand)SpeichernCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string BegruendungLaenge
        {
            get => _begruendungLaenge;
            set
            {
                    _begruendungLaenge = value;
                    RaisePropertyChanged();
            }
        }
    }
}
