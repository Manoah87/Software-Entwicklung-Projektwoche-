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
    class DashboardTermineViewModel : ObservableObject
    {

        #region Felder

        private NavigationViewModel navigation;

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public ObservableCollection<TermineViewModel> Termine { get; set; }

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

        #region Konstruktor

        public DashboardTermineViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;

            Termine = new ObservableCollection<TermineViewModel>();

            Navigation = new NavigationViewModel(navigation, viewMapper, userContext, httpClient);

            aktualisiereTermine();
        }

        #endregion

        private void aktualisiereTermine()
        {
            Termine.Clear();

            Termine.Add(new TermineViewModel(){
                Id = 5,
                Datum = "heute",
                Zeit = "20:00",
                Code = "XYZ",
                Titel = "SW-Entwickeln",
                Bezeichnung = "Einsteiger-Modul",
                Zimmer = "126a",
                Lehrperson = "Paul"
            });

            Termine.Add(new TermineViewModel()
            {
                Id = 5,
                Datum = "heute",
                Zeit = "20:00",
                Code = "XYZ",
                Titel = "SW-Entwickeln",
                Bezeichnung = "Einsteiger-Modul",
                Zimmer = "126a",
                Lehrperson = "Heinz"
            });
            Termine.Add(new TermineViewModel()
            {
                Id = 5,
                Datum = "heute",
                Zeit = "18:00",
                Code = "asdf",
                Titel = "Datenbank",
                Bezeichnung = "Einsteiger-Modul",
                Zimmer = "126a",
                Lehrperson = "Urs"
            });

        }
    }
}
