using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using hfupilot.Models;
using Newtonsoft.Json;
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

        }

        public void Update()
        {
            //Meldungen Laden
            Task<HttpResponseMessage> Response = _httpClient.GetAsync($"/api/Meldungen/{_userContext.SessionID}/0");
            Response.Wait();

            //Message auslessen
            Task<string> stundenplan = Response.Result.Content.ReadAsStringAsync();
            stundenplan.Wait();

            Meldungen objMeldungen = JsonConvert.DeserializeObject<Meldungen>(stundenplan.Result);
            if (objMeldungen != null)
            {
                objMeldungen.MeldungList.ToList().ForEach(m => Meldungen.Add(new MeldungenViewModel(m)));
            }

        }


    }
}
