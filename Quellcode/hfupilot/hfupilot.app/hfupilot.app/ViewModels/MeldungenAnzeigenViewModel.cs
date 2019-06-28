using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using hfupilot.app.CustomFramework.mvvm;
using hfupilot.app.Helper;
using hfupilot.app.Services;
using Xamarin.Forms;

namespace hfupilot.app.ViewModels
{
    class MeldungenAnzeigenViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IViewMapper _viewMapper;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public MeldungenAnzeigenViewModel(INavigation navigation,
            IViewMapper viewMapper,
            UserContext userContext,
            HttpClient httpClient)
        {
            _navigation = navigation;
            _viewMapper = viewMapper;
            _httpClient = httpClient;
            _userContext = userContext;
        }


    }
}
