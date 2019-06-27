using hfupilot.app.CustomFramework.mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using hfupilot.app.Services;
using System.Net.Http;

namespace hfupilot.app.ViewModels
{
    public class VerspaetungViewModel : ObservableObject
    {
        private List<string> _verspeatungsAuswahlsliste;
        private string _begruendung;
        private int _verspaetungAuswahl;

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;

        public ICommand SpeicherCommand { get; set; }

        public VerspaetungViewModel(INavigation navigation, IViewMapper viewMapper, HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;

            _verspeatungsAuswahlsliste = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                _verspeatungsAuswahlsliste.Add($"{i*15} min");
            }

            SpeicherCommand = new RelayCommand(SpeichernHandler, CanExecuteSpeichern);
        }

        public void SpeichernHandler(object obj)
        {
            ((Page)obj).DisplayAlert("Auswahl", $"index: {_verspaetungAuswahl} \n ausrede: {_begruendung}", "OK");

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
                ((RelayCommand)SpeicherCommand)?.RaiseCanExecuteChanged();
            }
        }

        public int AusgewaelteVerspaetung
        {
            get => _verspaetungAuswahl;
            set
            {
                _verspaetungAuswahl = value;
                RaisePropertyChanged();
                ((RelayCommand)SpeicherCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Begruendung
        {
            get => _begruendung;
            set
            {
                _begruendung = value;
                RaisePropertyChanged();
            }
        }
    }
}
