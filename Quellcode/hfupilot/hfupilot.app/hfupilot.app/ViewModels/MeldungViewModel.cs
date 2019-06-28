using hfupilot.app.CustomFramework.mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using hfupilot.app.Services;
using hfupilot.app.Helper;
using System.Net.Http;

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

        public ICommand SpeichernCommand { get; set; }

        public MeldungViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;

            _meldung = "";

            _artDerMeldungAuswahlsliste = new List<string>();
            _artDerMeldungAuswahlsliste.Add("Art Wählen");
            _artDerMeldungAuswahlsliste.Add("Zimmerwechsel");
            _artDerMeldungAuswahlsliste.Add("Sonstige Meldung");

            SpeichernCommand = new RelayCommand(SpeichernHandler, CanExecuteSpeichern);
        }

        public void SpeichernHandler(object obj)
        {
            ((Page)obj).DisplayAlert("Auswahl", $"index: {_artDerMeldungAuswahl} \n ausrede: {_meldung}", "OK");

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
