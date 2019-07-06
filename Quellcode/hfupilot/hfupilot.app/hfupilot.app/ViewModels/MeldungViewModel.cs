using hfupilot.app.CustomFramework.mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using hfupilot.app.Services;
using hfupilot.app.Helper;
using System.Net.Http;
using System;
using hfupilot.Models.api;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using hfupilot.Models;

namespace hfupilot.app.ViewModels
{
    class MeldungViewModel : ObservableObject
    {
        private List<string> _artDerMeldungAuswahlsliste;
        private string _meldung;
        private string _meldungLaenge;
        private int _artDerMeldungAuswahl;

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public ICommand SpeichernCommand { get; }

        public MeldungViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;

            _meldung = "";

            _artDerMeldungAuswahlsliste = new List<string>();
            _artDerMeldungAuswahlsliste.Add("Art Wählen");
            _artDerMeldungAuswahlsliste.Add("Zimmerwechsel");
            _artDerMeldungAuswahlsliste.Add("Sonstige Meldung");

            SpeichernCommand = new RelayCommand(SpeichernHandler, CanExecuteSpeichern);
        }

        public void SpeichernHandler(object obj)
        {
            int iArt = 0;
            switch (_artDerMeldungAuswahl)
            {
                case 1:
                    iArt = 5;
                    break;
                case 2:
                    iArt = 9;
                    break;
                default:
                    iArt = 9;
                    break;
            }

            Page page = obj as Page;
            if (page != null)
            {
                MeldungParameter meldungParameter = new MeldungParameter()
                {
                    SessionID = _userContext.SessionID,
                    ID = 0,
                    Meldung = Meldung,
                    AktiveBis = DateTime.Now.AddDays(7),
                    Art = iArt
                };

                var content = new StringContent(JsonConvert.SerializeObject(meldungParameter), 
                              Encoding.UTF8, "application/json");

                //Request an Web-API senden.
                Task<HttpResponseMessage> Response = _httpClient.PostAsync("/api/Meldungen/meldung", content);
                Response.Wait();

                //Message auslessen
                Task<string> reponseContent = Response.Result.Content.ReadAsStringAsync();
                reponseContent.Wait();

                BasisFehlerProperties result = JsonConvert.DeserializeObject<BasisFehlerProperties>(reponseContent.Result);

                if (result.Fehler == 0)
                {
                    page.DisplayAlert("Meldung", "Meldung wurde gespeichert", "OK");
                    Meldung = "";
                }
                else
                {
                    page.DisplayAlert("Fehler", result.FehlerMeldung, "OK");
                }
            }
        }

        public bool CanExecuteSpeichern(object obj)
        {
            return _artDerMeldungAuswahl != 0 && _meldung != "";
        }

        public List<string> MeldungsAuswahl
        {
            get => _artDerMeldungAuswahlsliste;
            set
            {
                _artDerMeldungAuswahlsliste = value;
                RaisePropertyChanged();
                ((RelayCommand)SpeichernCommand)?.RaiseCanExecuteChanged();
            }
        }

        public int AusgewaelteArtDerMeldung
        {
            get => _artDerMeldungAuswahl;
            set
            {
                _artDerMeldungAuswahl = value;
                RaisePropertyChanged();
                ((RelayCommand)SpeichernCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Meldung
        {
            get => _meldung;
            set
            {
                if (value.Length <= 512)
                {
                    _meldung = value;
                    MeldungLaenge = (512 - value.Length).ToString();
                }
                RaisePropertyChanged();
                ((RelayCommand)SpeichernCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string MeldungLaenge
        {
            get => _meldungLaenge;
            set
            {
                _meldungLaenge = value;
                RaisePropertyChanged();
            }
        }
    }
}
