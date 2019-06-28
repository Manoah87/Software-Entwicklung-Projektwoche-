using hfupilot.app.CustomFramework.mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using hfupilot.app.Services;
using hfupilot.app.Helper;
using System.Net.Http;

namespace hfupilot.app.ViewModels
{
    class AbwesenheitViewModel : ObservableObject
    {
        private string _begruendung;
        private string _begruendungLaenge;

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;

        public ICommand SpeichernCommand { get; set; }
        public ICommand BegruendungLaengeCommand { get; set; }

        public AbwesenheitViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;

            _begruendung = "";
            _begruendungLaenge = "2";

            SpeichernCommand = new RelayCommand(SpeichernHandler, CanExecuteSpeichern);
            BegruendungLaengeCommand = new RelayCommand(SpeichernHandler);
        }

        public void SpeichernHandler(object obj)
        {
            ((Page)obj).DisplayAlert("Auswahl", $" {_begruendungLaenge} \n ausrede: {_begruendung}", "OK");

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
