using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using hfupilot.Models;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace hfupilot.app.ViewModels
{
    class StundenplanViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;
        private ObservableCollection<TermineViewModel> stundenplan;

        public StundenplanViewModel() { }

        public ObservableCollection<TermineViewModel> StundenplanList
        {
            get => stundenplan;
            set
            {
                stundenplan = value;
                RaisePropertyChanged();
            }
        }

        public StundenplanViewModel(INavigation navigation,
                                 IViewMapper viewMapper,
                                 UserContext userContext,
                                 HttpClient httpClient)
        {
            _navigation = navigation; 
             _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;

            StundenplanList = new ObservableCollection<TermineViewModel>();

            int filter = 1;

            Task<HttpResponseMessage> Response = _httpClient.GetAsync($"/api/Stundenplan/{_userContext.SessionID}/{filter}");
            Response.Wait();

            //Message auslessen
            Task<string> stundenplan = Response.Result.Content.ReadAsStringAsync();
            stundenplan.Wait();

            Stundenplan objStundenplan = JsonConvert.DeserializeObject<Stundenplan>(stundenplan.Result);
            objStundenplan.StundenplanList.ToList().ForEach(t => StundenplanList.Add(new TermineViewModel(t)));
        }


    }
}
