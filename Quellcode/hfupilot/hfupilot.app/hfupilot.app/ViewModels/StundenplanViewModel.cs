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
using System.Windows.Input;

namespace hfupilot.app.ViewModels
{
    class StundenplanViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;
        private ObservableCollection<TermineViewModel> stundenplan;
        private string filterName;

        public ICommand FilterChanedCommand { get; }
        public ICommand LogoutCommand { get; }

        public int FilterID { get; set; }
        public string FilterName
        {
            get => filterName;
            set
            {
                filterName = value;
                RaisePropertyChanged();
                FilterChanedCommand.Execute(null);
                Update();
            }
        }

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

            FilterID = 1;
            FilterChanedCommand = new RelayCommand(FilterChanedHandler);
            LogoutCommand = new RelayCommand(LogoutHandler);
            Update();

        }

        public void Update()
        {
            StundenplanList.Clear();
            Task<HttpResponseMessage> Response = _httpClient.GetAsync($"/api/Stundenplan/{_userContext.SessionID}/{FilterID}");
            Response.Wait();

            //Message auslessen
            Task<string> stundenplan = Response.Result.Content.ReadAsStringAsync();
            stundenplan.Wait();

            Stundenplan objStundenplan = JsonConvert.DeserializeObject<Stundenplan>(stundenplan.Result);
            objStundenplan.StundenplanList.ToList().ForEach(t => StundenplanList.Add(new TermineViewModel(t)));

        }

        public void FilterChanedHandler(object obj)
        {
            switch (FilterName)
            {
                case "heute":
                    FilterID = 0;
                    break;
                case "diese Woche":
                    FilterID = 1;
                    break;
                case "nächste Woche":
                    FilterID = 2;
                    break;
                default:
                    FilterID = 1;
                    break;
            }

            Update();
        }

        public void LogoutHandler(object obj)
        {
            Page page = obj as Page;
            Task<HttpResponseMessage> Response = _httpClient.GetAsync($"/api/Authentifizierung/abmeldung/{_userContext.SessionID}");
            Response.Wait();

            //Message auslessen
            Task<string> abmelden = Response.Result.Content.ReadAsStringAsync();
            abmelden.Wait();

            BasisFehlerProperties objBasisFehler = JsonConvert.DeserializeObject<BasisFehlerProperties>(abmelden.Result);
            if (objBasisFehler.Fehler != 0 && page != null)
            {
                if(objBasisFehler.Fehler == 0)
                {
                    page.DisplayAlert("Tschaui", "Bis bald.", "OK");
                    _navigation.PushAsync(_viewMapper.Map(App.Services.GetInstance<AnmeldenViewModel>()));
                }
                else
                {
                    page.DisplayAlert("Fehler", objBasisFehler.FehlerMeldung,"OK");
                }
            }

        }

    }
}
