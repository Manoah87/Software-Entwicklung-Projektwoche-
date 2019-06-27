using hfupilot.app.CustomFramework.mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using hfupilot.app.Services;
using System.Net.Http;

namespace hfupilot.app.ViewModels
{
    class VerspaetungViewModel : ObservableObject
    {
        private List<int> _verspeatungsAuswahl;
        private string _begruendung;
        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        public ICommand SpeicherCommand { get; set; }

        public VerspaetungViewModel()
        {
            _verspeatungsAuswahl = new List<int> { 1, 2, 3, 4, 5 };
        }

        public List<int> Verspeatungen
        {
            get { return _verspeatungsAuswahl; }
            set
            {
                if (_verspeatungsAuswahl != value)
                {
                    _verspeatungsAuswahl = value;
                    //OnPropertyChanged();
                }
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
