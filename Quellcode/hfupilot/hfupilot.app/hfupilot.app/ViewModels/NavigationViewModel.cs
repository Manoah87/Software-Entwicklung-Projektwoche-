using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace hfupilot.app.ViewModels
{
    public class NavigationViewModel : ObservableObject
    {

        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly UserContext _userContext;
        private readonly HttpClient _httpClient;

        public ICommand StundenplanCommand { get; }
        public ICommand MeldungenAnzeigenCommand { get; }
        public ICommand MeldungenErfassenCommand { get; }

        public NavigationViewModel(INavigation navigation,
                                   IViewMapper viewMapper,
                                   UserContext userContext,
                                   HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _userContext = userContext;
            _httpClient = httpClient;
            StundenplanCommand = new RelayCommand(StundenplanOpen);
            MeldungenAnzeigenCommand = new RelayCommand(MeldungenAnzeigenOpen);
            MeldungenErfassenCommand = new RelayCommand(MeldungenErfassenOpen);
        }

        private void StundenplanOpen (object obj)
        {
            _navigation.PushAsync(_viewMapper.Map(new StundenplanViewModel(_navigation, _viewMapper, _userContext, _httpClient)));
        }
        private void MeldungenAnzeigenOpen (object obj)
        {
            _navigation.PushAsync(_viewMapper.Map(new MeldungenAnzeigenViewModel(_navigation, _viewMapper, _userContext, _httpClient)));
        }
        private void MeldungenErfassenOpen (object obj)
        {
            _navigation.PushAsync(_viewMapper.Map(new MeldungViewModel(_navigation, _viewMapper, _userContext, _httpClient)));
        }
    }
}
