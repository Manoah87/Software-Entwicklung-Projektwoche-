using hfupilot.app.CustomFramework.mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using hfupilot.app.Services;
using hfupilot.app.Helper;
using System.Net.Http;
using hfupilot.Models.api;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using hfupilot.Models;

namespace hfupilot.app.ViewModels
{
    public class VerspaetungViewModel : ObservableObject
    {
        private List<string> _verspeatungsAuswahlsliste;
        private string _begruendung;
        private string _begruendungLaenge;
        private int _verspaetungAuswahl;

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public ICommand SpeichernCommand { get; }

        
        public VerspaetungViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;

            _begruendung = "";
            _userContext = userContext;

            _verspeatungsAuswahlsliste = new List<string>();
            _verspeatungsAuswahlsliste.Add("Verspätung Wählen");
            for (int i = 1; i < 5; i++)
            {
                _verspeatungsAuswahlsliste.Add($"{i*15} min");
            }

            SpeichernCommand = new RelayCommand(SpeichernHandler, CanExecuteSpeichern);
        }

        public void SpeichernHandler(object obj)
        {
            TermineViewModel selectedTermin = App.Services.GetInstance<DashboardTermineViewModel>().SelectedTermine;

            VerspaetungParameter verspaetungParameter = new VerspaetungParameter()
            {
                Anzal = AusgewaelteVerspaetung,
                Grund = Begruendung,
                ID = selectedTermin.Id,
                SessionID = _userContext.SessionID
            };

            var content = new StringContent(JsonConvert.SerializeObject(verspaetungParameter),
                             Encoding.UTF8, "application/json");

            //Request an Web-API senden.
            Task<HttpResponseMessage> Response = _httpClient.PostAsync("/api/Meldungen/verspaetung", content);
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
            return _verspaetungAuswahl != 0 && _begruendung != "";
        }

        public List<string> Verspeatungen
        {
            get => _verspeatungsAuswahlsliste;
            set
            {
                _verspeatungsAuswahlsliste = value;
                RaisePropertyChanged();
                ((RelayCommand)SpeichernCommand)?.RaiseCanExecuteChanged();
            }
        }

        public int AusgewaelteVerspaetung
        {
            get => _verspaetungAuswahl;
            set
            {
                _verspaetungAuswahl = value;
                RaisePropertyChanged();
                ((RelayCommand)SpeichernCommand)?.RaiseCanExecuteChanged();
            }
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
