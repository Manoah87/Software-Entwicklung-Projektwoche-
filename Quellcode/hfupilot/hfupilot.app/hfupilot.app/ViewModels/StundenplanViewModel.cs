using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using hfupilot.Models.api;
using Newtonsoft.Json;
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

        public StundenplanViewModel(){}

        public StundenplanViewModel(INavigation navigation,
                                 IViewMapper viewMapper,
                                 UserContext userContext,
                                 HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;

            int filter = 1;

            //Task<HttpResponseMessage> Response = _httpClient.GetAsync($"/api/Stundenplan/{filter}/{_userContext.SessionID}");
            //Response.Wait();
        }


    }
}
