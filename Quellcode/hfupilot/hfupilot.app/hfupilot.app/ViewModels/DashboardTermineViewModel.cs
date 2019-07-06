using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using hfupilot.Models;

namespace hfupilot.app.ViewModels
{
    class DashboardTermineViewModel : ObservableObject
    {

        #region Felder

        private NavigationViewModel navigation;
        private ObservableCollection<TermineViewModel> termine;
        private TermineViewModel selectedTermine;
        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public ObservableCollection<TermineViewModel> TerminListe
        {
            get => termine;
            set
            {
                termine = value;
                RaisePropertyChanged();
            }
        }

        public NavigationViewModel Navigation
        {
            get => navigation;
            set
            {
                navigation = value;
                RaisePropertyChanged();
            }
        }

        public TermineViewModel SelectedTermine
        {
            get => selectedTermine;
            set
            {
                selectedTermine = value;
                RaisePropertyChanged();
                if(DateTime.Parse(selectedTermine.Datum).Day == DateTime.Now.Day &&
                   DateTime.Parse(selectedTermine.Datum).Month == DateTime.Now.Month || 1 == 1)
                {
                    // Wenn Termin Heute, dann kann nur eine Verspätung gemeldet werden
                    _navigation.PushAsync(_viewMapper.Map(App.Services.GetInstance<VerspaetungViewModel>()));
                }
                else
                {
                    _navigation.PushAsync(_viewMapper.Map(App.Services.GetInstance<AbwesenheitViewModel>()));
                    // Ansonsten eine Abweseneheit
                }

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



            Navigation = new NavigationViewModel(navigation, viewMapper, userContext, httpClient);

        }

        #endregion

        /// <summary>
        /// Liste der Termine wird nachgeladen
        /// </summary>
        public void Update()
        {
            TerminListe = new ObservableCollection<TermineViewModel>();
            TerminListe.Clear();

            //Termine Laden
            Task<HttpResponseMessage> Response = _httpClient.GetAsync($"/api/Stundenplan/{_userContext.SessionID}/{(int)ZeitFiler.diese_Woche}");
            Response.Wait();

            //Message auslessen
            Task<string> stundenplan = Response.Result.Content.ReadAsStringAsync();
            stundenplan.Wait();

            Stundenplan objStundenplan = JsonConvert.DeserializeObject<Stundenplan>(stundenplan.Result);
            if (objStundenplan != null)
            {
                objStundenplan.StundenplanList.ToList().ForEach(t => TerminListe.Add(new TermineViewModel(t)));
            }
        }
    }
}
