using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using Xamarin.Forms;

namespace hfupilot.app.ViewModels
{
    class DashboardMeldungenViewModel : ObservableObject
    {
        #region Felder
        private NavigationViewModel navigation;

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public ObservableCollection<MeldungenViewModel> Meldungen { get; set; }

        public NavigationViewModel Navigation
        {
            get => navigation;
            set
            {
                navigation = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        public DashboardMeldungenViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {

            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;



            Meldungen = new ObservableCollection<MeldungenViewModel>();

            Navigation = new NavigationViewModel(navigation, viewMapper, userContext, httpClient);



            aktualisiereMeldungen();
        }

        private void aktualisiereMeldungen()
        {
         Meldungen.Add(new MeldungenViewModel() {Art = 1,Datum = "26.07.17",Id=5,MeldungNachricht = "Hello World",Zeit = "20:00"});
         Meldungen.Add(new MeldungenViewModel() {Art = 2,Datum = "29.07.17",Id=1,MeldungNachricht = "Tschau World",Zeit = "22:00"});
         Meldungen.Add(new MeldungenViewModel() {Art = 3,Datum = "30.07.17",Id=6,MeldungNachricht = "Verschiebung",Zeit = "05:00"});

        }


    }
}
